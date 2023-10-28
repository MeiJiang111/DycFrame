using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    /// <summary>
    /// 加载过程中
    /// </summary>
    public Action SceneLoadLoadingEvent;

    /// <summary>
    /// 加载成功显示
    /// </summary>
    public Action SceneLoadActivedEvent;

    /// <summary>
    /// 界面加载成功显示
    /// </summary>
    bool _actived;
   
    string _lastScene = string.Empty;
    string _newScene = string.Empty;

    public bool IsLoading { get; private set; }
    public bool AutoActive { get; private set; }
    public string LoadingSceneName => _newScene;

    SceneInstance _loadScene;
    SceneInstance _preScene;


    protected override void Awake()
    {
        base.Awake();
       
        IsLoading = false;
        _lastScene = string.Empty;
    }

    /// <summary>
    /// 异步加载Scene
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="autoActive_"></param>
    public void OnAsyncLoadScene(string name_, bool autoActive_ = true)
    {
        if (IsLoading)
        {
            LogUtil.LogWarningFormat("have scene is loading ,can not load scene {0}!", name_);
            return;
        }

        _newScene = name_;
        _preScene = _loadScene;
        AutoActive = autoActive_;
        _actived = false;
        SceneLoadLoadingEvent?.Invoke();
        Addressables.LoadSceneAsync(_newScene, LoadSceneMode.Single, AutoActive).Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle_)
    {
        LogUtil.Log("SceneLoader OnSceneLoaded 666");
        if (handle_.Status != AsyncOperationStatus.Succeeded)
        {
            LogUtil.LogErrorFormat("local scene {0} failed", _newScene);
            _newScene = string.Empty;
            return;
        }

        _loadScene = handle_.Result;
        LogUtil.Log("SceneLoader OnSceneLoaded load scene 777 " + _loadScene.Scene.name);
        LogUtil.Log("SceneLoader OnSceneLoaded scene load success 888 888 888 ----------------------------");
     
        if (AutoActive)
        {
            _actived = true;
            _newScene = string.Empty;
            if (!string.IsNullOrEmpty(_lastScene))
            {
                Addressables.UnloadSceneAsync(_preScene);
                _lastScene = string.Empty;
            }
        }
        
        if (AutoActive)
        {
            StartCoroutine(SendActiveSceneEvent());
        }
    }

    IEnumerator SendActiveSceneEvent()
    {
        LogUtil.Log("SceneLoader SendActiveSceneEvent 999");
        yield return null;
        SceneLoadActivedEvent?.Invoke();
    }
}

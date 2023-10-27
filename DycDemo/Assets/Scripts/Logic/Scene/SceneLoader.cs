using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    public Action SceneLoadStartEvent;
    public Action SceneLoadedEvent;
    public Action SceneLoadActivedEvent;

    /// <summary>
    /// 界面加载成功显示
    /// </summary>
    bool _actived;
    string _lastScene = string.Empty;
    string _newScene = string.Empty;

    public bool InLoading { get; private set; }
    public string CurrScene { get; private set; }
    public bool AutoActive { get; private set; }
    public bool LevelActived => _actived;
    public string LoadingLevel => _newScene;

    SceneInstance _loadScene;
    SceneInstance _preScene;


    protected override void Awake()
    {
        base.Awake();
       
        InLoading = false;
        _lastScene = string.Empty;
    }

    /// <summary>
    /// 异步加载Scene
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="autoActive_"></param>
    public void OnAsyncLoadScene(string name_, bool autoActive_ = true)
    {
        LogUtil.Log("SceneLoader OnAsyncLoadScene 333");
        if (InLoading)
        {
            LogUtil.LogWarningFormat("have scene is loading ,can not load scene {0}!", name_);
            return;
        }

        //SceneLoadStartEvent?.Invoke();
        _newScene = name_;
        _preScene = _loadScene;
        AutoActive = autoActive_;
        _actived = false;
        Addressables.LoadSceneAsync(_newScene, LoadSceneMode.Single, AutoActive).Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle_)
    {
        LogUtil.Log("SceneLoader OnSceneLoaded 444");
        InLoading = false;
        if (handle_.Status != AsyncOperationStatus.Succeeded)
        {
            LogUtil.LogErrorFormat("local scene {0} failed", _newScene);
            _newScene = string.Empty;
            return;
        }

        _loadScene = handle_.Result;
        LogUtil.Log("SceneLoader OnSceneLoaded _loadScene 555 == " + _loadScene.Scene.name);
      
        if (AutoActive)
        {
            _actived = true;
            CurrScene = _newScene;
            _newScene = string.Empty;
            if (!string.IsNullOrEmpty(_lastScene))
            {
                Addressables.UnloadSceneAsync(_preScene);
                _lastScene = string.Empty;
            }
        }
        SceneLoadedEvent?.Invoke();

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AsyncPrefabData
{
    public string name;
    public Action<string, GameObject, object> CreatSuccess;
    public Action<string> CreatFaild;
}


/// 场景管理器
public class SceneManager : MonoSingleton<SceneManager>
{
    SceneLoader sceneLoader;
    List<AsyncPrefabData> SceneAsyncPrefabs;

    public Action<string> SceneMgrStartLoadingNewSceneEvent;
    public Action SceneMgrLoadedEvent;
    public Action SceneMgrPreStartEvent;
    public Action SceneMgrStartEvent;

    public bool _isStart;
    string _newScene;
    bool _autoActive;
    int _sceneStartWaitCount = 0;
    int asyncLoadedNum;

    public bool SceneIsStart => _isStart;
    public string CurScene { get; private set; }
    public bool IsMainLevel => (CurScene == Global.MAIN_SCENE_NAME);
    public bool LevelStartPaused
    {
        get
        {
            if (_sceneStartWaitCount > 0) 
            {
                return true;
            }
            return asyncLoadedNum < SceneAsyncPrefabs.Count;
        }
    }

    public float AsyncLoadingPct => asyncLoadedNum * 1f / SceneAsyncPrefabs.Count;
   
   
    protected override void Awake()
    {
        base.Awake();
        
        sceneLoader = SceneLoader.Instance;
        sceneLoader.SceneLoadStartEvent += OnSceneLoadStart;
        sceneLoader.SceneLoadedEvent += OnSceneLoaded;
        sceneLoader.SceneLoadActivedEvent += OnSceneLoadActived;
        SceneAsyncPrefabs = new List<AsyncPrefabData>();
    }

    public void RegisterLoadPrefabs(List<AsyncPrefabInfo> prefabs, Action<string, GameObject, object> success, Action<string> faild = null)
    {
        foreach (var item in prefabs)
        {
            LogUtil.Log("new scene prefab name == " + item.Name);
            SceneAsyncPrefabs.Add(new AsyncPrefabData() { name = item.Name, CreatSuccess = success, CreatFaild = faild });
        }
    }

    void CreatPrefabSuccess(GameObject obj, object parmas = null)
    {
        asyncLoadedNum++;
        var trueName = obj.name.Replace(Global.Clone_Str, "");
       
        AsyncPrefabData asyncPrefabs = new AsyncPrefabData();
        foreach (var item in SceneAsyncPrefabs)
        {
            if (item.name.Equals(trueName))
            {
                asyncPrefabs = item;
            }
        }

        if (string.IsNullOrEmpty(asyncPrefabs.name))
        {
            LogUtil.LogWarningFormat("Creat prefab {0} success but not exists!!!", trueName);
            return;
        }
        asyncPrefabs.CreatSuccess?.Invoke(trueName, obj, parmas);
    }

    void CreatPrefabFaild(string name)
    {
        LogUtil.LogWarningFormat("creat prefab {0} Faild !!!", name);
    }

    #region 加载场景
    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="autoActive"></param>
    /// <returns></returns>
    public bool StartChangeScene(string name_, bool autoActive = true)
    {
        LogUtil.Log("SceneManager StartChangeScene 111");
        if (sceneLoader.InLoading)
        {
            LogUtil.LogWarningFormat("call attempted to LoadScene {0} while a scene is already in the process of loading; ignoring the load request...", sceneLoader.LoadingLevel);
            return false;
        }

        _isStart = false;
        _autoActive = autoActive;
        _newScene = name_;
        _sceneStartWaitCount = 0;
        SceneAsyncPrefabs.Clear();
        asyncLoadedNum = 0;
        StopAllCoroutines();
        StartCoroutine(StartSceneImple());
        return true;
    }

    IEnumerator StartSceneImple()
    {
        LogUtil.Log("SceneManager StartSceneImple 222");
        //SceneMgrStartLoadingNewSceneEvent?.Invoke(_newScene);
        yield return null;
        sceneLoader.OnAsyncLoadScene(_newScene, _autoActive);
    }
    #endregion


    #region SceneLoader 事件执行
    private void OnSceneLoadStart()
    {
        
    }
            
    private void OnSceneLoaded()
    {
        LogUtil.Log("SceneManager OnSceneLoaded Action 666");
        CurScene = _newScene;
        SceneMgrLoadedEvent?.Invoke();
    }

    private void OnSceneLoadActived()
    {
        LogUtil.Log("SceneManager OnSceneLoadActived Action 10 10 10");
        foreach (var item in SceneAsyncPrefabs)
        {
            ResourceManager.Instance.CreatInstanceAsync(item.name, CreatPrefabSuccess, CreatPrefabFaild);
        }

        _autoActive = false;
        StartCoroutine(IenumSceneStart());
    }

    IEnumerator IenumSceneStart()
    {
        LogUtil.Log("SceneManager IenumSceneStart Action 11 11 11");
        yield return null;
        _isStart = false;
        SceneMgrPreStartEvent?.Invoke();

        while (LevelStartPaused)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        _isStart = true;
        SceneMgrStartEvent?.Invoke();
    }
    #endregion


    public void PauseLevelStart()
    {
        _sceneStartWaitCount++;
    }

    public void ResumeLevelStart()
    {
        _sceneStartWaitCount = Mathf.Max(_sceneStartWaitCount - 1, 0);
    }
}

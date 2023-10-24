using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AsyncPrefabs
{
    public string name;

    /// <summary>
    /// 创建成功
    /// </summary>
    public Action<string, GameObject, object> CreatSuccess;

    /// <summary>
    /// 创建失败
    /// </summary>
    public Action<string> CreatFaild;
}


/// 场景管理器
public class SceneManager : MonoSingleton<SceneManager>
{
    SceneLoader sceneLoader;
    List<AsyncPrefabs> SceneAsyncPrefabs;
  
    public Action<string> StartLoadingNewLevelEvent;
    public Action LevelLoadedEvent;
    public Action LevelPreStartEvent;
    public Action LevelStartEvent;
   
    public string CurLevel { get; private set; }
    public bool IsMainLevel => (CurLevel == Global.MAIN_LEVEL_NAME);
   
    public bool LevelStartPaused
    {
        get
        {
            if (_levelStartWaitCount > 0) 
            {
                return true;
            }
            return asyncLoadedNum < SceneAsyncPrefabs.Count;
        }
    }

    public bool _isStart;
    string _newScene;
    bool _autoActive;
    int _levelStartWaitCount = 0;
    int asyncLoadedNum;
    public float AsyncLoadingPct => asyncLoadedNum * 1f / SceneAsyncPrefabs.Count;
   
   
    protected override void Awake()
    {
        base.Awake();
        LogUtil.Log("SceneManager Awake");

        sceneLoader = SceneLoader.Instance;
        sceneLoader.LevelStartLoadEvent += OnLevelStartLoad;
        sceneLoader.LevelLoadedEvent += OnLevelLoaded;
        sceneLoader.LevelActivedEvent += OnLevelActived;
        SceneAsyncPrefabs = new List<AsyncPrefabs>();
    }

    public void RegisterLoadPrefabs(List<AsyncPrefabInfo> prefabs, Action<string, GameObject, object> success, Action<string> faild = null)
    {
        foreach (var item in prefabs)
        {
            SceneAsyncPrefabs.Add(new AsyncPrefabs() { name = item.Name, CreatSuccess = success, CreatFaild = faild });
        }
    }

    void CreatPrefabSuccess(GameObject obj, object parmas = null)
    {
        //asyncLoadedNum++;
        //var trueName = obj.name.Replace(Global.Clone_Str, "");
        //var _info = SceneAsyncPrefabs.Find(trueName);
        
        //if (string.IsNullOrEmpty(_info.name))
        //{
        //    LogUtil.LogWarningFormat("Creat prefab {0} success but not exists!!!", trueName);
        //    return;
        //}
        //_info.CreatSuccess?.Invoke(trueName, obj, parmas);
    }

    void CreatPrefabFaild(string name)
    {
        //asyncLoadedNum++;
        //var _info = SceneAsyncPrefabs.Find(name);
        //if (string.IsNullOrEmpty(_info.name))
        //{
        //    LogUtil.LogWarningFormat("Creat prefab {0} Faild but not exists!!!", name);
        //    return;
        //}
        //_info.CreatFaild?.Invoke(name);
    }


    #region 加载场景
    public bool StartLevel(string name_, bool autoActive = true)
    {
        LogUtil.Log("SceneManager StartLevel");
        if (sceneLoader.InLoading)
        {
            LogUtil.LogWarningFormat("Call attempted to LoadLevel {0} while a level is already in the process of loading; ignoring the load request...", sceneLoader.LoadingLevel);
            return false;
        }

        _isStart = false;
        _autoActive = autoActive;
        _newScene = name_;
        _levelStartWaitCount = 0;
        SceneAsyncPrefabs.Clear();
        asyncLoadedNum = 0;
        StopAllCoroutines();
        StartCoroutine(StartLevelImple());
        return true;
    }

    IEnumerator StartLevelImple()
    {
        yield return null;
        StartLoadingNewLevelEvent?.Invoke(_newScene);
        yield return null;
        sceneLoader.LoadLevelAsync(_newScene, _autoActive);
    }

    public void ActiveLevel()
    {
        if (string.IsNullOrEmpty(_newScene))
        {
            return;
        }
      
        if (!sceneLoader.LevelActived)
        {
            StartCoroutine(sceneLoader.ActiveLevel());
        }
    }
    #endregion


    #region 事件
    private void OnLevelStartLoad()
    {

    }


    private void OnLevelLoaded()
    {
        CurLevel = _newScene;
        LevelLoadedEvent?.Invoke();

        if (!sceneLoader.AutoActive && _autoActive)
        {
            StartCoroutine(sceneLoader.ActiveLevel());
        }
    }

    private void OnLevelActived()
    {
        foreach (var item in SceneAsyncPrefabs)
        {
            ResourceManager.Instance.CreatInstanceAsync(item.name, CreatPrefabSuccess, CreatPrefabFaild);
        }

        _autoActive = false;
        StartCoroutine(LevelStart());
    }

    IEnumerator LevelStart()
    {
        yield return null;
        _isStart = false;
        LevelPreStartEvent?.Invoke();

        while (LevelStartPaused)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        _isStart = true;
        LevelStartEvent?.Invoke();
    }
    #endregion

    public void PauseLevelStart()
    {
        _levelStartWaitCount++;
    }

    public void ResumeLevelStart()
    {
        _levelStartWaitCount = Mathf.Max(_levelStartWaitCount - 1, 0);
    }
}

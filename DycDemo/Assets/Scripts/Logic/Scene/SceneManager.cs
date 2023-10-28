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


/// ����������
public class SceneManager : MonoSingleton<SceneManager>
{
    SceneLoader sceneLoader;
    List<AsyncPrefabData> SceneAsyncPrefabs;

    public Action<string> SceneMgrFirstLoadSceneEvent;
    public Action SceneMgrLoadingEvent;
    public Action SceneMgrPrecentStartEvent;
    public Action SceneMgrLoadEndEvent;

    public bool _isStart;
    string _newScene;
    bool _autoActive;
    int _sceneStartWaitCount = 0;
    int asyncLoadedNum;

    public bool SceneIsStart => _isStart;
    public string CurScene { get; private set; }
    public bool IsMainLevel => (CurScene == Global.MAIN_SCENE_NAME);
    public bool SceneStartPaused
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
        sceneLoader.SceneLoadLoadingEvent += OnSceneLoading;
        sceneLoader.SceneLoadActivedEvent += OnSceneLoadActived;
        SceneAsyncPrefabs = new List<AsyncPrefabData>();
    }

    public void RegisterLoadPrefabs(List<AsyncPrefabInfo> prefabs, Action<string, GameObject, object> success, Action<string> faild = null)
    {
        foreach (var item in prefabs)
        {
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
            LogUtil.LogWarningFormat("creat prefab {0} success but not exists!!!", trueName);
            return;
        }
        asyncPrefabs.CreatSuccess?.Invoke(trueName, obj, parmas);
    }

    void CreatPrefabFaild(string name)
    {
        LogUtil.LogWarningFormat("creat prefab {0} faild !!!", name);
    }

    #region ���س���
    /// <summary>
    /// �л�����
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="autoActive"></param>
    /// <returns></returns>
    public bool StartChangeScene(string name_, bool autoActive = true)
    {
        LogUtil.Log("SceneManager StartChangeScene 111");
        if (sceneLoader.IsLoading)
        {
            LogUtil.LogWarningFormat("call attempted to LoadScene {0} while a scene is already in the process of loading; ignoring the load request...", sceneLoader.LoadingSceneName);
            return false;
        }

        _isStart = false;
        _autoActive = autoActive;
        _newScene = name_;
        _sceneStartWaitCount = 0;
        SceneAsyncPrefabs.Clear();
        asyncLoadedNum = 0;
        StopAllCoroutines();
        StartCoroutine(StartSceneLoad());
        return true;
    }

    IEnumerator StartSceneLoad()
    {
        LogUtil.Log("SceneManager StartSceneLoad 222");
        yield return null;
        sceneLoader.OnAsyncLoadScene(_newScene, _autoActive);
    }
    #endregion


    #region SceneLoader �¼�ִ��
    private void OnSceneLoading()
    {
        LogUtil.Log("SceneManager OnSceneLoading Action 444");
        SceneMgrLoadingEvent?.Invoke();
    }

    private void OnSceneLoadActived()
    {
        LogUtil.Log("SceneManager OnSceneLoadActived Action 10 10 10");
        CurScene = _newScene;

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
        SceneMgrPrecentStartEvent?.Invoke();

        LogUtil.Log("SceneManager IenumSceneStart 13 13 13" + SceneStartPaused);
        while (SceneStartPaused)
        {
            yield return null;
        }

        _isStart = true;
        SceneMgrLoadEndEvent?.Invoke();
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

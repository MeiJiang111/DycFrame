using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialize : MonoSingleton<GameInitialize>
{
    [Serializable]
    public struct LoadPrefabConfig
    {
        public string name;
        public Vector3 pos;
    }

    public bool ShowFrame;                    //��ʾ֡��
    public int TargetFrame;                   //�޶�֡��
    public bool ShowDebugGrid;

    [Header("��������")] public bool update;
    public List<LoadPrefabConfig> firstLoadPrefabs;
    public event Action GameInitEvent;


    protected override void Awake()
    {
        base.Awake();
        LogUtil.Log("Log GameInitialize Awake");

        Application.targetFrameRate = TargetFrame;
        Application.runInBackground = true;
       
        if (ShowFrame)
        {
            gameObject.AddComponent<FrameRate>();
        }
    }

    private void Start()
    {
        LogUtil.Log("Log GameInitialize Start");
        GameUpdate.Instance.StartGameUpdate(update);
    }

    //������Ϸ
    public IEnumerator EnterGame()
    {
        LogUtil.Log("Log GameInitialize EnterGame");
        var resourMgr = ResourceManager.Instance;
        int count = 0;
        count = firstLoadPrefabs.Count;
        foreach (var item in firstLoadPrefabs)
        {
            resourMgr.CreatInstanceAsync(item.name, (obj, parma) =>
            {
                LogUtil.Log("Log EnterGame success  " + obj + "  " + parma);
                obj.name = item.name;
                obj.transform.localPosition = item.pos;
                count--;
            });
        }

        while (count > 0)
        {
            yield return null;
        }

        UIManager.Instance.RegisterListener();
        //CameraController.Instance.RegisterListenner();


        // ����ϵͳ���� todo
        //ConfigManager.Instance.LoadAllConfigs();
        //yield return new WaitUntil(() => { return ConfigManager.Instance.IsLoaded; });


        //resourMgr.PreLoads();

        //while (!resourMgr.PreLoadFinish)
        //{
        //    yield return null;
        //}

        OnGameInit();

        var uiMgr = UIManager.Instance;
        uiMgr.AsyncLoadPreLoadingPanels(SceneType.All);
        while (uiMgr.HasWaite)
        {
            yield return null;
        }
        LogUtil.Log("Game Initialize Pre loading finish!!!!");
        yield return new WaitForEndOfFrame();

        //LevelManager.Instance.StartLevel(Global.LOGIN_LEVEL_NAME);
    }

    void OnGameInit()
    {
        //CacheResource.CheckCacheDir();
        //GameInitEvent?.Invoke();
    }

    void OnLoadUpdataPanel()
    {
        MainSceneUIManager.Instance.OpenTargetSystem(SystemType.Fishery);
    }
}

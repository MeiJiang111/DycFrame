using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UIFramework;

public class GameInitialize : MonoSingleton<GameInitialize>
{
    [Serializable]
    public struct LoadPrefabConfig
    {
        public string name;
        public Vector3 pos;
    }

    public bool ShowFrame;                    //显示帧数
    public int TargetFrame;                   //限定帧数
   
    [Header("开启更新")] public bool update;
    public List<LoadPrefabConfig> firstLoadPrefabs;
    public event Action GameInitEvent;


    protected override void Awake()
    {
        base.Awake();
        LogUtil.Log("GameInitialize Awake");

        Application.targetFrameRate = TargetFrame;
        Application.runInBackground = true;
       
        if (ShowFrame)
        {
            gameObject.AddComponent<FrameRate>();
        }
    }

    private void Start()
    {
        LogUtil.Log("GameInitialize Start");
        GameUpdate.Instance.StartGameUpdate(update);
    }

    //进入游戏
    public IEnumerator EnterGame()
    {
        LogUtil.Log("GameInitialize EnterGame");
        var resourMgr = ResourceManager.Instance;
        int count = 0;
        count = firstLoadPrefabs.Count;
        foreach (var item in firstLoadPrefabs)
        {
            resourMgr.CreatInstanceAsync(item.name, (obj, parma) =>
            {
                LogUtil.Log("GameInitialize EnterGame success = " + obj + "  " + parma);
                obj.name = item.name;
                obj.transform.localPosition = item.pos;
                count--;
            });
        }

        while (count > 0)
        {
            yield return null;
        }

        UIFrame.Instance.RegisterListener();
        LogUtil.Log("GameInitialize GameInitialize Loading Finish !!!");
       
        yield return new WaitForEndOfFrame();

        SceneManager.Instance.StartLevel(Global.LOGIN_LEVEL_NAME);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UIFramework;

public class GameInitialize : MonoSingleton<GameInitialize>
{
    [Serializable]
    public struct PrefabConfig
    {
        public string name;
        public Vector3 pos;
    }

    public bool ShowFrame;           //显示帧数        
    public int TargetFrame;          //限定帧数       

    [Header("开启更新")] public bool update;
    public List<PrefabConfig> prefabList;
    public event Action GameInitEvent;

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = TargetFrame;
        Application.runInBackground = true;
        
        if (ShowFrame)
        {
            gameObject.AddComponent<FrameRate>();
        }
    }

    private void Start()
    {
        GameUpdate.Instance.StartGameUpdate(update);
    }

    public IEnumerator EnterGame()
    {
        foreach (var item in prefabList)
        {
            ResourceManager.Instance.CreatInstanceAsync(item.name, (obj, parma) =>
            {
                obj.name = item.name;
                obj.transform.localPosition = item.pos;
            });
        }

        LogUtil.Log("GameInitialize Loading Finish !!!");
        ConfigManager.Instance.Init();
        yield return new WaitForEndOfFrame();

        UIFrame.Instance.RegisterListener();
        PanelInitialize.Instance.RegisterListener();
        yield return new WaitForEndOfFrame();
        SceneManager.Instance.StartChangeScene(Global.LOGIN_SCENE_NAME);
    }
}

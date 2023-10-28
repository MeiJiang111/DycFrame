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

    public bool ShowFrame;           //��ʾ֡��        
    public int TargetFrame;          //�޶�֡��       

    [Header("��������")] public bool update;
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
        int count = 0;
        count = prefabList.Count;
        foreach (var item in prefabList)
        {
            ResourceManager.Instance.CreatInstanceAsync(item.name, (obj, parma) =>
            {
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
        LogUtil.Log("GameInitialize Loading Finish !!!");
        yield return new WaitForEndOfFrame();

        SceneManager.Instance.StartChangeScene(Global.LOGIN_SCENE_NAME);
    }
}

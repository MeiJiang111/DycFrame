using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public long CurTimestamp;
    public int SecondForMinute = 1;
    public bool ASlash = true;
    Dictionary<Type, GameSystem> systemManagers;
    GlobalConfigBean globalSetting;
    public GlobalConfigBean GlobalSetting => globalSetting;


    protected override void Awake()
    {
        base.Awake();

        GameInitialize.Instance.GameInitEvent += OnGameInit;

        systemManagers = new Dictionary<Type, GameSystem>();
        AddAllManagers();
       
        //LevelManager.Instance.LevelPreStartEvent += OnLevelPreStart;
        //NetWorkManager.Instance.NetDropedEvent += OnNetDroped;

        TimerEventMgr.Instance.Add(1, Tick, -1);
    }

    private void Tick()
    {
        //CurTimestamp = TimeManager.Instance.Timestamp;
    }

    private void OnNetDroped()
    {
        var loginMgr = GetManager<LoginManager>();
        loginMgr.ReTryLogin();
    }

    string curLevelName;
    private void OnLevelPreStart()
    {
        //curLevelName = LevelManager.Instance.CurLevel;
        //if (curLevelName == Global.MAIN_LEVEL_NAME)
        //{
        //    EffectManager.Instance.PreLoadeEffect();
        //}
    }


    public void OnGameInit()
    {
        globalSetting = ConfigManager.Instance.GetContainer<GlobalConfigContainer>().GetDataBean(1);
        foreach (var mgr in systemManagers)
        {
            mgr.Value.Init();
        }
    }

    public void GameStart()
    {
        foreach (var mgr in systemManagers)
        {
            mgr.Value.OnStart();
        }
        SceneManager.Instance.StartLevel(Global.MAIN_LEVEL_NAME);
    }

    public void PlayerLoginIn(bool init_)
    {
        foreach (var mgr in systemManagers)
        {
            mgr.Value.OnLogin(init_);
        }
    }



    public void OnLogout()
    {
        foreach (var mgr in systemManagers)
        {
            mgr.Value.OnLogout();
        }
    }


    public void Save()
    {
        foreach (var mgr in systemManagers)
        {
            mgr.Value.Save(CurTimestamp);
        }
    }

    private void OnApplicationQuit()
    {
        if (curLevelName == Global.MAIN_LEVEL_NAME)
            Save();
    }

    public T GetManager<T>() where T : GameSystem
    {
        return GetManagerEx<T>(typeof(T));
    }

    T GetManagerEx<T>(Type type_) where T : GameSystem
    {
        if (systemManagers.ContainsKey(type_))
        {
            return (T)systemManagers[type_];
        }
        return null;
    }
    void AddAllManagers()
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(GameSystem).IsAssignableFrom(type))
                {
                    if (type.IsClass && !type.IsAbstract)
                    {
                        systemManagers.Add(type, (GameSystem)assembly.CreateInstance(type.Name));
                    }
                }
            }
        }
    }
}

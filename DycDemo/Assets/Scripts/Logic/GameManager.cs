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
   

    protected override void Awake()
    {
        base.Awake();

        GameInitialize.Instance.GameInitEvent += OnGameInit;

        systemManagers = new Dictionary<Type, GameSystem>();
        AddAllManagers();
       
        TimerEventMgr.Instance.Add(1, Tick, -1);
    }

    private void Tick()
    {
       
    }

  

    string curLevelName;
   


    public void OnGameInit()
    {
      
    }

    public void GameStart()
    {
        foreach (var mgr in systemManagers)
        {
            mgr.Value.OnStart();
        }
        SceneManager.Instance.StartChangeScene(Global.MAIN_SCENE_NAME);
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
        if (curLevelName == Global.MAIN_SCENE_NAME)
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

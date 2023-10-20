using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    private static bool applicationIsQuitting = false;

    /// <summary>
    /// 直接返回实例
    /// </summary>
    public static T RawInstance
    {
        get { return _instance; }
    }

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                //LogUtil.LogWarningFormat("[Singleton] Instance '{0}' already destroyed on application quit. Won't create again - returning null.", typeof(T));
                _instance = null;
                return _instance;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        //LogUtil.LogErrorFormat("[Singleton] Something went really wrong - there should never be more than 1 singleton: {0}! Reopenning the scene might fix it.", typeof(T));

                        return _instance;
                    }

                    if (_instance == null)
                    {
                        var startTime = Time.realtimeSinceStartup;
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();

                        //LogUtil.LogFormat("[Singleton] An instance of {0} was created with DontDestroyOnLoad, elapsed: {1}.", typeof(T), Time.realtimeSinceStartup - startTime);
                    }
                    else
                    {
                        //LogUtil.LogFormat("[Singleton] {0} Using instance of already created {1}!", typeof(T), _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
#if UNITY_EDITOR
        if (FindObjectsOfType(typeof(T)).Length > 1)
        {
            LogUtil.LogErrorFormat("[Singleton] Something went really wrong - there should never be more than 1 singleton: {0}! Reopenning the scene might fix it.", typeof(T));

            return;
        }
#endif
        _instance = this as T;

        if (_instance.transform.parent == null)
        {
            DontDestroyOnLoad(_instance.gameObject);
        }
    }

   
    public static bool ApplicationIsQuit
    {
        get { return applicationIsQuitting; }
    }


    void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}

using UnityEngine;

/// <summary>
/// 泛型单例基类 任何继承自该类的类，都是单例类
/// </summary>
/// <typeparam name="T">泛型</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance_;
    public static T Instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = FindObjectOfType(typeof(T)) as T;
                if (instance_ == null) 
                {
                    GameObject singleton = new GameObject();
                    instance_ = singleton.AddComponent<T>();
                    singleton.name = typeof(T).ToString();
                }
            }

            return instance_;
        }
    }

    protected virtual void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this as T;
        }

        if (instance_.transform.parent == null)
        {
            DontDestroyOnLoad(instance_.gameObject);
        }
    }

    public void OnApplicationQuit()
    {
        instance_ = null;
    }
}

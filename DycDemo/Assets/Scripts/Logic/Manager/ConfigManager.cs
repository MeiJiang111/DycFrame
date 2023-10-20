using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class ConfigManager : MonoSingleton<ConfigManager>
{
    public Dictionary<Type, BaseDataContainer> containers = new Dictionary<Type, BaseDataContainer>();
    private int _cur;   //��ǰ���ؽ���
    private int _total; //�ܼ��ؽ���
    bool _isInLoading;
    public int Cur
    {
        get
        {
            return _cur;
        }

        set
        {
            _cur = value;
        }
    }

    private bool isLoading => _isInLoading;
    bool _isLoaded;
    public bool IsLoaded => _isLoaded;

    /// <summary>
    /// ��ʼ����ͨ���������ļ�
    /// </summary>
    public void LoadAllConfigs()
    {
        if (_isInLoading || IsLoaded)
        {
            return;
        }
        _isInLoading = true;
        List<Type> types = new List<Type>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(BaseDataContainer).IsAssignableFrom(type))
                {
                    if (type.IsClass && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
            }
        }
        _total = types.Count;
        //�������ñ�
        foreach (Type type in types)
        {
            BaseDataContainer container = GetContainer<BaseDataContainer>(type);
            if (container != null)
            {
                container.Load();
            }
            else
            {
                LogUtil.LogErrorFormat("load config by {0} error:", type.GetType().ToString());
                _cur++;
            }
        }
    }


    public void LoadConfigFile<T>(string configName, Action<T, object> callBack = null, object param = null) where T : BaseDataContainer
    {
        ResourceManager.Instance.LoadAsset<T>(configName, (obj, param_) =>
        {
            _cur++;
            if (_cur == _total) { _isInLoading = false; _isLoaded = true; }
            callBack?.Invoke(obj, param_);
        }, (name_) =>
        {
            LogUtil.LogErrorFormat("LoadConfig {0} faild!", name);
            //callBack?.Invoke(null, null);
        }, param);
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    private T GetContainer<T>(Type type) where T : BaseDataContainer
    {
        if (containers.ContainsKey(type))
        {
            return (T)containers[type];
        }
        else
        {
            T instance = (T)ScriptableObject.CreateInstance(type.Name);
            containers.Add(type, instance);
            return instance;
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetContainer<T>() where T : BaseDataContainer
    {
        Type type = typeof(T);
        return GetContainer<T>(type);
    }
}

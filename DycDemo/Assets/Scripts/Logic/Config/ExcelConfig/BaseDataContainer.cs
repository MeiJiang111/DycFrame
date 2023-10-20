using System;
using UnityEngine;
using Newtonsoft.Json;
/*
* 基础数据容器类
* **/
[Serializable]
public abstract  class BaseDataContainer: ScriptableObject
{
    /// <summary>
    /// 加载二进制文件
    /// </summary>
    public abstract void Load() ;


    /// <summary>
    /// 获得对应配置文件名
    /// </summary>
    /// <returns></returns>
    public abstract string GetConfigName();

    public T DeserializeObject<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return default(T);
        }
        T t = default(T);

        try
        {
            t = JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception e)
        {
            LogUtil.LogError(e.StackTrace);
            throw e;
        }
        return t;
    }


    public virtual void OnLoaded() { }

}

using Newtonsoft.Json;
using System;

public class BaseBean
{
    /// <summary>
    /// Bean加载完成，可以复写这个方法处理预解析内容
    /// </summary>
    public virtual void OnLoaded() { }

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
            LogUtil.LogError(e.StackTrace + "   " + json);
        }
        return t;
    }
}

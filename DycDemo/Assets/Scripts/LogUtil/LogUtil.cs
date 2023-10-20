using System;
using UnityEngine;

public class LogUtil
{
    public static event Action<string> LogAction;

    public static void LogToActionEvent(string log_)
    {
        LogAction?.Invoke(log_);
        Debug.Log(log_);
    }

    public static void Log(object log_)
    {
        Debug.Log(log_);
    }

    public static void LogFormat(string log_, params object[] args)
    {
        Debug.LogFormat(log_, args);
    }
    public static void LogWarning(object log_)
    {
        Debug.LogWarning(log_);
    }

    public static void LogWarningFormat(string log_, params object[] args)
    {
        Debug.LogWarningFormat(log_, args);
    }
    public static void LogError(object log_)
    {
        Debug.LogError(log_);
    }

    public static void LogErrorFormat(string log_, params object[] args)
    {
        Debug.LogErrorFormat(log_, args);
    }
}

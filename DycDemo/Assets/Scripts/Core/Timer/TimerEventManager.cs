using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimerEventMgr : ASingleton<TimerEventMgr>
{
    int _id = 0;
    List<TimerEvent> _activeList = new List<TimerEvent>();
    List<TimerEvent> _poolList = new List<TimerEvent>();


    public List<TimerEvent> ActiveList
    {
        get { return _activeList; }
        set { _activeList = value; }
    }

    public List<TimerEvent> PoolList
    {
        get { return _poolList; }
        set { _poolList = value; }
    }


    void Update()
    {
        for (var i = _activeList.Count - 1; i >= 0; i--)
        {
            _activeList[i].Execute();
        }
    }

    //=============不带参数的=======
    /// <summary>
    /// 执行1次
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="func"></param>
    /// <param name="useScaleTime"></param>
    /// <returns></returns>
    public TimerEvent Add(float delay, Action func, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, func, null, null, null, 1, -1f, timeType);
    }

    /// <summary>
    /// 执行指定次数，间隔时间等于首次执行等待时间
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="func"></param>
    /// <param name="iterations"></param>
    /// <param name="useScaleTime"></param>
    /// <returns></returns>
    public TimerEvent Add(float delay, Action func, int iterations, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, func, null, null, null, iterations, -1f, timeType);
    }

    /// <summary>
    /// Add TimerEvent
    /// </summary>
    /// <param name="delay">Delay before first call</param>
    /// <param name="func">Callback function</param>
    /// <param name="iterations">0 means infinite</param>
    /// <param name="interval">iteration interval，-1时等于delay</param>
    /// <returns></returns>
    public TimerEvent Add(float delay, Action func, int iterations, float interval, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, func, null, null, null, iterations, interval, timeType);
    }

    public TimerEvent Add(float delay, Action func, Action funcComplete, int iterations, float interval, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, func, null, null, funcComplete, iterations, interval, timeType);
    }

    //=============带参数的=======
    public TimerEvent Add(float delay, Action<object> func, object argument, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, null, func, argument, null, 1, -1f, timeType);
    }

    public TimerEvent Add(float delay, Action<object> func, object argument, int iterations, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, null, func, argument, null, iterations, -1f, timeType);
    }

    public TimerEvent Add(float delay, Action<object> func, object argument, int iterations, float interval, TimeType timeType = TimeType.Scaled)
    {
        return AddTimerEvent(delay, null, func, argument, null, iterations, interval, timeType);
    }

    TimerEvent AddTimerEvent(float delay, Action func, Action<object> argFunc, object arguments, Action funcComplete,
        int iterations, float interval, TimeType timeType)
    {
        if (func == null && argFunc == null)
        {
            Debug.LogWarning("Failed to add timer, callback is null!");
            return null;
        }

        _id++;
        delay = Mathf.Max(0, delay);
        iterations = Mathf.Max(0, iterations);
        interval = interval == -1 ? delay : Mathf.Max(0, interval);

        TimerEvent newTimerEvent = null;
        if (_poolList.Count > 0)
        {
            newTimerEvent = _poolList[0];
            _poolList.Remove(newTimerEvent);
        }
        else
        {
            newTimerEvent = new TimerEvent();
        }

        newTimerEvent.Reset(_id, delay, func, argFunc, arguments, funcComplete, iterations, interval, timeType);

        _activeList.Add(newTimerEvent);
        return newTimerEvent;
    }

    public void Cancel(TimerEvent timerEvent)
    {
        if (timerEvent == null)
        {
            return;
        }

        timerEvent.Cancel();
    }

}


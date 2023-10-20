using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TimeType
    {
        Unscaled,   // 不受逻辑scale影响
        Scaled,     // 受逻辑scale影响
    }

public class TimerEvent
{

    public event Action OnCancleEvent;

    const float MAX_TOLERANCE = 2f;

    int _id;
    Action _func;
    Action<object> _argFunc;
    object _arguments;
    Action _funcComplete;
    int _iterations;

    float _interval; // 更新间隔
    TimeType _timeType;
    float _startTime;
    float _dueTime;

    public TimerEvent()
    {
        _id = 0;

        _func = null;
        _argFunc = null;
        _arguments = null;
        _funcComplete = null;
        _iterations = 1;
        _interval = -1;
        _timeType = TimeType.Unscaled;
        _startTime = 0;
        _dueTime = 0;
    }

    public void Reset(int id, float delay, Action func, Action<object> argFunc, object arguments, Action funcComplete,
        int iterations, float interval, TimeType timeType)
    {
        _id = id;

        _func = func;
        _argFunc = argFunc;
        _arguments = arguments;
        _funcComplete = funcComplete;
        _iterations = iterations;
        _interval = interval;
        _timeType = timeType;
        _startTime = GetTime(_timeType);
        _dueTime = _startTime + delay;
    }

    public void Execute()
    {
        if (Id == 0)
        {
            Recycle();
            return;
        }

        var curTime = _startTime = GetTime(_timeType);

        if (curTime < _dueTime)
        {
            return;
        }



        if (_func != null)
            _func.Invoke();
        else if (_argFunc != null)
            _argFunc.Invoke(_arguments);


        if (_iterations > 0)
        {
            _iterations--;
            if (_iterations == 0)
            {
                _funcComplete?.Invoke();
                Recycle();
                return;
            }
        }
        
        var diff = curTime - _dueTime;  // 时间误差
        if (diff < MAX_TOLERANCE)       // 小于允许的最大误差进行补偿
        {
            _dueTime = curTime + Interval - diff;
            //_dueTime += Interval; //时间误差约分后逻辑
        }
        else
        {
            _dueTime = curTime + Interval;
        }
    }

    void Recycle()
    {
        _id = 0;
        _func = null;
        _argFunc = null;
        _arguments = null;
        _funcComplete = null;

        _iterations = 0;
        _interval = 0;
        _timeType = TimeType.Unscaled;
        _startTime = 0;
        _dueTime = 0;

        var timerEventMgr = TimerEventMgr.Instance;
        if (timerEventMgr.ActiveList.Remove(this))
            timerEventMgr.PoolList.Add(this);
    }

    public void Cancel()
    {
        if (_id == 0)
        {
            Debug.LogWarning("Timer was already removed!");
            return;
        }

        _id = 0;

        _func = null;
        _argFunc = null;
        _arguments = null;
        _funcComplete = null;
        OnCancleEvent?.Invoke();
    }

    public int Id
    {
        get { return _id; }
    }

    public Action Func
    {
        get { return _func; }
    }

    public Action<object> ArgFunc
    {
        get { return _argFunc; }
    }

    public object Arguments
    {
        get { return _arguments; }
    }

    public float Interval
    {
        get { return _interval; }
    }

    public float DueTime
    {
        get { return _dueTime; }
    }

    public int Iterations
    {
        get { return _iterations; }
    }

    public bool IsActive
    {
        get { return Id != 0; }
    }


    public float TimeUntilNextIteration
    {
        get
        {
            if (IsActive)
                return _dueTime - GetTime(_timeType);
            return 0.0f;
        }
    }


    public float DurationLeft
    {
        get
        {
            if (IsActive)
                return TimeUntilNextIteration + ((_iterations - 1) * _interval);
            return 0.0f;
        }
    }

    /// <summary>
    /// 返回计时器经过的总时间
    /// </summary>
    public float Elapsed
    {
        get { return GetTime(_timeType) - _startTime; }
    }

    static float GetTime(TimeType timeType)
    {
        //return timeType == TimeType.Unscaled ? TimeManager.Instance.UnScaleTotalTime : TimeManager.Instance.ScaleTotalTime;
        return 10f;
    }
}


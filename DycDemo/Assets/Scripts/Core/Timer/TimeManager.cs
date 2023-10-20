//using Newtonsoft.Json;
//using System;
//using UnityEngine;

//public class TimeManager : ASingleton<TimeManager>
//{
//    float _totalTime; //不受业务逻辑scale影响的时间
//    float _totalScaleTime; //受业务逻辑scale影响的时间

//    float _timeScaleEx = 1;
//    float _logicTimeScale = 1;


//    TimeScaleEffect _timeScaleEffect ;
//    bool _isSlowTime = false;

//    public float deltaTime { get; private set; }
//    public float unscaledDeltaTime { get; private set; }
//    /// <summary>
//    /// 当前的时间戳
//    /// </summary>
//    public long Timestamp
//    {
//        get
//        {
//            TimeSpan ts = DateTime.Now - zeroDateTime;
//            return Convert.ToInt64(ts.TotalSeconds);
//        }
//    }
//    DateTime zeroDateTime;

//    protected override void Awake()
//    {
//        base.Awake();
//        _totalTime = 0;
//        _totalScaleTime = 0;
//        zeroDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
//    }

//    private void Start()
//    {
//        _timeScaleEffect = new TimeScaleEffect();
//    }

//    /// <summary>
//    /// 业务逻辑的scale
//    /// </summary>
//    public float LogicTimeScale
//    {
//        get { return _logicTimeScale; }
//        set
//        {
//            if (_logicTimeScale == value)
//                return;
//            _logicTimeScale = value;
//        }
//    }

//    /// <summary>
//    /// 用于总体节奏控制的 scale   
//    /// 业务逻辑不要使用这个
//    /// </summary>
//    public float TimeScale
//    {
//        get { return _timeScaleEx; }
//        set
//        {
//            if (_timeScaleEx == value)
//                return;
//            _timeScaleEx = value;
//        }
//    }

//    public float TotalTimeScale => _logicTimeScale * _timeScaleEx;

//    private void Update()
//    {
//        //必须先处理逻辑上的时间倍率
//        if (_timeScaleEffect.IsEnabled)
//        {
//            if (!_timeScaleEffect.Update())
//            {
//                _isSlowTime = false;
//            }
//        }
//        unscaledDeltaTime = Time.unscaledDeltaTime * _timeScaleEx;
//        deltaTime = unscaledDeltaTime * _logicTimeScale;
//        _totalTime += unscaledDeltaTime;
//        _totalScaleTime += deltaTime;

//        if (Input.GetKeyDown(KeyCode.T))
//            GameManager.Instance.GetManager<TouristerManager>().NewTouristersArrivaled(100);

//        if (Input.GetKeyDown(KeyCode.Y))
//            GameManager.Instance.GetManager<IsLandBuildingManager>().ShowIsLandDetail();

//        if (Input.GetKeyDown(KeyCode.I))
//            GameManager.Instance.GetManager<TouristerManager>().SpawnStrollingTourister();
//        if (Input.GetKeyDown(KeyCode.S))
//            GameManager.Instance.Save();

//        //if (Input.GetKeyDown(KeyCode.A))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.Rain);
//        //}
//        //if (Input.GetKeyDown(KeyCode.S))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.RainStorm);
//        //}
//        //if (Input.GetKeyDown(KeyCode.D))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.Cloudy);
//        //}
//        //if (Input.GetKeyDown(KeyCode.F))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.Snow);
//        //}
//        //if (Input.GetKeyDown(KeyCode.G))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.Blizzard);
//        //}
//        //if (Input.GetKeyDown(KeyCode.Space))
//        //{
//        //    GameManager.Instance.GetManager<WeatherManager>().ChangeWeather(WeatherType.Sunny);
//        //}
//    }

   

//    /// <summary>
//    /// 受缩放影响
//    /// </summary>
//    public float ScaleTotalTime => _totalScaleTime;
//    /// <summary>
//    /// 不受缩放影响
//    /// </summary>
//    public float UnScaleTotalTime => _totalTime;

//    public int UnScaleTotalTimeInt => Mathf.CeilToInt(_totalTime);
//    public int ScaleTotalTimeInt => Mathf.CeilToInt(_totalScaleTime);
//    public void SlowTime(float timeScale, float lerpTime, float dealy,
//                    bool enterLerp = true, bool exitLerp = true, bool exclusively = false)
//    {
//        if (_isSlowTime)
//        {
//            return;
//        }
//        _isSlowTime = exclusively;

//        _timeScaleEffect.Start(timeScale, lerpTime, dealy, enterLerp, exitLerp);
//    }

//    public void StopSlowTime()
//    {
//        if (!_timeScaleEffect.IsEnabled)
//        {
//            return;
//        }
//        _isSlowTime = false;

//        _timeScaleEffect.Stop();
//    }
//}

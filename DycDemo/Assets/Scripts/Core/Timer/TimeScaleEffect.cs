using UnityEngine;

public class TimeScaleEffect
{
    public enum Phase
    {
        FadeIn,
        Persistent,
        FadeOut,
    }

    float _targetTimeScale;
    float _lerpTime;
    float _dealy;
    bool _isFadeIn;
    bool _isFadeOut;

    bool _isEnabled;
    Phase _phase;
    float _phaseStartTime;

    //TimeManager _timeMgr;
    //public TimeScaleEffect()
    //{
    //    _timeMgr = TimeManager.Instance;
    //}

    public bool IsEnabled
    {
        get { return _isEnabled; }
    }
    
    public void Start(float timeScale, float lerpTime, float dealy,
                      bool isFadeIn, bool isFadeOut)
    {
        if (_isEnabled)
        {
            Stop();
        }

        _isEnabled = true;

        _targetTimeScale = timeScale;
        _lerpTime = lerpTime;
        _dealy = dealy;
        _isFadeIn = isFadeIn;
        _isFadeOut = isFadeOut;

        _phase = (_isFadeIn && _lerpTime >= 0.1f) ? Phase.FadeIn : Phase.Persistent;
        //_phaseStartTime = _timeMgr.UnScaleTotalTime;
    }

    public bool Update()
    {
        if (!_isEnabled)
        {
            return false;
        }

        switch (_phase)
        {
            case Phase.FadeIn:
                UpdateFadeIn();
                break;
            case Phase.Persistent:
                UpdatePersistent();
                break;
            case Phase.FadeOut:
                UpdateFadeOut();
                break;
        }

        return _isEnabled;
    }

    void UpdateFadeIn()
    {
        //var elapsed = _timeMgr.UnScaleTotalTime - _phaseStartTime;
        //if (elapsed < _lerpTime)
        //{
        //    var lerpScale = 1f / _lerpTime;
        //    var t = elapsed * lerpScale;
        //    var scale = Mathf.Lerp(1f, _targetTimeScale, t);

        //    TimeManager.Instance.LogicTimeScale = scale;
        //}
        //else
        //{
        //    _phase = Phase.Persistent;
        //    _phaseStartTime = _timeMgr.UnScaleTotalTime;
        //}
    }

    void UpdatePersistent()
    {
        //if (_timeMgr.UnScaleTotalTime - _phaseStartTime < _dealy)
        //{
        //    TimeManager.Instance.LogicTimeScale = _targetTimeScale;
        //}
        //else
        //{
        //    if (_isFadeOut && _lerpTime >= 0.1f)
        //    {
        //        _phase = Phase.FadeOut;
        //        _phaseStartTime = _timeMgr.UnScaleTotalTime;
        //    }
        //    else
        //    {
        //        Stop();
        //    }
        //}
    }

    void UpdateFadeOut()
    {
        //var elapsed = _timeMgr.UnScaleTotalTime - _phaseStartTime;
        //if (elapsed < _lerpTime)
        //{
        //    var lerpScale = 1f / _lerpTime;
        //    var t = elapsed * lerpScale;
        //    var scale = Mathf.Lerp(_targetTimeScale, 1f, t);

        //    TimeManager.Instance.LogicTimeScale = scale;
        //}
        //else
        //{
        //    Stop();
        //}

    }

    public void Stop()
    {
        //if (!_isEnabled)
        //{
        //    return;
        //}
        //_isEnabled = false;

        //TimeManager.Instance.LogicTimeScale = 1f;
    }
}

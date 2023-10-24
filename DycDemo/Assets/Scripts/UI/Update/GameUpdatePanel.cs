using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUpdatePanel : MonoBehaviour
{
    public Text stateLabel;
    public Text sliderLabel;
    public Slider slider;

    float SliderValue
    {
        set
        {
            slider.value = value;
            sliderLabel.text = string.Format("{0:P2}", value);
        }
    }

    private void Awake()
    {
        LogUtil.Log("Script GameUpdatePanel Awake");
        stateLabel.text = string.Empty;
        SliderValue = 0;
        var update = GameUpdate.Instance;
        update.UpdateStateChangedEvent += OnUpdateStateChanged;
        update.DownLoadProcessChangeEvent += OnDownLoadProcessChanged;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnDownLoadProcessChanged(float obj)
    {
        SliderValue = obj;
    }

    private void OnUpdateStateChanged(GameUpdate.UpdateState obj)
    {
        switch (obj)
        {
            case GameUpdate.UpdateState.None:
                stateLabel.text = string.Empty;
                break;
            case GameUpdate.UpdateState.Init:
                stateLabel.text = "初始化中...";
                break;
            case GameUpdate.UpdateState.VerifyVersion:
                stateLabel.text = "版本对比中...";
                break;
            case GameUpdate.UpdateState.VerifyVersionSuccess:
                stateLabel.text = "版本对比完毕";
                break;
            case GameUpdate.UpdateState.Download:
                stateLabel.text = "更新资源下载中...";
                break;
            case GameUpdate.UpdateState.Failed:
                stateLabel.text = "版本更新失败";
                break;
            case GameUpdate.UpdateState.Finish:
                stateLabel.text = "版本更新完成";
                break;
        }
    }
}

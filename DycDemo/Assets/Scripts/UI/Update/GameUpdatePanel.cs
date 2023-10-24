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
                stateLabel.text = "��ʼ����...";
                break;
            case GameUpdate.UpdateState.VerifyVersion:
                stateLabel.text = "�汾�Ա���...";
                break;
            case GameUpdate.UpdateState.VerifyVersionSuccess:
                stateLabel.text = "�汾�Ա����";
                break;
            case GameUpdate.UpdateState.Download:
                stateLabel.text = "������Դ������...";
                break;
            case GameUpdate.UpdateState.Failed:
                stateLabel.text = "�汾����ʧ��";
                break;
            case GameUpdate.UpdateState.Finish:
                stateLabel.text = "�汾�������";
                break;
        }
    }
}

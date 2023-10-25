using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUpdatePanel : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textLabel;
    public TextMeshProUGUI textPercent;

    float SliderValue
    {
        set
        {
            slider.value = value;
            textPercent.text = string.Format("{0:P2}", value);
        }
    }

    private void Awake()
    {
        textLabel.text = string.Empty;
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
                textLabel.text = string.Empty;
                break;
            case GameUpdate.UpdateState.Init:
                textLabel.text = "初始化中...";
                break;
            case GameUpdate.UpdateState.VerifyVersion:
                textLabel.text = "版本对比中...";
                break;
            case GameUpdate.UpdateState.VerifyVersionSuccess:
                textLabel.text = "版本对比完毕";
                break;
            case GameUpdate.UpdateState.Download:
                textLabel.text = "更新资源下载中...";
                break;
            case GameUpdate.UpdateState.Failed:
                textLabel.text = "版本更新失败";
                break;
            case GameUpdate.UpdateState.Finish:
                textLabel.text = "版本更新完成";
                break;
        }

        if(obj == GameUpdate.UpdateState.Finish)
        {
            ResourceManager.Instance.DestroyInstance(this.gameObject);
        }
    }
}

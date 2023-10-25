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
                textLabel.text = "��ʼ����...";
                break;
            case GameUpdate.UpdateState.VerifyVersion:
                textLabel.text = "�汾�Ա���...";
                break;
            case GameUpdate.UpdateState.VerifyVersionSuccess:
                textLabel.text = "�汾�Ա����";
                break;
            case GameUpdate.UpdateState.Download:
                textLabel.text = "������Դ������...";
                break;
            case GameUpdate.UpdateState.Failed:
                textLabel.text = "�汾����ʧ��";
                break;
            case GameUpdate.UpdateState.Finish:
                textLabel.text = "�汾�������";
                break;
        }

        if(obj == GameUpdate.UpdateState.Finish)
        {
            ResourceManager.Instance.DestroyInstance(this.gameObject);
        }
    }
}

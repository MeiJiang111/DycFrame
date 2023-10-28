using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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

        var sceneMgr = SceneManager.Instance;
        sceneMgr.SceneMgrLoadingEvent = OnOpenLogin;
    }

    private void OnDestroy()
    {
        var update = GameUpdate.Instance;
        if (update != null)
        {
            update.UpdateStateChangedEvent -= OnUpdateStateChanged;
            update.DownLoadProcessChangeEvent -= OnDownLoadProcessChanged;
        }

        var sceneMgr = SceneManager.Instance;
        if (sceneMgr != null)
        {
            sceneMgr.SceneMgrLoadingEvent -= OnOpenLogin;
        }
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
    }

    //private void OnClosePanel()
    //{
    //    LogUtil.Log("GameUpdatePanel OnClosePanel Action 14 14 14");
    //    ResourceManager.Instance.DestroyInstance(this.gameObject);
    //}

    private void OnOpenLogin()
    {
        LogUtil.Log("GameUpdatePanel OnClosePanel Action 14 14 14");
        ResourceManager.Instance.DestroyInstance(this.gameObject);
    }
}

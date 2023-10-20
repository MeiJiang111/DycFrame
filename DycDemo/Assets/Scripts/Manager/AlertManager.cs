using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipsPivot
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Center,
    Right,
    BottomLeft,
    Bottom,
    BottomRight,
}

public enum AlertClickType
{
    Yes,
    No,
    Cancel,
}

public enum AlertType
{
    SimpleMessage,
    YesMessage,
    YesOrNoMessage,
    PopUp,
}

public class AlertManager : MonoSingleton<AlertManager>
{
    AlertType _alertPanelType;                      // ��ʾ����
    string _alertTips;                              // ��ʾ˵��
    string _alertYesBtnText;                        // ��ʾȷ����ť�Զ�������
    string _alertNoBtnText;                         // ��ʾȡ����ť�Զ�������
    bool _cancelAsNo;                               // ȷ����ȡ����ʾ���հ��������Ƿ���Ч��Ĭ�ϵ���հ������൱�ڵ��ȡ����ť��
    Action<bool> _callbackFunc;
    TipsPivot _tipsPivot;                           // ��ʾ��ʾλ��

    public AlertType AlertPanelType => _alertPanelType;
    public string AlertTips => _alertTips;
    public string AlertYesBtnText => _alertYesBtnText;
    public string AlertNoBtnText => _alertNoBtnText;
    public bool BackCanClick => _cancelAsNo;
    public TipsPivot Tipspovot => _tipsPivot;

    public void OpenYesOrNoPanel(string tips, Action<bool> callback = null, string btnYesText = null, string btnNoText = null, bool backCanClick = true)
    {
        OpenAlertPanel(AlertType.YesOrNoMessage, callback, tips, btnYesText, btnNoText, backCanClick);
    }

    public void OpenYesPanel(string tips, Action<bool> callback = null, string btnYesText = null, bool backClick = true)
    {
        OpenAlertPanel(AlertType.YesMessage, callback, tips, btnYesText, null, backClick);
    }

    public void OpenSimpleTips(string tips, string btnText = null)
    {
        OpenAlertPanel(AlertType.SimpleMessage, null, tips, btnText, null, true);
    }

    public void ExecuteCallback(AlertClickType type)
    {
        if (_callbackFunc != null)
        {
            if (!_cancelAsNo && type == AlertClickType.Cancel)
            {
                _callbackFunc = null;
            }
            else
            {
                var tmpCallBack = _callbackFunc;
                _callbackFunc = null;
                tmpCallBack(type == AlertClickType.Yes ? true : false);
            }
        }
    }

    void OpenAlertPanel(AlertType type, Action<bool> callback, string tips, string btnYesText, string btnNoText, bool cancelAsNo, TipsPivot pivot_ = TipsPivot.Top)
    {
        if (btnYesText == null) btnYesText = "ȷ��";  //�ȴ����԰�
        if (btnNoText == null) btnNoText = "ȡ��";   //�ȴ����԰�


        var uiMgr = UIManager.Instance;
        if (uiMgr.IsPanelOpend(PanelType.AlertPanel))
        {
            uiMgr.ClosePanel(PanelType.AlertPanel);
        }

        _alertPanelType = type;
        _alertTips = tips;
        _alertYesBtnText = btnYesText;
        _alertNoBtnText = btnNoText;
        _cancelAsNo = cancelAsNo;
        _callbackFunc = callback;
        _tipsPivot = pivot_;
        uiMgr.OpenPanel(PanelType.AlertPanel);
    }

    /// <summary>
    ///  ��ʾȫ����ʾ
    /// </summary>
    /// <param name="tips">��ʾ����</param>
    /// <param name="offx">ƫ��ֵx</param>
    /// <param name="offy">ƫ��ֵy</param>
    public void ShowPopUpTips(string tips, TipsPivot pivot_ = TipsPivot.Top)
    {
        OpenAlertPanel(AlertType.PopUp, null, tips, null, null, false, pivot_);
    }
}

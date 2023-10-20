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
    AlertType _alertPanelType;                      // 提示类型
    string _alertTips;                              // 提示说明
    string _alertYesBtnText;                        // 提示确定按钮自定义文字
    string _alertNoBtnText;                         // 提示取消按钮自定义文字
    bool _cancelAsNo;                               // 确定与取消提示面板空白区域点击是否有效（默认点击空白区域相当于点击取消按钮）
    Action<bool> _callbackFunc;
    TipsPivot _tipsPivot;                           // 提示显示位置

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
        if (btnYesText == null) btnYesText = "确定";  //等待语言包
        if (btnNoText == null) btnNoText = "取消";   //等待语言包


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
    ///  显示全局提示
    /// </summary>
    /// <param name="tips">显示内容</param>
    /// <param name="offx">偏移值x</param>
    /// <param name="offy">偏移值y</param>
    public void ShowPopUpTips(string tips, TipsPivot pivot_ = TipsPivot.Top)
    {
        OpenAlertPanel(AlertType.PopUp, null, tips, null, null, false, pivot_);
    }
}

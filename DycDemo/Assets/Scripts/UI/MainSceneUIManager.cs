using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SystemPanelConfig
{
    public SystemType systemType;
    public PanelType panel;
    public bool isExclusive;
}

//基于uimananger 主要 负责 各个功能模块的打开关闭  后续如果有 多背景的时候 也是用这个脚本处理
public class MainSceneUIManager : MonoSingleton<MainSceneUIManager>
{
    public List<SystemPanelConfig> systemMainPanelConfigList;
    Dictionary<SystemType, SystemPanelConfig> systemMainPanelConfig = new Dictionary<SystemType, SystemPanelConfig>();
    SystemUnlockManager unlockMgr;
    SystemUnlockManager UnlockMgr
    {
        get
        {
            if (unlockMgr == null)
                unlockMgr = GameManager.Instance.GetManager<SystemUnlockManager>();
            return unlockMgr;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        for (var i = 0; i < systemMainPanelConfigList.Count; ++i)
        {
            var config = systemMainPanelConfigList[i];
#if UNITY_EDITOR
            if (systemMainPanelConfig.ContainsKey(config.systemType))
            {
                LogUtil.LogErrorFormat("system main panel {0}:{1} already exist!", config.systemType.ToString(), config.panel.ToString());
                continue;
            }
#endif
            systemMainPanelConfig.Add(config.systemType, config);
        }

        systemMainPanelConfigList = null;

    }

    public void OpenTargetSystem(SystemType system_, object params_ = null)
    {
        if (!UnlockMgr.IsSystemUnlock(system_))
        {
            AlertManager.Instance.OpenSimpleTips("系统还未开放!");
            return;
        }

        SystemPanelConfig config;
        if (!systemMainPanelConfig.TryGetValue(system_, out config))
        {
            LogUtil.LogWarningFormat("system {0} default panle config not exists!", system_.ToString());
            return;
        }

        openTargetSystemImple(config, params_);
    }

    private void openTargetSystemImple(SystemPanelConfig config, object params_ = null)
    {
        //背景更换   音乐更换 todo
        UIManager.Instance.OpenPanel(config.panel, params_);
    }

}

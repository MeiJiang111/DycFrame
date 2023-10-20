using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SystemUnlockInfo
{
    public SystemType System;
    public bool Open;
}
//目前先本地维护
public class SystemUnlockManager : GameSystem
{
    public event Action<SystemType, bool> SystemUnlockStateChangEvent;

    Dictionary<SystemType, SystemUnlockInfo> systemInfo;

    public SystemUnlockManager()
    {
        systemInfo = new Dictionary<SystemType, SystemUnlockInfo>();
    }

    public override void OnLogin(bool init_)
    {
        systemInfo.Clear();
    }


    public void UpdateSystemUnLockState(SystemType system_, bool unlock_)
    {
        if (!systemInfo.ContainsKey(system_))
        {
            systemInfo.Add(system_, new SystemUnlockInfo() { System = system_, Open = false });
        }
        systemInfo[system_].Open = unlock_;
        SystemUnlockStateChangEvent?.Invoke(system_, unlock_);
    }

    public bool IsSystemUnlock(SystemType system_)
    {

        if (system_ == SystemType.None) return true;

        SystemUnlockInfo _info;
        if (systemInfo.TryGetValue(system_, out _info))
        {
            return _info.Open;
        }
        LogUtil.LogWarningFormat("can not found System  {0}", system_.ToString());
        return false;
    }
}

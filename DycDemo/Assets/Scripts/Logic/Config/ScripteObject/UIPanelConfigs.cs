using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// UI√Ê∞Â≈‰÷√
public class UIPanelConfigs : ScriptableObject
{
    [SerializeField]
    public List<UIPanelStruct> panelCfgList = new List<UIPanelStruct>();

    public void Add(UIPanelStruct data)
    {
        if (IsExists(data.type))
        {
            Debug.LogErrorFormat("Panel Type {0}-->{1} is exists! can not add more!", data.type.ToString(), data.name);
            return;
        }
        panelCfgList.Add(data);
    }

    public bool IsExists(string name, PanelTypeTest type)
    {
        foreach (var item in panelCfgList)
        {
            if (item.type == type && item.name == name)
            {
                return true;
            }
        }
        return false;
    }

    bool IsExists(PanelTypeTest type_)
    {
        foreach (var item in panelCfgList)
        {
            if (item.type == type_)
            {
                return true;
            } 
        }
        return false;
    }

}

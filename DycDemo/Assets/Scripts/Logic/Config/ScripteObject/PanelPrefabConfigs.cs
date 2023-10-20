using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPrefabConfigs : ScriptableObject
{
    [SerializeField]
    public List<PanelPrefabConfig> configLis = new List<PanelPrefabConfig>();

    public void Add(PanelPrefabConfig panelPrefabConfig_)
    {
        if (IsExists(panelPrefabConfig_.type))
        {
            Debug.LogErrorFormat("Panel Type {0}-->{1} is exists! can not add more!", panelPrefabConfig_.type.ToString(), panelPrefabConfig_.name);
            return;
        }
        configLis.Add(panelPrefabConfig_);
    }

    public bool IsExists(string prefabName_, PanelGroup group_, PanelType panelType_)
    {
        foreach (var item in configLis)
        {
            if (item.type == panelType_ && item.name == prefabName_ && item.group == group_)
            {
                return true;
            }
                
        }
        return false;
    }

    bool IsExists(PanelType type_)
    {
        foreach (var item in configLis)
        {
            if (item.type == type_)
                return true;
        }
        return false;
    }
}

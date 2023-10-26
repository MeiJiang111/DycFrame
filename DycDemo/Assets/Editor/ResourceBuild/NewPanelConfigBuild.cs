using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPanelConfigBuild
{
    public struct PanelPrefab
    {
        public string prefab;
        public PanelTypeTest panelType;
        public bool isResident;

        public PanelPrefab(string prefab,PanelTypeTest panelType, bool isResident)
        {
            this.prefab = prefab;
            this.panelType = panelType;
            this.isResident = isResident;
        }
    }

    static UIPanelConfigs  uIPanelConfigs; 
    
    public static void Build()
    {
        uIPanelConfigs = ScriptableObject.CreateInstance<UIPanelConfigs>();
        foreach (var item in EditorPath.PANEL_PATHS)
        {
            var prefabs = CollectPanel(item);
            if (prefabs.Count == 0)
            {
                Debug.LogWarningFormat("Failed to collect any panel prefab from: {0}", item);
            }
            else
            {
                AddPanelPrefab(prefabs);
            }
        }

        AssetDatabase.CreateAsset(uIPanelConfigs, EditorPath.UI_PANEL_CONFIG_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static List<UIPanelInit> CollectPanel(string path)
    {
        var panelList = new List<UIPanelInit>();
        var prefabs = EditorHelpers.CollectAsset<Transform>(path);

        foreach (var prefab in prefabs)
        {
            var panel = prefab.GetComponent<UIPanelInit>();
            if (panel != null)
            {
                panelList.Add(panel);
            }
        }
        return panelList;
    }

    static void AddPanelPrefab(List<UIPanelInit> panelList)
    {
        var panelInfoList = new List<PanelPrefab>();
        foreach (var item in panelList)
        {
            var parent = item.transform.parent;
            var prefabName = parent != null ? parent.name : item.name;

            if (IsExistPanelPrefab(prefabName, item.type)) 
            {
                continue;
            }
            
            var panelInfo = new PanelPrefab(item.name,item.type, item.isResident);
            panelInfoList.Add(panelInfo);
        }
        AddPanelConfigs(panelInfoList);
    }

    static void AddPanelConfigs(List<PanelPrefab> panelInfoList)
    {
        foreach (var panelInfo in panelInfoList)
        {
            var panelType = panelInfo.panelType;
            var panelName = panelInfo.prefab;

            if (panelType == PanelTypeTest.None)
            {
                Debug.LogWarning(string.Format("Panel: {0}' type not set!", panelName));
                continue;
            }
            uIPanelConfigs.Add(new UIPanelStruct() {name = panelName, type = panelType, isResident = panelInfo.isResident });
        }
    }

    static bool IsExistPanelPrefab(string name ,PanelTypeTest panelType)
    {
        return uIPanelConfigs.IsExists(name,panelType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PanelPrefabConfigBuild 
{
    public struct PanelPrefab
    {
        public string prefab;
        public PanelType panelType;
        public SceneType panelScene;
        public PanelGroup panelGroup;
        public bool isResident;

        public PanelPrefab(string prefab, PanelType panelType, SceneType sence, PanelGroup group, bool isResident)
        {
            this.prefab = prefab;
            this.panelType = panelType;
            this.panelScene = sence;
            this.panelGroup = group;
            this.isResident = isResident;
        }
    }

 
    static PanelPrefabConfigs panelPrefabConfigs;

    public static void Build()
    {
        panelPrefabConfigs = ScriptableObject.CreateInstance<PanelPrefabConfigs>();
        
        foreach (var item in EditorPath.PANEL_PATHS)
        {
            var prefabs = CollectPanel(item);
            if (prefabs.Count == 0)
            {
                Debug.LogWarningFormat("Failed to collect any panel prefab from: {0}" , item);
            }
            else
            {
                AddPanelPrefab(prefabs);
            }
        }
        AssetDatabase.CreateAsset(panelPrefabConfigs, EditorPath.PANEL_CONFIG_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static List<OpenClosePanel> CollectPanel(string path)
    {
        var panelList = new List<OpenClosePanel>();
        var prefabs = EditorHelpers.CollectAsset<Transform>(path);
        foreach (var item in prefabs)
        {
            var panel = item.GetComponent<OpenClosePanel>();
            if (panel != null)
            {
                panelList.Add(panel);
            }
            else
            {
                if (item.childCount > 0)
                {
                    for (var i = 0; i < item.childCount; ++i)
                    {
                        panel = item.GetChild(i).GetComponent<OpenClosePanel>();
                        if (panel != null && panel.type != PanelType.None)
                        {
                            panelList.Add(panel);
                        }
                    }
                }
            }
        }
        return panelList;
    }


    static void AddPanelPrefab(List<OpenClosePanel> panelList)
    {
        var panelInfoList = new List<PanelPrefab>();
        foreach (var openClosePanel in panelList)
        {
            var parent = openClosePanel.transform.parent;
            var prefabName = parent != null ? parent.name : openClosePanel.name;
            if (IsExistPanelPrefab(prefabName, openClosePanel.group, openClosePanel.type))
                continue;
            var panelInfo = new PanelPrefab(prefabName, openClosePanel.type, openClosePanel.scene, openClosePanel.group,openClosePanel.isResident);
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

            if (panelType == PanelType.None)
            {
                Debug.LogWarning(string.Format("Panel: {0}' type not set!", panelName));
                continue;
            }
          
            panelPrefabConfigs.Add(new PanelPrefabConfig() { group = panelInfo.panelGroup, type = panelType, scene = panelInfo.panelScene, name = panelName, isResident = panelInfo.isResident });
        }
    }

    static bool IsExistPanelPrefab(string prefabName, PanelGroup group, PanelType panelType)
    {
        return panelPrefabConfigs.IsExists(prefabName, group, panelType);
    }
}

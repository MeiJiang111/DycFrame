﻿using UnityEditor;

public class ResourceToolMenu 
{
    [MenuItem("资源管理/生成Excel配置", false, 1)]
    public static void BuildExcelConfig()
    {
        CreateConfigRes.CreateRes();
        AssetDatabase.Refresh();
    }

    [MenuItem("资源管理/构建所有资源配置(AB)", false, 2)]
    public static void BuildResourcePathObj()
    {
       AddressableGroupBuild.BuildResourcePathObjImpl();
    }

    [MenuItem("资源管理/构建UI面板配置", false, 3)]
    public static void BuildUIPanelConfig()
    {
        PanelPrefabConfigBuild.Build();
    }

    [MenuItem("资源管理/构建海岛地图(Test)", false, 4)]
    public static void BuildGridsConfig()
    {
        //var obj = ScriptableObject.CreateInstance<GridsConfig>();
        //AssetDatabase.CreateAsset(obj, EditorPath.TOWN_GRIDS_CONFIG_PATH);
        //AssetDatabase.Refresh();
    }
}

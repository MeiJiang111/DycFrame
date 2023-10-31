using UnityEditor;

public class ResourceToolMenu 
{
    [MenuItem("AB/生成Excel配置", false, 1)]
    public static void BuildExcelConfig()
    {
        AssetDatabase.Refresh();
    }

    [MenuItem("AB/构建所有资源配置(AB)", false, 2)]
    public static void BuildResourcePathObj()
    {
       AddressableGroupBuild.BuildResourcePathObjImpl();
    }

    [MenuItem("AB/构建UI面板配置", false, 3)]
    public static void BuildUIPanelConfig()
    {
        //PanelPrefabConfigBuild.Build();
    }

    [MenuItem("AB/构建海岛地图(Test)", false, 4)]
    public static void BuildGridsConfig()
    {
        //var obj = ScriptableObject.CreateInstance<GridsConfig>();
        //AssetDatabase.CreateAsset(obj, EditorPath.TOWN_GRIDS_CONFIG_PATH);
        //AssetDatabase.Refresh();
    }

    [MenuItem("AB/构建UI面板配置(New)", false, 5)]
    public static void BuildUIConfig()
    {
        NewPanelConfigBuild.Build();
    }
}

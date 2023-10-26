using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static Vector3 InvaildV3 = new Vector3(-999, -999, -999);
    public static Vector2 InvaildV2 = new Vector3(-999, -999);
    public static List<int> PierConfigIds = new List<int>() {210001, 210002};

    public const float TOURIST_SPAWN_FREQUENCY = 1f;

    ///Prefab Name
    public const string LOGIN_LEVEL_NAME = "Login";
    public const string LOGINPANEL = "LoginPanel";

    public const string MAIN_LEVEL_NAME = "Main";
    public const string SIGLE_GRID_PREFAB = "SingleGround";
   
    public const string BOAT_EFFECT = "E_Chuan";
    public const string FISHING_BOAT = "E_YuChuan";
    public const string FISHING_EFFECT = "E_Fish";
    public const string YOULUN = "E_YouLun";
    public const string SINGLE_SEA_GROUND = "SingleGround";
    public const string START_BUILD_EFFECT = "E_JianZaoZhong";
    public const string BUILD_BUILDING_FINISH_EFFECT = "E_wancheng";
    public const string BUILD_LEVELUP_FINISH_EFFECT = "E_shengji";

    public const string PANLE_PREFAB_CONFIG = "PanelPrefabsConfig";
    public const string TOWN_GRIDS_CONFIG = "IslandGridsConfig";
    public const string TOWN_BUILDING_CONFIG = "IslandBuildsConfig";
    public const string WEATHER_CONFIG = "WeatherConfig";
    public const string CHARACTER_CONFIG = "CharacterConfigs";

    public const string Clone_Str = "(Clone)";


    public const int ONE_MINUTE = 60;
    public const int ONE_HOUR = ONE_MINUTE * 60;
    public const int ONE_DAY = ONE_HOUR * 24;

    static int index = 0;
    public static int Index { get { index++; return index; } }

#if UNITY_EDITOR
    public static string CacheFileDir = Application.dataPath.Replace("Assets", "GameCache/");
#else
    public static string CacheFileDir = Application.persistentDataPath + "/GameCache/";
#endif
    public const string CacheFileSuffix = ".txt";


    //public static SimplePool<SingleWorkData> WorkPool = new SimplePool<SingleWorkData>();


    public static int GetGridUuid(int x_, int y_)
    {
        return x_ * 100 + y_;
    }
    public static void GetGridXYByUuid(int uuid_, out int x_, out int y_)
    {
        x_ = uuid_ % 100;
        y_ = uuid_ - x_;
        return;
    }
}

public class Tags
{
    public const string Main_UI_TAG = "UIRoot";
}

public class Layers
{
    public const string BUILDING_LAYER = "Build";
    public const string VISUAL_BUILDING_LAYER = "VBuild";
    public const string GROUND_LAYER = "Ground";
    public const string TOURISTER_LAYER = "Tourist";
    public const string LOOK_TOURISTER_LAYER = "LookTarget";

}

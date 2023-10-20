﻿
using System.Collections.Generic;


public class EditorPath
{

    public const string BUILD_RES_ROOT = "Assets/ResResources/";
    public const string LocalFistGroup = "GameUpdate";

    public static List<string> FirstAssetsPaths = new List<string>
    {
        BUILD_RES_ROOT+ "Atlas/GameUpdate/bg1.jpg",
        BUILD_RES_ROOT+ "Atlas/GameUpdate/white.png",
        BUILD_RES_ROOT + "Prefabs/UI/Update/UpdatePanel.prefab",
    };


    public static string[] PANEL_PATHS = new string[]{
        "Assets/ResResources/Prefabs/UI/Global",
        "Assets/ResResources/Prefabs/UI/Login",
        "Assets/ResResources/Prefabs/UI/Main/Panel",
    };

    public static string PANEL_CONFIG_PATH = "Assets/ResResources/Config/Scripteobject/PanelPrefabsConfig.asset";
    public static string TOWN_GRIDS_CONFIG_PATH = "Assets/ResResources/Config/Scripteobject/IslandGridsConfig.asset";
    public static string TOWN_BUILDS_CONFIG_PATH = "Assets/ResResources/Config/Scripteobject/IslandBuildsConfig.asset";

    public const string BUILD_PATH = BUILD_RES_ROOT + "Prefabs/Build/";
    public const string BUILD_OBSTACLE_PATH = BUILD_RES_ROOT + "Prefabs/Environment/Obstacle/";
}

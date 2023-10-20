
/// <summary>
/// Enum UI面板类型
/// </summary>
public enum PanelType
{
    None = 0,                         //无效值
    LoginPanel = 1,                   //登录面板
    LoadingPanel = 2,
    MainPanel = 3,                    //主面板
    PlayerPanel = 4,                  //角色信息面板
    BuildMainPanel = 5,               //建造界面
    DavigationPanel = 6,              //航海系统
    TreasurePanel = 7,                //宝藏界面
    FisheryPanel = 8,                 //渔业界面
    BuildDetalPanel = 9,              //建筑详情
    TouristInteractionPanel = 10,     //游客互动
    SigninPanel = 11,                 //签到面板
    ExchangePanel = 12,               //兑换面板
    OfflineRevenuePanel = 13,         //离线收益
    WishingTreePanel = 14,            //许愿树
    BlankMainPanel = 15,              //银行主界面
    StructurePanel = 899,             //建造信息界面
    AlertPanel = 900,                 //全局弹窗面板
    AsyncMaskPanel = 901,             //异步加载面板
    RewardPanel = 902,                //通用奖励界面
    GameUpdatePanel = 999,            //更新面板
}

public enum SceneType
{
    None = 0,
    Login = 1,
    Main = 2,
    All = 10,
}

/// <summary>
/// 面板所属组
/// </summary>
public enum PanelGroup
{
    None = 0,                //无效值
    FullScreen = 1,          //全屏面板
    HalfScreen = 2,          //非全屏的大面板
    Dialog = 3,              //UI跟随的显示位置
    PopInfo = 4,             //显示类
    Global = 5,              //加载节点 最高
}


[System.Serializable]
public struct PanelPrefabConfig
{
    public PanelGroup group;
    public PanelType type;
    public SceneType scene;
    public string name;
    public bool isResident;           //是否持久，
    public bool preloading;           //是否预加载
}

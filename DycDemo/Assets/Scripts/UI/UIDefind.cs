
/// <summary>
/// Enum UI�������
/// </summary>
public enum PanelType
{
    None = 0,                         //��Чֵ
    LoginPanel = 1,                   //��¼���
    LoadingPanel = 2,
    MainPanel = 3,                    //�����
    PlayerPanel = 4,                  //��ɫ��Ϣ���
    BuildMainPanel = 5,               //�������
    DavigationPanel = 6,              //����ϵͳ
    TreasurePanel = 7,                //���ؽ���
    FisheryPanel = 8,                 //��ҵ����
    BuildDetalPanel = 9,              //��������
    TouristInteractionPanel = 10,     //�οͻ���
    SigninPanel = 11,                 //ǩ�����
    ExchangePanel = 12,               //�һ����
    OfflineRevenuePanel = 13,         //��������
    WishingTreePanel = 14,            //��Ը��
    BlankMainPanel = 15,              //����������
    StructurePanel = 899,             //������Ϣ����
    AlertPanel = 900,                 //ȫ�ֵ������
    AsyncMaskPanel = 901,             //�첽�������
    RewardPanel = 902,                //ͨ�ý�������
    GameUpdatePanel = 999,            //�������
}

public enum SceneType
{
    None = 0,
    Login = 1,
    Main = 2,
    All = 10,
}

/// <summary>
/// ���������
/// </summary>
public enum PanelGroup
{
    None = 0,                //��Чֵ
    FullScreen = 1,          //ȫ�����
    HalfScreen = 2,          //��ȫ���Ĵ����
    Dialog = 3,              //UI�������ʾλ��
    PopInfo = 4,             //��ʾ��
    Global = 5,              //���ؽڵ� ���
}


[System.Serializable]
public struct PanelPrefabConfig
{
    public PanelGroup group;
    public PanelType type;
    public SceneType scene;
    public string name;
    public bool isResident;           //�Ƿ�־ã�
    public bool preloading;           //�Ƿ�Ԥ����
}

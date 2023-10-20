using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class UIManager : MonoSingleton<UIManager>
{
    public Action<PanelType> PanelStartCloseEvent;
    public Action<PanelType> PanelStartOpenEvent;
    public Action<PanelType> PanelOpenedFinishEvent;
    public Action<PanelType> PanelClosedFinishEvent;

    public Transform fullScreenRoot;
    public Transform halfScreenRoot;
    public Transform dialogRoot;
    public Transform popInfoRoot;
    public Transform GlobalRoot;

    public Camera UiCamera;
    public Material desaturateMaterial;

    private SceneType curSceneType_;
    public SceneType CurUISceneType
    {
        get
        {
            return curSceneType_;
        }

        set
        {
            curSceneType_ = value;
            var types = cachePanels.Keys.ToArray();
            for (int i = types.Length - 1; i >= 0; i--)
            {
                var panleType = types[i];
                var _config = GetPanelConfig(panleType);
                if (!_config.isResident ||
                    (_config.scene != curSceneType_ && _config.scene != SceneType.All))  //非持久化 直接卸载掉
                {
                    var openclose = cachePanels[panleType];
                    cachePanels.Remove(panleType);

                    ResourceManager.Instance.DestroyInstance(openclose.gameObject);
                    LogUtil.LogFormat("destroy panel {0}", panleType);
                }
            }

        }
    }

    private PanelPrefabConfig defaultPanelPrefabConfig = new PanelPrefabConfig() { name = string.Empty, isResident = false };

    Dictionary<PanelType, PanelPrefabConfig> _panelPrefabConfigDict = new Dictionary<PanelType, PanelPrefabConfig>();
   
    public List<PanelType> preLoadingPanels;

    Dictionary<PanelType, OpenClosePanel> cachePanels = new Dictionary<PanelType, OpenClosePanel>();
    List<OpenClosePanel> curOpendPanel = new List<OpenClosePanel>();
    List<OpenClosePanel> tempList = new List<OpenClosePanel>();
   
    int _waiteCount;
    public bool HasWaite => _waiteCount > 0;

    public float CanvasOffset { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        if (fullScreenRoot == null || halfScreenRoot == null || dialogRoot == null || popInfoRoot == null)
        {
            LogUtil.LogError("UIManage Something Bind Null!");
            return;
        }
        _waiteCount = 0;
    }

    public void RegisterListener()
    {
        //var levelMgr = LevelManager.Instance;
        //levelMgr.StartLoadingNewLevelEvent += OnLoadNewLevel;
        //levelMgr.LevelLoadedEvent += OnLevelLoaded;
        //levelMgr.LevelPreStartEvent += OnLevelPreStart;

        //UiCamera = CameraController.Instance.uiCamera;
    }


    void CalculateCanvasOffset()
    {
        CanvasScaler canvasScaler = transform.GetComponent<CanvasScaler>();
        //实际画布的宽高
        float resolutionX = canvasScaler.referenceResolution.x;
        float resolutionY = canvasScaler.referenceResolution.y;
        //计算缩放比
        CanvasOffset = (Screen.width / resolutionX) * (1 - canvasScaler.matchWidthOrHeight) + (Screen.height / resolutionY) * canvasScaler.matchWidthOrHeight;
    }

    /// <summary>
    /// Panel Config配置加载
    /// </summary>
    /// <param name="configs"></param>
    public void OnPanleConfigLoaded(PanelPrefabConfigs configs)
    {
        var list = configs.configLis;
        for (var i = 0; i < list.Count; ++i)
        {
            var panelPrefabConfig = list[i];

            if (_panelPrefabConfigDict.ContainsKey(panelPrefabConfig.type))
            {
#if UNITY_EDITOR
                LogUtil.LogErrorFormat("Panel prefab {0}:{1} already exist!", panelPrefabConfig.type, panelPrefabConfig.name);
#endif
                continue;
            }
            _panelPrefabConfigDict.Add(panelPrefabConfig.type, panelPrefabConfig);
        }

        ResourceManager.Instance.UnloadAsset<PanelPrefabConfigs>(configs);
    }


    private void OnLoadNewLevel(string obj)
    {
        CloseAllPanelExceptGlobal();
    }

    private void OnLevelPreStart()
    {
        CalculateCanvasOffset();
    }

    //private void OnLevelLoaded()
    //{
    //    LevelManager.Instance.PauseLevelStart();
    //    StartCoroutine(LevelLoadedImple());
    //}

    //IEnumerator LevelLoadedImple()
    //{
    //    yield return null;
    //    AsyncLoadPreLoadingPanels(_curSceneType);
    //    while (HasWaite)
    //    {
    //        yield return null;
    //    }
    //    yield return null;
    //    LevelManager.Instance.ResumeLevelStart();
    //}



    public void RegisterPanel(OpenClosePanel openClose_)
    {
        if (cachePanels.ContainsKey(openClose_.type))
        {
            LogUtil.LogWarningFormat("{0} already registed!", openClose_.type);
            return;
        }
        cachePanels.Add(openClose_.type, openClose_);
    }

    public void RegisterUIGlobalRoot(Transform transform_)
    {
        GlobalRoot = transform_;

    }

    /// <summary>
    /// 预加载预制
    /// </summary>
    /// <param name="scene_"></param>
    public void AsyncLoadPreLoadingPanels(SceneType scene_)
    {
        foreach (var item in preLoadingPanels)
        {
            var _config = GetPanelConfig(item);
            if (string.IsNullOrEmpty(_config.name))
            {
                LogUtil.LogWarningFormat("panle type {0} in preloading List,but config is null!", item);
                continue;
            }

            if (_config.scene == scene_ && !cachePanels.ContainsKey(item))
            {
                CreatPanelAsync(_config.name, null, false, false);
            }
        }
    }

    /// <summary>
    /// 打开转圈面板
    /// </summary>
    public void OpenAsyncMaskPanel()
    {
        OpenPanel(PanelType.AsyncMaskPanel);
    }
    public void CloseAsyncMaskPanel()
    {
        var panel = GetPanel(PanelType.AsyncMaskPanel);
        if (panel != null && panel.IsOpen)
            panel.ClosePanel();
    }



    #region Panel Open OR Close

    public bool IsPanelOpend(PanelType type_)
    {
        foreach (var item in curOpendPanel)
        {
            if (item.type == type_) return true;
        }
        return false;
    }

    /// <summary>
    /// 当面板将要被打开时通知
    /// 现在处理为关闭其他面板
    /// </summary>
    /// <param name="openPanel_"></param>
    public void PanelStartOpen(OpenClosePanel openPanel_)
    {
        if (openPanel_.group == PanelGroup.FullScreen)
        {
            CloseAllPanelByGroup(openPanel_.group);
        }
        PanelStartOpenEvent?.Invoke(openPanel_.type);
    }
    public void PanelOpenFinished(OpenClosePanel openClose_)
    {
        curOpendPanel.Add(openClose_);
        PanelOpenedFinishEvent?.Invoke(openClose_.type);
    }

    public void ClosePanel(PanelType type_)
    {
        foreach (var item in curOpendPanel)
        {
            if (item.type == type_)
            {
                item.ClosePanel();
                break;
            }
        }

    }

    public void PanelStartClose(OpenClosePanel openClose_)
    {
        if (curOpendPanel.Contains(openClose_))
            curOpendPanel.Remove(openClose_);

        UpdateTopOpendedPanel(openClose_);

        PanelStartCloseEvent?.Invoke(openClose_.type);

    }

    private void UpdateTopOpendedPanel(OpenClosePanel openClose_)
    {
        //打开的面板有变动  应该要处理一下 ...todo
    }

    public void PanelCloseFinised(OpenClosePanel openClose_)
    {
        PanelClosedFinishEvent?.Invoke(openClose_.type);
        //如果不是持久化面板 直接销毁掉  后续根据内存和规则 是否销毁 现在先直接销毁 
        var _config = GetPanelConfig(openClose_.type);
        if (string.IsNullOrEmpty(_config.name))
        {
            //不应该会出现,这种情况  todo
            LogUtil.LogWarningFormat("panleClose {0}-{1} but not found panle config", openClose_.gameObject.name, openClose_.type);
            return;
        }
        if (!_config.isResident)  //非持久化 直接卸载掉
        {
            if (cachePanels.ContainsKey(openClose_.type))
            {
                cachePanels.Remove(openClose_.type);
            }
            ResourceManager.Instance.DestroyInstance(openClose_.gameObject);
            LogUtil.LogFormat("destroy panel {0}", openClose_.type);
        }
    }

    #endregion

    void CloseAllPanelExceptGlobal()
    {
        tempList.Clear();
        foreach (var item in curOpendPanel)
        {
            tempList.Add(item);
        }
        foreach (var item in tempList)
        {
            if (item.group != PanelGroup.Global)
                item.ClosePanel();
        }
    }
    void CloseAllPanelByGroup(PanelGroup group_)
    {
        tempList.Clear();
        foreach (var item in curOpendPanel)
        {
            tempList.Add(item);
        }
        foreach (var item in tempList)
        {
            if (item.group == PanelGroup.FullScreen)
                item.ClosePanel();
        }
    }

    #region 打开面板 imple
    void OpenPanel(string name_, PanelType type_, object params_ = null)
    {
        if (cachePanels.ContainsKey(type_))
        {
            OpenPanelImple(cachePanels[type_], params_);
            return;
        }
        CreatPanelAsync(name_, params_);
    }

    public void OpenPanel(PanelType panelType_, object params_ = null)
    {
        var config = GetPanelConfig(panelType_);
        if (string.IsNullOrEmpty(config.name)) return;
        OpenPanel(config.name, panelType_, params_);
    }

    void CreatPanelAsync(string name_, object params_ = null, bool openMask_ = true, bool open_ = true)
    {
        if (openMask_)
        {
            OpenAsyncMaskPanel();
            _waiteCount++;
        }
       
        if (open_)
        {
            ResourceManager.Instance.CreatInstanceAsync(name_, OnCreatAndOpenPanelSuccess, OnCreatPanelFaild, params_);
        }
        else
        {
            ResourceManager.Instance.CreatInstanceAsync(name_, OnCreatPanelSuccess, OnCreatPanelFaild, params_);

        }  
    }

    void OnCreatPanelFaild(string name_)
    {
        _waiteCount--;
    }

    void OnCreatPanelSuccess(GameObject panel_, object params_ = null)
    {
        _waiteCount--;
        var openClose = panel_.GetComponent<OpenClosePanel>();
        SetPanelParent(openClose);
    }

    void OnCreatAndOpenPanelSuccess(GameObject panel_, object params_ = null)
    {
        _waiteCount--;
        var openClose = panel_.GetComponent<OpenClosePanel>();
#if UNITY_EDITOR
        if (openClose == null)
        {
            LogUtil.LogErrorFormat("panel {0} must add OpenClosePanel Component!!", panel_.name);
            return;
        }
#endif
        SetPanelParent(openClose);
        TimerEventMgr.Instance.Add(0.01f, () =>
        {
            OpenPanelImple(openClose, params_);
        });
    }

    void OpenPanelImple(OpenClosePanel openClose_, object params_ = null)
    {
        openClose_.OpenPanel(params_);
        CloseAsyncMaskPanel();
    }

    void SetPanelParent(OpenClosePanel panel_)
    {
        switch (panel_.group)
        {
            case PanelGroup.FullScreen:
                panel_.transform.SetParent(fullScreenRoot, false);
                break;
            case PanelGroup.HalfScreen:
                panel_.transform.SetParent(halfScreenRoot, false);
                break;
            case PanelGroup.Dialog:
                panel_.transform.SetParent(dialogRoot, false);
                break;
            case PanelGroup.PopInfo:
                panel_.transform.SetParent(popInfoRoot, false);
                break;
            case PanelGroup.Global:
                panel_.transform.SetParent(GlobalRoot, false);
                break;
            default:
                break;
        }
        panel_.transform.localPosition = Vector3.zero;
        panel_.transform.localRotation = Quaternion.identity;
        panel_.transform.localScale = Vector3.one;
    }
    #endregion


    /// <summary>
    /// 面板配置
    /// </summary>
    /// <param name="panelType_"></param>
    /// <returns></returns>
    public PanelPrefabConfig GetPanelConfig(PanelType panelType_)
    {
        PanelPrefabConfig config;
        if (!_panelPrefabConfigDict.TryGetValue(panelType_, out config))
        {
            LogUtil.LogWarningFormat("panelType {0} config not found!", panelType_.ToString());
            return defaultPanelPrefabConfig;
        }
        return config;
    }

    public OpenClosePanel GetPanel(PanelType panelType_)
    {
        OpenClosePanel panel_;
        cachePanels.TryGetValue(panelType_, out panel_);
        return panel_;
    }
}

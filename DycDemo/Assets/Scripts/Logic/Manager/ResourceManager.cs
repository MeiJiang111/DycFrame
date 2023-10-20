using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;


public class ResourceManager : ASingleton<ResourceManager>
{

    public bool PreLoadFinish { get; private set; }

    public event Action<AsyncOperationHandle, Exception> AddressableErrorEvent;

    private List<AsyncOperationHandle> _curLoadingHandle;

    private Dictionary<string, AsyncOperationHandle> dic;

    protected override void Awake()
    {
        base.Awake();
        dic = new Dictionary<string, AsyncOperationHandle>();
        UnityEngine.ResourceManagement.ResourceManager.ExceptionHandler += AddressablesException;
    }

    //��ʱֻ�ǻ�ȡ������,������ô���� todo
    private void AddressablesException(AsyncOperationHandle arg1, Exception arg2)
    {
        AddressableErrorEvent?.Invoke(arg1, arg2);
        LogUtil.LogErrorFormat("[Addressable] {0}->{1}", arg1.DebugName, arg2.ToString());
    }

    
    /// <summary>
    /// 1.��������
    /// 2.С�������ϸ��ӵ�����
    /// 3.С�������Ͻ����������
    /// </summary>
    int waiteCount;
    public void PreLoads()
    {
        PreLoadFinish = false;
        waiteCount = 4;
        LoadAsset<PanelPrefabConfigs>(Global.PANLE_PREFAB_CONFIG, (config, parma) =>
        {
            UIManager.Instance.OnPanleConfigLoaded(config);
            SubWaiteCount();
        }, (name_) =>
        {
            LogUtil.LogErrorFormat("{0} Load Faild !!!", name_);
            SubWaiteCount();
        });

        /*
        var gridBuildMgr = GameManager.Instance.GetManager<GridBuildSystemManager>();

        LoadAsset<GridsConfig>(Global.TOWN_GRIDS_CONFIG, (config, parma) =>
        {
            gridBuildMgr.OnGridsConfigLoaded(config, parma);
            SubWaiteCount();
        }, (name_) =>
        {
            LogUtil.LogErrorFormat("{0} load faild!!!", name_);
            SubWaiteCount();
        });

        var weatherMgr = GameManager.Instance.GetManager<WeatherManager>();
        LoadAsset<WeatherConfigs>(Global.WEATHER_CONFIG, (config, parma) =>
        {
            weatherMgr.OnWeatherConfigLoaded(config, parma);
            SubWaiteCount();
        }, (name_) =>
        {
            LogUtil.LogErrorFormat("{0} load faild!!!", name_);
            SubWaiteCount();
        });

        var navigationMgr = GameManager.Instance.GetManager<NavigationManager>();
        LoadAsset<CharacterConfigs>(Global.CHARACTER_CONFIG, (config, parma) =>
        {
            navigationMgr.OnCharacterConfigLoaded(config, parma);
            SubWaiteCount();
        }, (name_) =>
        {
            LogUtil.LogErrorFormat("{0} load faild!!!", name_);
            SubWaiteCount();
        });
        */
    }
   
    void SubWaiteCount()
    {
        waiteCount = Mathf.Max(0, waiteCount - 1);
        if (waiteCount == 0)
        {
            PreLoadFinish = true;
        } 
    }
    
    
    public void CreatInstanceAsync(string name_, Vector3 pos_, Quaternion rotation, Transform parent = null, Action<GameObject, object> success_ = null, Action<string, object> faild_ = null, object param_ = null)
    {
        Addressables.InstantiateAsync(name_, pos_, rotation, parent).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                LogUtil.LogWarningFormat("Instance {0} failed!", name_);
                faild_?.Invoke(name_, param_);
                return;
            }

            success_?.Invoke(handle.Result as GameObject, param_);
        };

    }

    /// <summary>
    /// �첽ʵ����
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="callback_"></param>
    /// <param name="param_"></param>
    public void CreatInstanceAsync(string name_, Action<GameObject, object> success_ = null, Action<string> faild_ = null, object param_ = null)
    {
        Addressables.InstantiateAsync(name_).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                LogUtil.LogWarningFormat("Instance {0} failed!", name_);
                faild_?.Invoke(name_);
                return;
            }

            success_?.Invoke(handle.Result as GameObject, param_);
        };
    }

    /// <summary>
    /// ����һ��ʵ��
    /// </summary>
    /// <param name="gameobj_"></param>
    public void DestroyInstance(GameObject gameobj_)
    {
        Addressables.ReleaseInstance(gameobj_);
        Destroy(gameobj_);
    }

    public void LoadAsset<T>(string name_, Action<T, object> success_ = null, Action<string> faild_ = null, object param_ = null) where T : UnityEngine.Object
    {
        Addressables.LoadAssetAsync<T>(name_).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                LogUtil.LogWarningFormat("Instance {0} failed!", name_);
                faild_?.Invoke(name_);
                return;
            }

            success_?.Invoke(handle.Result as T, param_);
        };

    }

    public void UnloadAsset<T>(T asset) where T : UnityEngine.Object
    {
        Addressables.Release<T>(asset);
    }
}

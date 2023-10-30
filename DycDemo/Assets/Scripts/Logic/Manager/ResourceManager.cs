using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


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

    //暂时只是获取到报错,后续怎么处理 todo
    private void AddressablesException(AsyncOperationHandle arg1, Exception arg2)
    {
        AddressableErrorEvent?.Invoke(arg1, arg2);
        LogUtil.LogErrorFormat("[Addressable] {0}->{1}", arg1.DebugName, arg2.ToString());
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
    /// 异步实例化
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
    /// 销毁一个实例
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

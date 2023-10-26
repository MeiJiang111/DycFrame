using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Feif.UI;
using Feif.UIFramework;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;


public class LoginInitialize : MonoBehaviour
{
    [SerializeField] private GameObject stuckPanel;

    public UIPanelConfigs UIdata;
    private Dictionary<Type, AsyncOperationHandle<GameObject>> handles = new Dictionary<Type, AsyncOperationHandle<GameObject>>();

    private void Awake()
    {
        LogUtil.Log("LoginInitialize Awake ---------");
    }

    void Start()
    {
        LogUtil.Log("LoginInitialize Start ---------");

        // 注册资源请求释放事件
        UIFrame.OnAssetRequest += LoadAssetRequest;
        UIFrame.OnAssetRelease += OnAssetRelease;
        // 注册UI卡住事件
        // 加载时间超过0.5s后触发UI卡住事件
        UIFrame.StuckTime = 0.5f;
        UIFrame.OnStuckStart += OnStuckStart;
        UIFrame.OnStuckEnd += OnStuckEnd;

        var data = new LoginPanelData();
        UIFrame.Show<LoginPanel>(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<GameObject> LoadAssetRequest(Type type)
    {
        Debug.Log("type = " + type);
        var layer = UIFrame.GetLayer(type);
        Debug.Log("layer = " + layer);
        Debug.Log("type.Name = " + type.Name);
        
        if (!handles.ContainsKey(type))
        {
            // 使用Addressables异步加载资源
            var handle = Addressables.LoadAssetAsync<GameObject>(type.Name);

            // 等待异步加载完成
            await handle.Task;

            // 将资源加载操作句柄存储在字典中
            handles[type] = handle;
        }

        foreach (var item in handles)
        {
            Debug.Log("item = " + item.Key);
        }
        return handles[type].Result;
    }

    // 资源释放事件
    private void OnAssetRelease(Type type)
    {
        Debug.Log("资源释放事件" + type);
        if (handles.ContainsKey(type))
        {
            Addressables.Release(handles[type]);
        }
        handles.Clear();
    }

    private void OnStuckStart()
    {
        stuckPanel.SetActive(true);
    }

    private void OnStuckEnd()
    {
        stuckPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        handles.Clear();
    }
}

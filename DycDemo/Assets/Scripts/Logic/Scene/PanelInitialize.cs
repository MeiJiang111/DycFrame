using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Feif.UI;
using Feif.UIFramework;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;


public class PanelInitialize : MonoSingleton<PanelInitialize>
{
    [SerializeField] private GameObject stuckPanel;
    private Dictionary<Type, AsyncOperationHandle<GameObject>> handles = new Dictionary<Type, AsyncOperationHandle<GameObject>>();

  
    protected override void Awake()
    {
        base.Awake();
        LogUtil.Log("PanelInitialize Awake");
    }

    void Start()
    {
        LogUtil.Log("PanelInitialize Start");

        // ע����Դ�����ͷ��¼�
        UIFrame.OnAssetRequest += LoadAssetRequest;
        UIFrame.OnAssetRelease += OnAssetRelease;
        // ע��UI��ס�¼�
        // ����ʱ�䳬��0.5s�󴥷�UI��ס�¼�
        UIFrame.StuckTime = 0.5f;
        UIFrame.OnStuckStart += OnStuckStart;
        UIFrame.OnStuckEnd += OnStuckEnd;

        var data = new LoginPanelData();
        UIFrame.Show<LoginPanel>(data);
    }

    private async Task<GameObject> LoadAssetRequest(Type type)
    {
        var layer = UIFrame.GetLayer(type);
        Debug.Log("type = " + type);
        //Debug.Log("layer = " + layer);
        Debug.Log("type.Name = " + type.Name);
        
        if (!handles.ContainsKey(type))
        {
            // ʹ��Addressables�첽������Դ
            var handle = Addressables.LoadAssetAsync<GameObject>(type.Name);

            // �ȴ��첽�������
            await handle.Task;

            // ����Դ���ز�������洢���ֵ���
            handles[type] = handle;
        }

        //foreach (var item in handles)
        //{
        //    Debug.Log("item = " + item.Key);
        //}
        return handles[type].Result;
    }

    // ��Դ�ͷ��¼�
    private void OnAssetRelease(Type type)
    {
        Debug.Log("��Դ�ͷ��¼�" + type);
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

    public void RegisterListener()
    {

    }
}

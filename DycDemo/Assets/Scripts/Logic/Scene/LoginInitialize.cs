using Feif.UIFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Feif.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class LoginInitialize : MonoBehaviour
{
    [SerializeField] private GameObject stuckPanel;

    private Dictionary<Type, AsyncOperationHandle<GameObject>> handles = new Dictionary<Type, AsyncOperationHandle<GameObject>>();

    private void Awake()
    {
        LogUtil.Log("LoginInitialize Awake ---------");
    }

    void Start()
    {
        LogUtil.Log("LoginInitialize Start ---------");

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
        Debug.Log("type = " + type);
        var layer = UIFrame.GetLayer(type);
        Debug.Log("layer = " + layer);

        if (!handles.ContainsKey(type))
        {
            // ʹ��Addressables�첽������Դ
            var handle = Addressables.LoadAssetAsync<GameObject>(Global.LOGINPANEL);

            // �ȴ��첽�������
            await handle.Task;

            // ����Դ���ز�������洢���ֵ���
            handles[type] = handle;
        }

        foreach (var item in handles)
        {
            Debug.Log("item = " + item.Key);
        }
        return handles[type].Result;
    }

       
    // ��Դ�ͷ��¼�
    private void OnAssetRelease(Type type)
    {
        // TODO
    }

    private void OnStuckStart()
    {
        stuckPanel.SetActive(true);
    }

    private void OnStuckEnd()
    {
        stuckPanel.SetActive(false);
    }

}

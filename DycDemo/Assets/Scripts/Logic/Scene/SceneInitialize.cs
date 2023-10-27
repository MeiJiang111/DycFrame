using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AsyncPrefabInfo
{
    public string Name;
    public Vector3 Pos;
}

/// 场景初始化脚本
public class SceneInitialize : MonoBehaviour
{
    public List<AsyncPrefabInfo> asyncPrefabList;
    public string bgm;

    private void Awake()
    {
        LogUtil.Log("SceneInitialize Awake");

        SceneManager.Instance.RegisterLoadPrefabs(asyncPrefabList, CreatPrefab, CreatPrefabFaild);
        AudioSourceManager.Instance.CurBGM = bgm;
    }

    private void CreatPrefab(string name, GameObject obj, object parmas_)
    {
        LogUtil.Log("SceneInitialize CreatPrefab");
        foreach (var item in asyncPrefabList)
        {
            if (item.Name.Equals(name))
            {
                obj.name = item.Name;
                obj.transform.localPosition = item.Pos;
            }
        }
    }

    private void CreatPrefabFaild(string name_)
    {

    }
}

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
    public List<AsyncPrefabInfo> asyncPrefabs;
    public string bgm;

    private void Awake()
    {
        SceneManager.Instance.RegisterLoadPrefabs(asyncPrefabs, CreatPrefab);
        AudioSourceManager.Instance.CurBGM = bgm;
    }

    private void CreatPrefab(string name, GameObject obj, object parmas_)
    {
        LogUtil.Log("SceneInitialize CreatPrefab");

        foreach (var item in asyncPrefabs)
        {
            if (item.Name.Equals(name))
            {
                obj.name = item.Name;
                obj.transform.localPosition = item.Pos;
            }
        }
    }
}

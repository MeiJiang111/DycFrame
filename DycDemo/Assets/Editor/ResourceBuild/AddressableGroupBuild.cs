using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;

public class AddressableGroupBuild
{
    public struct AssetInfo
    {
        public string FilePath;
        public string GroupName;
        public string FileName;
        public string LabelName;

        public AssetInfo(string path_)
        {
            FilePath = path_;
            FileName = Path.GetFileNameWithoutExtension(path_);

            var tempPath = path_.Replace(EditorPath.BUILD_RES_ROOT, "");
            //LogUtil.Log("tempPath" + tempPath);
            var index = tempPath.IndexOf('/');
            if (index < 0)
            {
                Debug.Log(path_);
            }
            GroupName = tempPath.Substring(0, tempPath.IndexOf('/'));

            LabelName = Path.GetDirectoryName(path_).Replace("\\", "/").Replace(EditorPath.BUILD_RES_ROOT, "").Replace("/", "_");
        }
    }

    static AddressableAssetSettings setting 
    { 
        get { return AddressableAssetSettingsDefaultObject.Settings; } 
    }
   

    public static void BuildResourcePathObjImpl()
    {
        var assetsList = new List<AssetInfo>();
        
        var files = Directory.GetFiles(EditorPath.BUILD_RES_ROOT, "*", SearchOption.AllDirectories);
        LogUtil.Log("files" + files);

        var total = files.Length;
        var index = 1f;

        foreach (var file in files)
        {
            if (file.Contains(".meta") || file.Contains(".exr") || file.Contains("tpsheet") || file.Contains(".DS_Store") || file.Contains("/Template") ||  file.Contains(".otf") || file.Contains(".ttf"))
            {
                index++;
                continue;
            }
       
            EditorUtility.DisplayProgressBar("搜集资源...", "", index / total);
            var filePath = file.Replace('\\', '/');
            var _assetInfo = new AssetInfo(filePath);
            assetsList.Add(_assetInfo);
            index++;
        }

        EditorUtility.ClearProgressBar();
        CreatAllGroups(assetsList);
        MoveFirstAssetToLocalGroup();
        EditorUtility.DisplayDialog("提示", string.Format("建立Group索引完毕,资源总数{0}",assetsList.Count), "确定");
    }

    static void CreatAllGroups(List<AssetInfo> list_)
    {
        var total = list_.Count;
        var index = 1f;
        foreach (var item in list_)
        {
            EditorUtility.DisplayProgressBar("建立Group", item.FileName, index / total);
            var group = GetGroup(item.GroupName);
            string guid = AssetDatabase.AssetPathToGUID(item.FilePath);         //要打包的资产条目   将路径转成guid
            
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group, false, true);          //要打包的资产条目 会将要打包的路径移动到group节点下
            entry.address = item.FileName;
            entry.SetLabel(item.LabelName, true, true, false);
            index++;
        }
        EditorUtility.ClearProgressBar();
    }

    static AddressableAssetGroup GetGroup(string groupName_)
    {
        var schemas = setting.DefaultGroup.Schemas[0];
        AddressableAssetGroup group = setting.FindGroup(groupName_);
        if (group != null) return group;
        return setting.CreateGroup(groupName_, false, false, false, new List<AddressableAssetGroupSchema> { setting.DefaultGroup.Schemas[0], setting.DefaultGroup.Schemas[1] });
    }

    static void MoveFirstAssetToLocalGroup()
    {
        var total = EditorPath.FirstAssetsPaths.Count;
        var index = 1f;
        foreach (var item in EditorPath.FirstAssetsPaths)
        {
            EditorUtility.DisplayProgressBar("移动首包资源...", "", index / total);
            var group = GetGroup(EditorPath.LocalFistGroup);
            string guid = AssetDatabase.AssetPathToGUID(item);
            Debug.Log(item);
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group, false, true);
            entry.address = Path.GetFileNameWithoutExtension(item);
            entry.SetLabel(EditorPath.LocalFistGroup, true, true, false);
            index++;
        }
        EditorUtility.ClearProgressBar();
    }
}

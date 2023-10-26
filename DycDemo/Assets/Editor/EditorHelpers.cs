using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;


class EditorHelpers
{
    // This method loads all files at a certain path and
    // returns a list of specific assets.
    public static List<T> CollectAllAsset<T>(string path) where T : Object
    {
        List<T> l = new List<T>();

        //check if directory doesn't exit
        if (!Directory.Exists(path))
        {
            return l;
        }

        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            if (file.Contains(".meta")) continue;

            var assets = AssetDatabase.LoadAllAssetsAtPath(file);
            foreach (var asset in assets)
            {
                var obj = asset as T;
                if (obj != null)
                    l.Add(obj);
            }
        }
        return l;
    }

	public static List<T> CollectAsset<T>(string path) where T : Object
	{
		List<T> l = new List<T>();

        //check if directory doesn't exit
        if (!Directory.Exists(path))
        {
            return l;
        }

	    List<string> files = new List<string>();
	    GetFiles(path, files);
		foreach (string file in files)
		{
			if (file.Contains(".meta")) continue;
			
			T asset = (T) AssetDatabase.LoadAssetAtPath(file, typeof(T));
			//if (asset == null) throw new Exception("Asset is not " + typeof(T) + ": " + files);
			if (asset != null)
				l.Add(asset);
		}
		return l;
	}

    
    /// <summary>
    /// 获得文件夹下所有的文件
    /// </summary>
    /// <param name="filePath">文件夹路径</param>
    /// <param name="files">文件列表</param>
    public  static void GetFiles(string filePath, List<string> files)
    {
        DirectoryInfo folder = new DirectoryInfo(filePath);

        string[] chldFiles = Directory.GetFiles(filePath);
        foreach (string chlFile in chldFiles)
        {
            files.Add(chlFile);
        }
        string[] chldFolders = Directory.GetDirectories(filePath);
        foreach (string chldFolder in chldFolders)
        {
            GetFiles(chldFolder, files);
        }
    }
  
    public static Object[] GetSelectedAssets<T>()
    {
        return Selection.GetFiltered(typeof(T), SelectionMode.DeepAssets);
    }
	
	public static List<string> GetSelectedPaths()
	{
		// collect all folders
		var paths = new List<string>();
		
        var objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets); 
        foreach (Object obj in objs)
        {
            var fullpath = AssetDatabase.GetAssetPath(obj);
            if (File.Exists(fullpath))  
            {   
                fullpath = Path.GetDirectoryName(fullpath); 
            }
            fullpath =  fullpath.Replace("\\", "/");
            if (!paths.Contains(fullpath))
            {
                paths.Add(fullpath);
            }
        }
        
        return paths;
    }

    public static List<string> GetSelectedObject()
    {
        var paths = new List<string>();
        var objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in objs)
        {
            var fullpath = AssetDatabase.GetAssetPath(obj);

            if (File.Exists(fullpath))
            {
                if (!paths.Contains(fullpath))
                {
                    paths.Add(fullpath);
                }
            }
        }

        return paths;
    }

    public static List<string> GetAllAssetPathEx(string rootPath)
    {
        var paths = new List<string>();

        //check if directory doesn't exit
        if (!Directory.Exists(rootPath))
        {
            return paths;
        }

        var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            if (file.Contains(".meta")) continue;
            paths.Add(file);
        }
        return paths;
    }

    public static List<string> GetAllAssetPath(string rootPath)
    {
        var paths = new List<string>();

        //check if directory doesn't exit
        if (!Directory.Exists(rootPath))
        {
            return paths;
        }

        var files = Directory.GetFiles(rootPath);

        foreach (string file in files)
        {
            if (file.Contains(".meta")) continue;
            paths.Add(file);
        }
        return paths;
    }

    public static string[] GetAllDirectories(string path)
    {
        return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
    }

    public static string[] GetDirectories(string path)
    {
        return Directory.GetDirectories(path);
    }

    public static string GetFileNameFromPath(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }
}
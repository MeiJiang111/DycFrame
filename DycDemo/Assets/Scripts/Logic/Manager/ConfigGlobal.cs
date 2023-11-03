using cfg;
using SimpleJSON;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ConfigGlobal
{
    private static Tables tables;
    public static Tables Table => tables;

    static ConfigGlobal()
    {
        tables = new Tables(LoadJson);
    }

    private static JSONNode LoadJson(string name)
    {
        var handler = Addressables.LoadAssetAsync<TextAsset>(name);
        handler.WaitForCompletion();
        return JSON.Parse(handler.Result.text);
    }
}

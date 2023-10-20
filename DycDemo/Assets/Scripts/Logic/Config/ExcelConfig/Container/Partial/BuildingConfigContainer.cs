using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BuildingConfigContainer
{
    List<BuildingConfigBean> buildList;
    Dictionary<int, List<BuildingConfigBean>> allLevelUpDic;
    /// <summary>
    /// 所有可建造的建筑物列表
    /// </summary>
    public List<BuildingConfigBean> BuildList => buildList;

    public Dictionary<int, List<BuildingConfigBean>> AllBuildLevelUpConfig;

    /// <summary>
    /// 所有可建造类型集合
    /// </summary>
    public List<int> buildTypes;
    /// <summary>
    /// 按类型分配的可建造的集合
    /// </summary>
    private Dictionary<int, List<BuildingConfigBean>> BaseBuildDic;

    public override void OnLoaded()
    {
        buildList = new List<BuildingConfigBean>();
        allLevelUpDic = new Dictionary<int, List<BuildingConfigBean>>();
        buildTypes = new List<int>();
        BaseBuildDic = new Dictionary<int, List<BuildingConfigBean>>();
        foreach (var item in dataList)
        {
            if (item.CanBuild == 1) continue;

            item.needItems = DeserializeObject<List<NeedItemData>>(item.Cost);
            if (allLevelUpDic.ContainsKey(item.BuildingId))
                allLevelUpDic[item.BuildingId].Add(item);
            else
            {
                var list = new List<BuildingConfigBean>() { item };
                allLevelUpDic.Add(item.BuildingId, list);
            }
            if (item.Level == 1)
            {
                buildList.Add(item);
                if (BaseBuildDic.ContainsKey(item.BuildingType))
                    BaseBuildDic[item.BuildingType].Add(item);
                else
                {
                    var list = new List<BuildingConfigBean>() { item };
                    BaseBuildDic.Add(item.BuildingType, list);
                }
            }

            if (!IsHaveType(item.BuildingType) && item.CanBuild == 0)
            {
                buildTypes.Add(item.BuildingType);
            }
        }
        buildList.Sort(Compare);
        buildTypes.Sort((x, y) => x.CompareTo(y));
        foreach (var item in allLevelUpDic)
        {
            var list = item.Value;
            list.Sort(CompareLevel);
        }
    }

    private bool IsHaveType(int type_)
    {
        if (buildTypes == null || buildTypes.Count == 0)
        {
            return false;
        }
        for (int i = 0; i < buildTypes.Count; i++)
        {
            if (buildTypes[i] == type_)
                return true;
        }
        return false;
    }

    public BuildingConfigBean GetBuildConfigByLevel(int buildId_, int index_)
    {
        if (!allLevelUpDic.TryGetValue(buildId_, out List<BuildingConfigBean> list_))
        {
            return null;
        }
        if (list_.Count <= index_) return null;
        return list_[index_];
    }

    public BuildingConfigBean GetDataBean(string name_)
    {
        foreach (var item in dataList)
        {
            if (item.ResourceName.Equals(name_)) return item;
        }
        LogUtil.LogWarningFormat("build base Name {0} excel config not found!", name_);
        return null;
    }

    static int Compare(BuildingConfigBean a, BuildingConfigBean b)
    {
        return a.Condition > b.Condition ? 1 : 0;
    }

    static int CompareLevel(BuildingConfigBean a, BuildingConfigBean b)
    {
        return a.Level > b.Level ? 1 : -1;
    }

    public List<BuildingConfigBean> GetCfgsByType(int type_)
    {
        if (BaseBuildDic.TryGetValue(type_, out List<BuildingConfigBean> list))
        {
            return list;
        }
        return null;
    }
}

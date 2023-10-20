using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TouristCenterLvConfigContainer
{
    private Dictionary<int, int> _max_levelDic;
    private List<int> _buildTypes;
    public override void OnLoaded()
    {
        _max_levelDic = new Dictionary<int, int>();
        _buildTypes = new List<int>();
        foreach (var bean in dataList)
        {
            bean.NeedMoney_Data = DeserializeObject<NeedItemData>(bean.NeedMoney);
            if (!_max_levelDic.ContainsKey(bean.UpType))
            {
                _max_levelDic[bean.UpType] = bean.Level;
                _buildTypes.Add(bean.UpType);
            }
            else if (_max_levelDic[bean.UpType] < bean.Level)
            {
                _max_levelDic[bean.UpType] = bean.Level;
            }
        }
    }

    public List<int> BuildTypes => _buildTypes;

    public TouristCenterLvConfigBean GetCfgByBuildTypeAndLv(int buildType_, int lv_)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].UpType == buildType_ && dataList[i].Level == lv_)
            {
                return dataList[i];
            }
        }
        return null;
    }

    public int GetMaxLvelByBuildType(int buildtype_)
    {
        if (_max_levelDic.TryGetValue(buildtype_, out int lv))
        {
            return lv;
        }
        return 0;
    }
}

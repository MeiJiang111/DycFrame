using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MineSettingConfigContainer
{
    public override void OnLoaded()
    {
        foreach (var bean in dataList)
        {
            bean.MineNeedItem = DeserializeObject<NeedItemData>(bean.MineItemCost);
            bean.BuyNeedItem = DeserializeObject<NeedItemData>(bean.BuyCost);
        }
    }
}

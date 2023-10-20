using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WishingTreeConfigContainer
{
    public override void OnLoaded()
    {
        foreach (var bean in dataList)
        {
            bean.PremiumRewards = DeserializeObject<List<NeedItemData>>(bean.GrandPrize);
            bean.OrdinaryRewards = DeserializeObject<List<NeedItemData>>(bean.CommonPrize);
        }
    }
}

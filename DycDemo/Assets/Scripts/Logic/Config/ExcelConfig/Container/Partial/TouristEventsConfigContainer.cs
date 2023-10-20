using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TouristEventsConfigContainer
{
    public override void OnLoaded()
    {
        foreach (var bean in dataList)
        {
            bean.Rewardlist = DeserializeObject<List<IntervalReward>>(bean.Rewards);
        }
    }
}

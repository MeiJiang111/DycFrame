using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalReward
{
    public int Id;
    public int Min;
    public int Max;
}

public partial class TouristEventsConfigBean
{
    public List<IntervalReward> Rewardlist;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TouristCenterConfigContainer
{
    public override void OnLoaded()
    {
        foreach (var bean in dataList)
        {
            bean.ServiceNeedItem_list = DeserializeObject<List<DockRepairData>>(bean.ServiceNeedItem);
            bean.DurabilityNeedItem_list = DeserializeObject<List<NeedItemData>>(bean.DurabilityNeedItem);
        }
    }
}

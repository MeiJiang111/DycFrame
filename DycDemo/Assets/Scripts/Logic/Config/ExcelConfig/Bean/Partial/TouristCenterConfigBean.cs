using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DockRepairData
{
    public int Id;
    public int Num;
    public int Pernum;
}

[System.Serializable]
public struct NeedItemData
{
    public int Id;
    public int Num;
}
public partial class TouristCenterConfigBean
{
    public List<DockRepairData> ServiceNeedItem_list;
    public List<NeedItemData> DurabilityNeedItem_list;
}

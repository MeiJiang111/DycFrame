using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BuildingConfigBean 
{
    public string VisualName => string.Format("V_{0}", ResourceName);
    public string EntityName => string.Format("E_{0}", ResourceName);
    public List<NeedItemData> needItems;

    public BuildType Type => (BuildType)BuildingType;
}

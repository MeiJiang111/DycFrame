using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemConfigContainer
{
    public string GetItemNameById(int id_)
    {
        var itembeam = GetDataBean(id_);
        if (itembeam != null)
        {
            return itembeam.Name;
        }
        return "null";
    }
}

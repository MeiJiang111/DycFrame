using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPanelInit : MonoBehaviour
{
    public PanelTypeTest type;
    public bool isResident = false;

    private void Awake()
    {
        if (type == PanelTypeTest.None)
        {
            LogUtil.LogWarning("curr ui name no set !!!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MineConfigContainer
{
    public int GetMineId(int value_)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (value_ < dataList[i].Probability)
            {
                return dataList[i].Id;
            }
            else
            {
                value_ = value_ - dataList[i].Probability;
            }
        }
        return dataList[0].Id;
    }
}

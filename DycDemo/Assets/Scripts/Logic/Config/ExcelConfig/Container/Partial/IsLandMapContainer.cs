using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class IsLandMapContainer
{
    public int GetGridBuildId(int x_, int y_)
    {
        var bean = GetDataBean(x_ + 1);
        if (bean == null) return -1;
        switch (y_)
        {
            case 0: return bean.Col1;
            case 1: return bean.Col2;
            case 2: return bean.Col3;
            case 3: return bean.Col4;
            case 4: return bean.Col5;
            case 5: return bean.Col6;
            case 6: return bean.Col7;
            case 7: return bean.Col8;
            case 8: return bean.Col9;
            case 9: return bean.Col10;
            case 10: return bean.Col11;
            case 11: return bean.Col12;
            case 12: return bean.Col13;
            case 13: return bean.Col14;
            default:
                LogUtil.LogErrorFormat("col {0} not exists!", y_);
                break;
        }
        return -1;
    }
}
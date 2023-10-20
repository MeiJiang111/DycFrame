using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable] 
public partial class GlobalConfigBean : BaseBean
{
    /// <summary>
    ///表格标识
    /// <summary>
    public int Id;
    /// <summary>
    ///岛内游客展示百分比
    /// <summary>
    public int VisitorShowPer;
    /// <summary>
    ///岛内游客展示人数上限值
    /// <summary>
    public int VisitorShowMax;
    /// <summary>
    ///游戏内每日重置的时间节点
    /// <summary>
    public string ResetTime;
    /// <summary>
    ///回复一点耐久度消耗金币数
    /// <summary>
    public int DurableRecoverCost;
    /// <summary>
    ///回复一点清洁值消耗金币数
    /// <summary>
    public int CleanRecoverCost;
    /// <summary>
    ///离线收益繁荣度系数
    /// <summary>
    public int OffLineProsperity;
    /// <summary>
    ///离线收益人口影响系数
    /// <summary>
    public int OffLineVisitor;
    /// <summary>
    ///离线收益最大时间(h)
    /// <summary>
    public int OffLineMaxTime;
}

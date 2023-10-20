public enum BuildLvAttType
{
    /// <summary>
    /// 收益
    /// </summary>
    Income,
    /// <summary>
    /// 清洁度
    /// </summary>
    Cleanliness,
    /// <summary>
    /// 繁荣度
    /// </summary>
    BoomUp,
    /// <summary>
    /// 幸福度
    /// </summary>
    HappinessUp,
    /// <summary>
    /// 建造时间
    /// </summary>
    ConstructionTime,
    /// <summary>
    /// 建造消耗
    /// </summary>
    NeedItem,
    /// <summary>
    /// 能量消耗
    /// </summary>
    EnergyConsumption,
    /// <summary>
    /// 能量生产
    /// </summary>
    EnergyProduction,
    /// <summary>
    /// 周期
    /// </summary>
    Cycle,
    /// <summary>
    /// 耐久度
    /// </summary>
    Durability,
    /// <summary>
    /// 容纳数量
    /// </summary>
    HoldValue,
    /// <summary>
    /// 辐射范围
    /// </summary>
    RadiationRange,
    /// <summary>
    /// 等级
    /// </summary>
    Level,
}

/// <summary>
/// 建筑物类型 1、住宅，2、娱乐，3、餐饮，4、清洁，5、功能，6、装饰
/// </summary>
public enum BuildType
{
    Invaild = 0,
    Hotel = 1,
    Rides = 2,
    Repast = 3,
    Clean = 4,
    Useful = 5,
    Decorate = 6
}
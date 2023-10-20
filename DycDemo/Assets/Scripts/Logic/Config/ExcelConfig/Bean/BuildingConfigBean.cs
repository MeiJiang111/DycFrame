using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * BuildingConfig Bean
 */
 [Serializable]
public partial class BuildingConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 建筑物名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 建筑物Id
    /// </summary>
	public  int BuildingId;

		/// <summary>
	/// 建筑物等级
    /// </summary>
	public  int Level;

		/// <summary>
	/// 建筑物资源名字
    /// </summary>
	public  string ResourceName;

		/// <summary>
	/// 图标icon
    /// </summary>
	public  string Icon;

		/// <summary>
	/// 建筑物简单介绍
    /// </summary>
	public  string Description;

		/// <summary>
	/// 建筑物类型 1、住宅，2、娱乐，3、餐饮，4、清洁，5、功能，6、装饰
    /// </summary>
	public  int BuildingType;

		/// <summary>
	/// 解锁需要的繁荣度
    /// </summary>
	public  int Condition;

		/// <summary>
	/// 是否可以建造,0可以 1不可以 不可以表示不会出现在建造列表中
    /// </summary>
	public  int CanBuild;

		/// <summary>
	/// 建造或者升级消耗（1金币 2食物 3钻石）
    /// </summary>
	public  string Cost;

		/// <summary>
	/// 影响范围
    /// </summary>
	public  int Range;

		/// <summary>
	/// 影响范围描述
    /// </summary>
	public  string RangeDesc;

		/// <summary>
	/// 幸福值增加
    /// </summary>
	public  int HappinessUp;

		/// <summary>
	/// 繁荣度增加
    /// </summary>
	public  int BoomUp;

		/// <summary>
	/// 清洁值上限
    /// </summary>
	public  int CleanValueMax;

		/// <summary>
	/// 耐久度(单个游客降低耐久度，娱乐类建筑)
    /// </summary>
	public  int BuildDurableCost;

		/// <summary>
	/// 耐久度上限值
    /// </summary>
	public  int BuildDurableMax;

		/// <summary>
	/// 娱乐建筑-门票价格/
    //住宅建筑-入住费用
    /// </summary>
	public  int TicketPrice;

		/// <summary>
	/// 游客上限/入住上限/食物库存上限
    /// </summary>
	public  int BuildTop;

		/// <summary>
	/// 食物价格
    /// </summary>
	public  int FoodPrice;

		/// <summary>
	/// 食物回复的饥饿度
    /// </summary>
	public  int FoodHunger;

		/// <summary>
	/// 游客周期影响值-产生垃圾
    /// </summary>
	public  int CycleValueRubbish;

		/// <summary>
	/// 游客周期影响值-饥饿度
    /// </summary>
	public  int CycleValueFoodCost;

		/// <summary>
	/// 游客周期影响值-产生食物
    /// </summary>
	public  int CycleValueFoodMake;

		/// <summary>
	/// 周期频率（秒）
    /// </summary>
	public  int CycleTime;

		/// <summary>
	/// 建造时间(秒）
    /// </summary>
	public  int BuildTime;

	}
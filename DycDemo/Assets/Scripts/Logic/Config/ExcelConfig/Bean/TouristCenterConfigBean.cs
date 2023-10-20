using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * TouristCenterConfig Bean
 */
 [Serializable]
public partial class TouristCenterConfigBean : BaseBean
{

		/// <summary>
	/// 唯一ID
    /// </summary>
	public  int Id;

		/// <summary>
	/// 修复游客中心需要消耗的道具
    /// </summary>
	public  string ServiceNeedItem;

		/// <summary>
	/// 默认事件发生间隔
    /// </summary>
	public  int EventInterval;

		/// <summary>
	/// 导游上限
    /// </summary>
	public  int GuideLimit;

		/// <summary>
	/// 每个导游减少事件的时间
    /// </summary>
	public  int ReduceTime;

		/// <summary>
	/// 默认耐久度
    /// </summary>
	public  int Durability;

		/// <summary>
	/// 每点耐久度需要消耗的道具
    /// </summary>
	public  string DurabilityNeedItem;

		/// <summary>
	/// 每点耐久度需要消耗的时间（秒）
    /// </summary>
	public  int DurabilityNeedTime;

		/// <summary>
	/// 每位游客每分钟消耗的食物数量
    /// </summary>
	public  int TouristFoodNum;

		/// <summary>
	/// 每次增加导游的数量
    /// </summary>
	public  int GuideAdd;

		/// <summary>
	/// 每次增加食物的数量
    /// </summary>
	public  int FoodAdd;

		/// <summary>
	/// CD时间（秒）
    /// </summary>
	public  int Cdtime;

		/// <summary>
	/// 游客接待最低食物数量
    /// </summary>
	public  int Need_minfood;

	}
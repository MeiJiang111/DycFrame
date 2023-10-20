using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * MineSettingConfig Bean
 */
 [Serializable]
public partial class MineSettingConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 初始锄头数量
    /// </summary>
	public  int HoeInit;

		/// <summary>
	/// 锄头恢复周期（s）
    /// </summary>
	public  int HoeRecovery;

		/// <summary>
	/// 锄头恢复上限
    /// </summary>
	public  int HoeRecoveryMax;

		/// <summary>
	/// 开启矿山消耗道具
    /// </summary>
	public  string MineItemCost;

		/// <summary>
	/// 购买10个消耗
    /// </summary>
	public  string BuyCost;

		/// <summary>
	/// 购买次数
    /// </summary>
	public  int BuyTimeMax;

	}
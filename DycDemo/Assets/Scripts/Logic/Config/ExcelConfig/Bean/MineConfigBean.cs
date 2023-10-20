using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * MineConfig Bean
 */
 [Serializable]
public partial class MineConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 矿山名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 获得几率（万分比）
    /// </summary>
	public  int Probability;

		/// <summary>
	/// 金币上限
    /// </summary>
	public  int CoinMax;

		/// <summary>
	/// 每次挖矿的金币掉落
    /// </summary>
	public  int[] DropCoin;

		/// <summary>
	/// 每次挖矿的物品掉落
    /// </summary>
	public  int DropItem;

	}
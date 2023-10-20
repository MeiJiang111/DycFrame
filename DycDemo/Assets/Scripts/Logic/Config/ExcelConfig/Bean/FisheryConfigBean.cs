using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * FisheryConfig Bean
 */
 [Serializable]
public partial class FisheryConfigBean : BaseBean
{

		/// <summary>
	/// 唯一ID
    /// </summary>
	public  int Id;

		/// <summary>
	/// 名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 兑换食物（数量/kg）
    /// </summary>
	public  int ExchangeFood;

		/// <summary>
	/// 普通单价
    /// </summary>
	public  int[] Price;

		/// <summary>
	/// 高级单价
    /// </summary>
	public  int AdvPrice;

		/// <summary>
	/// 出现权重
    /// </summary>
	public  int Probability;

		/// <summary>
	/// 捕鱼效率（s/kg）
    /// </summary>
	public  int Effectiveness;

	}
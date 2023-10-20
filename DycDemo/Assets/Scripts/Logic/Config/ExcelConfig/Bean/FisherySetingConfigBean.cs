using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * FisherySetingConfig Bean
 */
 [Serializable]
public partial class FisherySetingConfigBean : BaseBean
{

		/// <summary>
	/// 唯一ID
    /// </summary>
	public  int Id;

		/// <summary>
	/// 叫价间隔（秒）
    /// </summary>
	public  int BidInterval;

		/// <summary>
	/// 出现高级价格的概率(万分比)
    /// </summary>
	public  int Probability;

		/// <summary>
	/// 捕鱼上限
    /// </summary>
	public  int UpperLimit;

	}
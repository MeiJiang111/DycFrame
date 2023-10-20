using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * TouristEventsConfig Bean
 */
 [Serializable]
public partial class TouristEventsConfigBean : BaseBean
{

		/// <summary>
	/// 唯一ID
    /// </summary>
	public  int Id;

		/// <summary>
	/// 事件描述
    /// </summary>
	public  string Description;

		/// <summary>
	/// 奖励
    /// </summary>
	public  string Rewards;

		/// <summary>
	/// 事件类型(1 获取 2 消耗)
    /// </summary>
	public  int GenreType;

		/// <summary>
	/// 权重
    /// </summary>
	public  int Probability;

	}
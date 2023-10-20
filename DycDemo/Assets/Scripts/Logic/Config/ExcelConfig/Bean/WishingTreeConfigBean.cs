using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * WishingTreeConfig Bean
 */
 [Serializable]
public partial class WishingTreeConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 大奖
    /// </summary>
	public  string GrandPrize;

		/// <summary>
	/// 普通奖励
    /// </summary>
	public  string CommonPrize;

		/// <summary>
	/// 普通奖励掉落id
    /// </summary>
	public  int Drop;

		/// <summary>
	/// 进度条上限
    /// </summary>
	public  int ProgressBarMax;

		/// <summary>
	/// 进度条增加值
    /// </summary>
	public  int ProgressBarAdd;

		/// <summary>
	/// 每日许愿次数
    /// </summary>
	public  int WishTimeDay;

		/// <summary>
	/// 普通许愿倒计时(s)
    /// </summary>
	public  int WishCountDown;

	}
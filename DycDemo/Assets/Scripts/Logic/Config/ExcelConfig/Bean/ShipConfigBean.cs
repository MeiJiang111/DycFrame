using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * ShipConfig Bean
 */
 [Serializable]
public partial class ShipConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 船只名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 船只资源名字
    /// </summary>
	public  string ResourceName;

		/// <summary>
	/// 船只描述
    /// </summary>
	public  string Desc;

		/// <summary>
	/// 星级
    /// </summary>
	public  int Level;

		/// <summary>
	/// 船只类型（1、捕鱼，2、客运，3、填海船）
    /// </summary>
	public  int Type;

		/// <summary>
	/// 造船时间(秒）
    /// </summary>
	public  int Time;

		/// <summary>
	/// 造船消耗
    /// </summary>
	public  int Cost;

		/// <summary>
	/// 海域限制
    /// </summary>
	public  int[] SeaLimit;

		/// <summary>
	/// 渔网上限
    /// </summary>
	public  int FishNetMax;

		/// <summary>
	/// 渔网回复（秒/个）
    /// </summary>
	public  int FishNetCircle;

		/// <summary>
	/// 载客数量
    /// </summary>
	public  int VisitorNum;

		/// <summary>
	/// 填海范围
    /// </summary>
	public  int ReclamationRange;

		/// <summary>
	/// 填海时间（秒）
    /// </summary>
	public  int ReclamationTime;

	}
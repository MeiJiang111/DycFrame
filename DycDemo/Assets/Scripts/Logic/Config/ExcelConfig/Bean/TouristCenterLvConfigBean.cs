using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * TouristCenterLvConfig Bean
 */
 [Serializable]
public partial class TouristCenterLvConfigBean : BaseBean
{

		/// <summary>
	/// 唯一ID
    /// </summary>
	public  int Id;

		/// <summary>
	/// 升级类型
    /// </summary>
	public  int UpType;

		/// <summary>
	/// 等级
    /// </summary>
	public  int Level;

		/// <summary>
	/// 名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 描述
    /// </summary>
	public  string Dec;

		/// <summary>
	/// 消耗材料
    /// </summary>
	public  string NeedMoney;

		/// <summary>
	/// 增加数值
    /// </summary>
	public  int AddValue;

	}
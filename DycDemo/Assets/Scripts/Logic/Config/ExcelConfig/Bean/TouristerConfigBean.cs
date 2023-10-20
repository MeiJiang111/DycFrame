using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * TouristerConfig Bean
 */
 [Serializable]
public partial class TouristerConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 游客名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 游客资源
    /// </summary>
	public  string ResourceName;

	}
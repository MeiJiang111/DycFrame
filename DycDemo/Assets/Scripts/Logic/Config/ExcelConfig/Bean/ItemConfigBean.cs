using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * ItemConfig Bean
 */
 [Serializable]
public partial class ItemConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 描述
    /// </summary>
	public  string Desc;

		/// <summary>
	/// 道具图标
    /// </summary>
	public  string Icon;

		/// <summary>
	/// 道具类型(8、鱼类)
    /// </summary>
	public  int Type;

		/// <summary>
	/// 堆叠上限
    /// </summary>
	public  int Stack;

		/// <summary>
	/// 出售价格
    /// </summary>
	public  int Price;

	}
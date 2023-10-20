using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * BankVIP Bean
 */
 [Serializable]
public partial class BankVIPBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 身份名字
    /// </summary>
	public  string name;

		/// <summary>
	/// 存款利率提升比列
    /// </summary>
	public  int saverateup;

		/// <summary>
	/// 看广告次数
    /// </summary>
	public  int condition;

	}
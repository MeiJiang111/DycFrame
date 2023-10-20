using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * Bank Bean
 */
 [Serializable]
public partial class BankBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 存款金额
    /// </summary>
	public  int saveamount;

		/// <summary>
	/// 存款时间（s）
    /// </summary>
	public  int savetime;

		/// <summary>
	/// 存款利率(百分比）
    /// </summary>
	public  int saverate;

		/// <summary>
	/// 借款金额
    /// </summary>
	public  int loanamount;

		/// <summary>
	/// 贷款期限（s）
    /// </summary>
	public  int loantime;

		/// <summary>
	/// 贷款利息(百分比）
    /// </summary>
	public  int loanrate;

	}
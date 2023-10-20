using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * SeaConfig Bean
 */
 [Serializable]
public partial class SeaConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 海域名字
    /// </summary>
	public  string Name;

		/// <summary>
	/// 资源名字
    /// </summary>
	public  string ResourceName;

		/// <summary>
	/// 海域类型(1、小型海域  2、中型海域  3、大型海域)
    /// </summary>
	public  int SeaAreaType;

		/// <summary>
	/// 解锁需要的繁荣度
    /// </summary>
	public  int Prosperity;

		/// <summary>
	/// 资源上限(鱼的数量）
    /// </summary>
	public  int ResourceMax;

		/// <summary>
	/// 资源恢复1个需要的秒数
    /// </summary>
	public  int ResourceCircle;

		/// <summary>
	/// 可以捕到的鱼的种类
    /// </summary>
	public  string FishType;

		/// <summary>
	/// 捕鱼掉落id
    /// </summary>
	public  int FishReward;

	}
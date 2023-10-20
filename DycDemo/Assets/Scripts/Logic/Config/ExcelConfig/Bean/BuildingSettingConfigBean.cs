using System;
using UnityEngine;
using System.Collections.Generic;
/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * BuildingSettingConfig Bean
 */
 [Serializable]
public partial class BuildingSettingConfigBean : BaseBean
{

		/// <summary>
	/// 唯一Id
    /// </summary>
	public  int Id;

		/// <summary>
	/// 食物
    /// </summary>
	public  int Food;

		/// <summary>
	/// 人
    /// </summary>
	public  int Human;

		/// <summary>
	/// 金币
    /// </summary>
	public  int Coins;

		/// <summary>
	/// 资源产出（1金币 2食物 3钻石）
    /// </summary>
	public  string OutPut;

		/// <summary>
	/// 金币上限
    /// </summary>
	public  int CoinsUp;

		/// <summary>
	/// 食物上限
    /// </summary>
	public  int FoodUp;

		/// <summary>
	/// 人口上限
    /// </summary>
	public  int HumanUp;

		/// <summary>
	/// 幸福度
    /// </summary>
	public  int Happiness;

	}
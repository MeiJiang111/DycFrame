using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * MineConfig Container
 */
[Serializable]
public partial class MineConfigContainer : BaseDataContainer{

		public List<MineConfigBean> dataList = new List<MineConfigBean>();
		private Dictionary<int,MineConfigBean> dataMap = new Dictionary<int,MineConfigBean>();
		protected string configNameRes = "MineConfig_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<MineConfigContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as MineConfigContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				MineConfigBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public MineConfigBean GetDataBean(int key,bool showNullWarning = true)
		{
			if (dataMap.ContainsKey(key))
			{
				return dataMap[key];
			}
			if(showNullWarning){
				LogUtil.LogWarning(this.GetType().ToString() + "non-existent Bean ,Id=" + key);
			}
			return null;
		}
	
		public Boolean HasDataBean(int key)
		{
			return dataMap.ContainsKey(key);
		}
	
		public List<MineConfigBean> GetList()
		{
			return dataList;
		}
	
		/// <summary>
		/// 获得列表中的对象 通过id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private MineConfigBean GetById(int id)
		{
			for (int i = 0; i < dataList.Count; i++)
			{
				if (dataList[i].Id.ToString().Equals(id.ToString()))
				{
					return dataList[i];
				}
			}
			return null;
		}

		public Dictionary<int,MineConfigBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}
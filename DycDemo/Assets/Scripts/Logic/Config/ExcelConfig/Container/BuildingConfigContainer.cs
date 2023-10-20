using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * BuildingConfig Container
 */
[Serializable]
public partial class BuildingConfigContainer : BaseDataContainer{

		public List<BuildingConfigBean> dataList = new List<BuildingConfigBean>();
		private Dictionary<int,BuildingConfigBean> dataMap = new Dictionary<int,BuildingConfigBean>();
		protected string configNameRes = "BuildingConfig_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<BuildingConfigContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as BuildingConfigContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				BuildingConfigBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public BuildingConfigBean GetDataBean(int key,bool showNullWarning = true)
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
	
		public List<BuildingConfigBean> GetList()
		{
			return dataList;
		}
	
		/// <summary>
		/// 获得列表中的对象 通过id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private BuildingConfigBean GetById(int id)
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

		public Dictionary<int,BuildingConfigBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}
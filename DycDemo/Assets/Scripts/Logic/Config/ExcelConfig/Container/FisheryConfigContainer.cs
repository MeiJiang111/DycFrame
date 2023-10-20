using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * FisheryConfig Container
 */
[Serializable]
public partial class FisheryConfigContainer : BaseDataContainer{

		public List<FisheryConfigBean> dataList = new List<FisheryConfigBean>();
		private Dictionary<int,FisheryConfigBean> dataMap = new Dictionary<int,FisheryConfigBean>();
		protected string configNameRes = "FisheryConfig_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<FisheryConfigContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as FisheryConfigContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				FisheryConfigBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public FisheryConfigBean GetDataBean(int key,bool showNullWarning = true)
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
	
		public List<FisheryConfigBean> GetList()
		{
			return dataList;
		}
	
		/// <summary>
		/// 获得列表中的对象 通过id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private FisheryConfigBean GetById(int id)
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

		public Dictionary<int,FisheryConfigBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}
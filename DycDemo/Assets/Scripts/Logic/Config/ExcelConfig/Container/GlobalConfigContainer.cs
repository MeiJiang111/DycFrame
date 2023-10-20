using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

[Serializable]
public partial class GlobalConfigContainer : BaseDataContainer{

		public List<GlobalConfigBean> dataList = new List<GlobalConfigBean>();
		private Dictionary<int,GlobalConfigBean> dataMap = new Dictionary<int,GlobalConfigBean>();
		protected string configNameRes = "GlobalConfig_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<GlobalConfigContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as GlobalConfigContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				GlobalConfigBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public GlobalConfigBean GetDataBean(int key,bool showNullWarning = true)
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
	
		public List<GlobalConfigBean> GetList()
		{
			return dataList;
		}
	
		private GlobalConfigBean GetById(int id)
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

		public Dictionary<int,GlobalConfigBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}

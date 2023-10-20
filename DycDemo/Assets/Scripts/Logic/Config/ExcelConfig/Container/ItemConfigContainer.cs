using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * ItemConfig Container
 */
[Serializable]
public partial class ItemConfigContainer : BaseDataContainer{

		public List<ItemConfigBean> dataList = new List<ItemConfigBean>();
		private Dictionary<int,ItemConfigBean> dataMap = new Dictionary<int,ItemConfigBean>();
		protected string configNameRes = "ItemConfig_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<ItemConfigContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as ItemConfigContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				ItemConfigBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public ItemConfigBean GetDataBean(int key,bool showNullWarning = true)
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
	
		public List<ItemConfigBean> GetList()
		{
			return dataList;
		}
	
		/// <summary>
		/// 获得列表中的对象 通过id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private ItemConfigBean GetById(int id)
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

		public Dictionary<int,ItemConfigBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
 

/** 
 * @author ExcelUtil Auto Maker
 * 
 * @version 1.0.0
 * 
 * Bank Container
 */
[Serializable]
public partial class BankContainer : BaseDataContainer{

		public List<BankBean> dataList = new List<BankBean>();
		private Dictionary<int,BankBean> dataMap = new Dictionary<int,BankBean>();
		protected string configNameRes = "Bank_Res";
	
		public override void Load()
		{
			ConfigManager.Instance.LoadConfigFile<BankContainer>(configNameRes, OnFinish);
		}
			
		protected void OnFinish(Object objData,object param)
		{
			dataList.Clear();
			dataMap.Clear();
			var data = objData as BankContainer;
			dataList.AddRange(data.dataList);
			int count = dataList.Count;
			for (int i = 0; i < count; i++)
			{
				BankBean bean = dataList[i];
				if (bean != null)
				{
					dataMap.Add(bean.Id,bean);
					bean.OnLoaded();
				}
			}
			OnLoaded();
		}
			
		public BankBean GetDataBean(int key,bool showNullWarning = true)
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
	
		public List<BankBean> GetList()
		{
			return dataList;
		}
	
		/// <summary>
		/// 获得列表中的对象 通过id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private BankBean GetById(int id)
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

		public Dictionary<int,BankBean> GetDataMap()
		{
			return dataMap;
		}
	
		public override string GetConfigName()
		{
			return configNameRes;
		}		
}
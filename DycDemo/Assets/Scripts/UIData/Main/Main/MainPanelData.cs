using cfg;
using Feif.UIFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feif.UI.Data
{
    public class MainPanelData : UIData
    {
        public Players playerData = ConfigManager.Instance.tables.TbPlayers.Get(10000);
        public int goldNum = ConfigManager.Instance.tables.TbPlayers.Get(10000).Gold;
    
        public int AddGold()
        {
            if(goldNum >= 1000)
            {
                return goldNum;
            }

            goldNum += 100;
            return goldNum;
        }

        public int GetGold() 
        {
            return goldNum;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UI.Data;
using Feif.UIFramework;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

namespace Feif.UI
{
    [PanelLayer]
    public class MainPanel : UIComponent<MainPanelData>
    {
        public Button btnShop;
        public TextMeshProUGUI textPlayerName;
        public TextMeshProUGUI textlevel;

     
        protected override Task OnCreate()
        {
            LogUtil.Log("MainPanel OnCreate");
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            btnShop.onClick.AddListener(OnOpenShop);
        }

        protected override Task OnRefresh()
        {
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
            btnShop.onClick.RemoveListener(OnOpenShop);
        }

        protected override void OnShow()
        {
            Debug.Log("MainPanel OnShow");
            InitShow();
        }

        protected override void OnHide()
        {

        }

        protected override void OnDied()
        {


        }

        private void OnOpenShop()
        {
            var data = new ShopPanelData();
            UIFrame.Show<ShopPanel>(data);
        }

        private void InitShow()
        {
            textPlayerName.text = ConfigManager.Instance.tables.TbPlayers.Get(10000).Name;
            textlevel.text = ConfigManager.Instance.tables.TbPlayers.Get(10000).Level.ToString();
        }
    }
}

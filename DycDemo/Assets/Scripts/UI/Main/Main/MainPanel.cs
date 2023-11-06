using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UI.Data;
using Feif.UIFramework;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System;

namespace Feif.UI
{
    [PanelLayer]
    public class MainPanel : UIComponent<MainPanelData>
    {
        public Button btnShop;
        public TextMeshProUGUI textPlayerName;
        public TextMeshProUGUI textlevel;
        public TextMeshProUGUI textSlider;
        public Slider slider;
        public Text textGold;
        public Text textGem;
        public Button btnAddGold;
        public Button btnAddGem;
       

        protected override Task OnCreate()
        {
            LogUtil.Log("MainPanel OnCreate");
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            btnShop.onClick.AddListener(OnOpenShop);
            btnAddGold.onClick.AddListener(OnAddGold);
        }

        protected override Task OnRefresh()
        {
            textGold.text = this.Data.GetGold().ToString(); ;
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
            btnShop.onClick.RemoveListener(OnOpenShop);
            btnAddGold.onClick.RemoveListener(OnAddGold);
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
            textPlayerName.text = this.Data.playerData.Name;
            textlevel.text = this.Data.playerData.Level.ToString();
           
            string val = $"{this.Data.playerData.AttrHp}/100  HP";
            textSlider.text = val;
            slider.value = this.Data.playerData.AttrHp;

            //textGold.text = this.Data.playerData.Gold.ToString();
            textGem.text = this.Data.playerData.Gem.ToString();
        }

        private void OnAddGold()
        {
            UIFrame.Refresh(this, new MainPanelData()
            {
                goldNum = this.Data.AddGold()
            });
        }
    }
}

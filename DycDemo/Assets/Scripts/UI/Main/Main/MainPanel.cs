using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UI.Data;
using Feif.UIFramework;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Feif.UI
{
    [PanelLayer]
    public class MainPanel : UIComponent<MainPanelData>
    {
        public Button btnShop;
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
    }
}

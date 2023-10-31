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
    public class ShopPanel : UIComponent<ShopPanelData>
    {
        public Button btnBack;
        public Button btnGoMain;

        protected override Task OnCreate()
        {
            
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            btnBack.onClick.AddListener(OnClosePanel);
            btnGoMain.onClick.AddListener(OnClosePanel);
        }

        protected override Task OnRefresh()
        {
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
            btnBack.onClick.RemoveListener(OnClosePanel);
            btnGoMain.onClick.RemoveListener(OnClosePanel);
        }

        protected override void OnShow()
        {
            Debug.Log("ShopPanel OnShow");
        }

        protected override void OnHide()
        {

        }

        protected override void OnDied()
        {


        }


        /// ------------------------------------------ Main ------------------------------------------

        private void OnClosePanel()
        {
            UIFrame.Hide();
        }
    }

}



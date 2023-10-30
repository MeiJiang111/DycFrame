using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Feif.UIFramework;
using TMPro;
using Feif.UI.Data;

namespace Feif.UI 
{

    [PanelLayer] 
    public class LoadingPanel : UIComponent<LoadingPanelData>
    {
        public Slider slider;
        public TextMeshProUGUI pectText;

        protected override Task OnCreate()
        {
          
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
         
        }

        protected override Task OnRefresh()
        {
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
           
        }

        protected override void OnShow()
        {
           
        }

        protected override void OnHide()
        {
            
        }

        protected override void OnDied()
        {
            
        }
    }
}


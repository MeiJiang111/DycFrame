using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Feif.UIFramework;


namespace Feif.UI 
{
    public class LoadingPanelData : UIData
    {

    }


    [PanelLayer] 
    public class LoadingPanel : UIComponent<LoadingPanelData>
    {
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


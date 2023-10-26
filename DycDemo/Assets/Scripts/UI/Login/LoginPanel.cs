using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Feif.UIFramework;
using System;


namespace Feif.UI
{
    public class LoginPanelData : UIData
    {

    }

    [PanelLayer]
    public class LoginPanel : UIComponent<LoginPanelData>
    {

        protected override Task OnRefresh()
        {
            Debug.Log("LoginPanel OnRefresh");
            return Task.CompletedTask;
        }
        protected override void OnBind()
        {
          
        }

        protected override void OnUnbind()
        {
          
        }

    }

}

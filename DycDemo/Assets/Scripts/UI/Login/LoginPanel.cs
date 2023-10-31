using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Feif.UIFramework;
using Feif.UI.Data;

namespace Feif.UI
{
    public class LoginPanelData : UIData
    {

    }

    [PanelLayer]
    public class LoginPanel : UIComponent<LoginPanelData>
    {
        public Button loginBtn;
        public InputField account;
        public InputField password;
       
        private void Awake()
        {
            Debug.Log("LoginPanel Awake");
        }

        protected override Task OnCreate()
        {
            Debug.Log("LoginPanel OnCreate");
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            loginBtn.onClick.AddListener(LoginBtnClick);
        }

        protected override Task OnRefresh()
        {
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
           
            loginBtn.onClick.RemoveListener(LoginBtnClick);
        }

        protected override void OnShow()
        {
            Debug.Log("LoginPanel OnShow");
        }

        protected override void OnHide()
        {
            
        }

        protected override void OnDied()
        {

        }

        private void LoginBtnClick()
        {
            UIFrame.Hide(this);
            var data = new MainPanelData();
            UIFrame.Show<MainPanel>(data);
        }
    }
}

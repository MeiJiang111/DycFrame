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


        protected override Task OnCreate()
        {
            Debug.Log("LoginPanel OnCreate");
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            Debug.Log("LoginPanel OnBind");
            loginBtn.onClick.AddListener(LoginBtnClick);
        }

        protected override Task OnRefresh()
        {
            Debug.Log("LoginPanel OnRefresh");
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {
            Debug.Log("LoginPanel OnUnbind");
            loginBtn.onClick.RemoveListener(LoginBtnClick);
        }

        protected override void OnShow()
        {
            Debug.Log("LoginPanel OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("LoginPanel OnHide");
        }

        protected override void OnDied()
        {
            Debug.Log("LoginPanel OnDied");
        }

        private void LoginBtnClick()
        {
            UIFrame.Hide(this);

            //var data = new LoadingPanelData();
            //UIFrame.Show<LoadingPanel>(data);

            var data = new MainPanelData();
            UIFrame.Show<MainPanel>(data);
        }
    }
}

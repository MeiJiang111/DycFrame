using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feif.UI.Data;
using Feif.UIFramework;
using System.Threading.Tasks;

namespace Feif.UI
{
    [PanelLayer]
    public class MainPanel : UIComponent<MainPanelData>
    {
        protected override Task OnCreate()
        {
            LogUtil.Log("MainPanel OnCreate");
            return Task.CompletedTask;
        }
    }
}

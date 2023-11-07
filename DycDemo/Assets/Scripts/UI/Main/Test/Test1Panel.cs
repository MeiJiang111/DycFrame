using Feif.UI.Data;
using Feif.UIFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


namespace Feif.UI
{
    
    public class Test1Data : UIData
    {

    }

    [PanelLayer]
    public class Test1Panel : UIComponent<Test1Data>
    {
        //public Transform centerTra;
        //public Transform elementTra;
        //private Transform targetTra;

        //public float radius = 150f;
        //public float heightMultiple = 2f;
        //public float speed = 100f;

        //public float amountAngle { get; private set; }

        //private float beforeRadius;
        //private float beforeHeightMultiple;
        //private int unityVersion;
        //public bool isAuto = true;
        //public float autoSpeed = 20f;
        //public float intervalTime = 3f;
        //private float beforeAutoTime;
        //public float autoStopTime = 3f;
        //private float beforeStopTime;


        /// ----------------------------------------------- 
        public GameObject optionPrefab;
        public Transform optionGroup;
        Transform[] options;
        TextMeshProUGUI[] textNameList = new TextMeshProUGUI[6];

        [Range(0, 10)]
        public int optionNum;
        float halfNum;

        Dictionary<Transform, Vector2> optionDic = new Dictionary<Transform, Vector2>();
        Vector3 center = Vector3.zero;
        float R = 500f;

        [Range(1f, 10f)]
        public float speed;

        protected override Task OnCreate()
        {
            for (int i = 0; i < optionNum; i++)
            {
                GameObject go = Instantiate(optionPrefab);
                go.transform.SetParent(optionGroup);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.name = $"btn_{i}";

                textNameList[i] = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                textNameList[i].text = i.ToString();
            }

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
            //Create();

            //StartCoroutine(StartIEnumerator());
        }


        private void Create()
        {
         
        }
    }

}


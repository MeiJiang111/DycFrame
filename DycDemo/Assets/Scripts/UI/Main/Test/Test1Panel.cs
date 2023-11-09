using Feif.UI.Data;
using Feif.UIFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feif.UI
{

    public class Test1Data : UIData
    {

    }

    [PanelLayer]
    public class Test1Panel : UIComponent<Test1Data>
    {
        public Button btnLeft;
        public Button btnRight;
        public GameObject optionPrefab;
        public Transform optionGroup;
      
        Transform[] options;
        TextMeshProUGUI[] textNameList = new TextMeshProUGUI[6];

        [Range(0, 10)]
        public int optionNum;
        float halfNum;

        Dictionary<Transform, Vector2> optionPDic = new Dictionary<Transform, Vector2>();
        Dictionary<Transform, int> optionSDic = new Dictionary<Transform, int>();
        Vector3 center = Vector3.zero;
        float R = 500f;

        [Range(1f, 10f)]
        public float speed;
        public float yOffset;

        Coroutine currentPie;

        protected override Task OnCreate()
        {
            for (int i = 0; i < optionNum; i++)
            {
                GameObject go = Instantiate(optionPrefab);
                go.transform.SetParent(optionGroup);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.name = $"btn_{i}";

                textNameList[i] = go.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                textNameList[i].text = i.ToString();
            }

            halfNum = optionNum / 2;
            options = new Transform[optionNum];

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = optionGroup.GetChild(i);
            }

            InitPos();
            InitSibling();
            return Task.CompletedTask;
        }

        protected override void OnBind()
        {
            btnLeft.onClick.AddListener(OnBtnMoveLeft);
            btnRight.onClick.AddListener(OnBtnMoveRight);
        }

        protected override Task OnRefresh()
        {
            return Task.CompletedTask;
        }

        protected override void OnUnbind()
        {

            btnLeft.onClick.RemoveListener(OnBtnMoveLeft);
            btnRight.onClick.RemoveListener(OnBtnMoveRight);
        }

        protected override void OnShow()
        {

        }

        private void InitPos()
        {
            float angle;
            for (int i = 0; i < optionNum; i++)
            {
                angle = (360f / (float)optionNum) * i * Mathf.Deg2Rad;
                float x = Mathf.Sin(angle) * R;
                float z = -Mathf.Cos(angle) * R;
                float y = 0;

                if (i != 0)
                {
                    y = i * yOffset;
                    if (i > halfNum)
                    {
                        y = (optionNum - i) * yOffset;
                    }
                }

                options[i].localPosition = new Vector3(x, y, z);
                //LogUtil.Log($"x = {x}  y = {y} z = {z}  temp = {options[i].localPosition}");
                optionPDic.Add(options[i], options[i].localPosition);
            }
        }

        private void InitSibling()
        {
            for (int i = 0; i < optionNum; i++)
            {
                if (i <= halfNum)
                {
                    if (optionNum % 2 == 0)
                    {
                        options[i].SetSiblingIndex((int)halfNum - i);
                    }
                    else
                    {
                        options[i].SetSiblingIndex((int)((optionNum - 1) / 2) - i);
                    }
                }
                else
                {
                    options[i].SetSiblingIndex(options[optionNum - i].GetSiblingIndex());
                }
            }

            for (int i = 0; i < optionNum; i++)
            {
                optionSDic.Add(options[i], options[i].GetSiblingIndex());
            }
        }

        private void OnBtnMoveLeft()
        {
            StartCoroutine(MoveLeft());
        }

        private void OnBtnMoveRight()
        {
            StartCoroutine(MoveRight());
        }

        IEnumerator MoveLeft()
        {
            if (currentPie != null)
            {
                yield return currentPie;
            }

            Vector3 p = optionPDic[options[0]];
            int s = optionSDic[options[0]];
            Vector3 targetP;

            for (int i = 0; i < optionNum; i++)
            {
                if(i == (optionNum - 1))
                {
                    targetP = p;
                    optionSDic[options[i]] = s;
                }
                else
                {
                    targetP = options[(i + 1) % optionNum].localPosition;
                    optionSDic[options[i]] = optionSDic[options[(i + 1) % optionNum]];
                }
                options[i].SetSiblingIndex(optionSDic[options[i]]);
                currentPie = StartCoroutine(MoveToTarget(options[i],targetP));
            }

            yield return null;
        }

        IEnumerator MoveRight()
        {
            if(currentPie  != null) 
            {
                yield return currentPie;
            }

            Vector3 p = optionPDic[options[optionNum - 1]];
            int s = optionSDic[options[optionNum - 1]];
            Vector3 targetP;

            for (int i = optionNum - 1; i >= 0; i--)
            {
                if(i == 0)
                {
                    targetP = p;
                    optionSDic[options[i]] = s;
                }
                else
                {
                    targetP = options[(i - 1) % optionNum].localPosition;
                    optionSDic[options[i]] = optionSDic[options[(i - 1)% optionNum]];
                }

                options[i].SetSiblingIndex(optionSDic[options[i]]);
                currentPie = StartCoroutine(MoveToTarget(options[i], targetP));
            }
            yield return null;
        }

        IEnumerator MoveToTarget(Transform tf, Vector3 target)
        {
            float tempSpeed = (tf.localPosition - target).magnitude * speed;
            while (tf.localPosition != target) 
            {
                tf.localPosition = Vector3.MoveTowards(tf.localPosition, target, tempSpeed * Time.deltaTime);
                yield return null;
            }

            optionPDic[tf] = target;
            yield return null;
        }

    }
}


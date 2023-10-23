using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Feif.UIFramework 
{
    //��Ӳ����������Ϊ
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public abstract class UIBase : MonoBehaviour
    {
        /// <summary>
        /// �Ƿ��Զ�����
        /// </summary>
        public bool AutoDestroy = true;

        /// <summary>
        /// һ�����ڵ�
        /// </summary>
        public UIBase Parent;

        /// <summary>
        /// һ���ӽڵ�
        /// </summary>
        public List<UIBase> Children = new List<UIBase>();

        protected internal Task InnerOnCreate() => OnCreate();
        protected internal Task InnerOnRefresh() => OnRefresh();
        protected internal void InnerOnBind() => OnBind();
        protected internal void InnerOnUnbind() => OnUnbind();
        protected internal void InnerOnShow() => OnShow();
        protected internal void InnerOnHide() => OnHide();
        protected internal void InnerOnDied() => OnDied();


        /// <summary>
        /// ����ʱ���ã�����������ִֻ��һ��
        /// </summary>
        protected virtual Task OnCreate() => Task.CompletedTask;


        /// <summary>
        /// ˢ��ʱ����
        /// </summary>
        protected virtual Task OnRefresh() => Task.CompletedTask;


        /// <summary>
        /// ���¼�
        /// </summary>
        protected virtual void OnBind()
        {

        }

        /// <summary>
        /// ����¼�
        /// </summary>
        protected virtual void OnUnbind() 
        { 

        }

        /// <summary>
        /// ��ʾʱ����
        /// </summary>
        protected virtual void OnShow() 
        { 

        }

        /// <summary>
        /// ����ʱ����
        /// </summary>
        protected virtual void OnHide()
        {

        }

        /// <summary>
        /// ����ʱ���ã�����������ִֻ��һ��
        /// </summary>
        protected virtual void OnDied() 
        {

        }
    }
}



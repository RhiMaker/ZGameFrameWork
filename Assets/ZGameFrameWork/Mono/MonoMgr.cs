using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace ZGameFrameWork
{
    public class MonoMgr : BaseManager<MonoMgr>
    {
        private MonoController controller;

        public MonoMgr()
        {
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }

        public void AddUpdateListener(UnityAction fun)
        {
            controller.AddUpdateListener(fun);
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            controller.RemoveUpdateListener(fun);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }
        //这个方法智能调用controllrt里自带的方法
        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }
        public Coroutine StartCoroutine_Auto(IEnumerator routine) => controller.StartCoroutine(routine);
    }
}
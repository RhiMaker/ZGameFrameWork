using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZGameFrameWork
{
    public class MonoController : MonoBehaviour
    {
        public event UnityAction updateEvent;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (updateEvent != null)
            {
                updateEvent.Invoke();
            }
        }

        public void AddUpdateListener(UnityAction fun)
        {
            updateEvent += fun;
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            updateEvent -= fun;
        }
    }
}
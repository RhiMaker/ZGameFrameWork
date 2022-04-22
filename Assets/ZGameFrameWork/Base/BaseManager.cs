using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZGameFrameWork
{
    public class BaseManager<T> where T:new()
    {
        private static T instance;

        private BaseManager(){}
        public static T GetInstance()
        {
            if (instance==null)
            {
                instance = new T();
            }

            return instance;
        }
    }
}


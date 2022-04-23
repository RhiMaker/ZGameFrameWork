using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using ZGameFrameWork;

namespace ZGameFrameWork
{
    public class EventCenter : BaseManager<EventCenter>
    {
        private Dictionary<string, UnityAction<object>> eventDic = new FlexibleDictionary<string, UnityAction<object>>();

        public void AddEventListener(string name, UnityAction<object> action)
        {
            if (eventDic.ContainsKey(name))
            {
                eventDic[name] += action;
            }
            else
            {
                eventDic.Add(name, action);
            }
        }

        public void RemoveEventListener(string name, UnityAction<object> action)
        {
            if (eventDic.ContainsKey(name))
            {
                eventDic[name] -= action;
            }
        }
        public void EventTrigger(string name,object info)
        {
            if (eventDic.ContainsKey(name))
            {
                eventDic[name].Invoke(info);
            }
        }

        public void Clear()
        {
            eventDic.Clear();
        }
    }
}
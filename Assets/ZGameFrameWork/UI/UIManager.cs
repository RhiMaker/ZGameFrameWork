using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ZGameFrameWork
{
    public enum E_UI_Layer
    {
        Bot,
        Mid,
        Top,
        System
    }

    public class UIManager : BaseManager<UIManager>
    {
        public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
        private Transform bot;
        private Transform mid;
        private Transform top;
        private Transform system;
        public RectTransform canvas;

        public UIManager()
        {
            GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
            canvas = obj.transform as RectTransform;
            GameObject.DontDestroyOnLoad(obj);
            bot = canvas.Find("Bot");
            mid = canvas.Find("Mid");
            top = canvas.Find("Top");
            system = canvas.Find("System");
            obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
            GameObject.DontDestroyOnLoad(obj);
        }

        public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callback = null)
            where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowMe();
                if (callback != null)
                {
                    callback(panelDic[panelName] as T);
                }
                return;
            }

            ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
            {
                Transform father = bot;
                switch (layer)
                {
                    case E_UI_Layer.Mid:
                        father = mid;
                        break;
                    case E_UI_Layer.Top:
                        father = top;
                        break;
                    case E_UI_Layer.System:
                        father = system;
                        break;
                }

                obj.transform.SetParent(father);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.zero;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;

                T panel = obj.GetComponent<T>();
                if (callback != null)
                {
                    callback(panel);
                }

                panelDic.Add(panelName, panel);
            });
        }

        public void HidePanel(string panelName)
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideMe();
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }

        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                return panelDic[panelName] as T;
            }
            else
            {
                return null;
            }
        }

        public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callback)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = control.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callback);

            trigger.triggers.Add(entry);
        }
    }
}
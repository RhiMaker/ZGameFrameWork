using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZGameFrameWork;

namespace ZGameFrameWork
{
    public class PoolData
    {
        public GameObject fatherObj;
        public List<GameObject> poolList;

        public PoolData(GameObject obj, GameObject poolObj)
        {
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.parent = poolObj.transform;
            poolList = new List<GameObject>() {};
            PushObj(obj);
        }

        public void PushObj(GameObject obj)
        {
            poolList.Add(obj);
            obj.transform.parent = fatherObj.transform;
            obj.SetActive(false);
        }

        public GameObject GetObj()
        {
            GameObject obj = null;
            obj = poolList[0];
            poolList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.parent = null;
            return obj;
        }
    }

    public class PoolMgr : BaseManager<PoolMgr>
    {
        public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
        private GameObject poolObj;

        public GameObject GetObj(string name)
        {
            GameObject obj = null;
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                obj = poolDic[name].GetObj();
            }
            else
            {
                obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
                obj.name = name;
            }
            return obj;
        }

        public void PushObj(string name, GameObject obj)
        {
            if (poolObj == null)
            {
                poolObj = new GameObject("PoolObj");
            }
            if (poolDic.ContainsKey(name))
            {
                poolDic[name].PushObj(obj);
            }
            else
            {
                poolDic.Add(name,new PoolData(obj,poolObj));
            }
        }

//场景切换，清空
        public void Clear()
        {
            poolObj = null;
            poolDic.Clear();
        }
    }
}
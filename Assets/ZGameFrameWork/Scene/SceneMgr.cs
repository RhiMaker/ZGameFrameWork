using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using ZGameFrameWork;

namespace ZGameFrameWork
{
    public class SceneMgr : BaseManager<SceneMgr>
    {
        public void LoadScene(string name,UnityAction fun)
        {
            SceneManager.LoadScene(name);
            fun();
        }
        public void LoadSceneAsyn(string name,UnityAction fun)
        {
            MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
        }

        private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction fun)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            while (!ao.isDone)
            {
                //进度条分发进度
                EventCenter.GetInstance().EventTrigger("Loading",ao.progress);
                yield return ao.progress;
            }
            fun();
        }
    }
}


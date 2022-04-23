using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZGameFrameWork
{
    public class MusicMgr : BaseManager<MusicMgr>
    {
        private AudioSource bgMusic = null;
        private float bgmValue = 1;

        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();
        private float soundValue = 1;

        public MusicMgr()
        {
            MonoMgr.GetInstance().AddUpdateListener(Update);
        }

        private void Update()
        {
            for (int i = soundList.Count-1; i >=0; --i)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
        }
        public void PlayBGM(string name)
        {
            if (bgMusic == null)
            {
                GameObject obj = new GameObject();
                obj.name = "bgMusic";
                obj.AddComponent<AudioSource>();
            }

            ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BGM/" + name, (clip) =>
            {
                bgMusic.clip = clip;
                bgMusic.volume = bgmValue;
                bgMusic.loop = true;
                bgMusic.Play();
            });
        }

        public void ChangeBGMVolume(float v)
        {
            bgmValue = v;
            if (bgMusic == null)
            {
                return;
            }

            bgMusic.volume = bgmValue;
        }

        public void PauesBGM()
        {
            if (bgMusic == null)
            {
                return;
            }

            bgMusic.Pause();
        }

        public void StopBGM()
        {
            if (bgMusic == null)
            {
                return;
            }

            bgMusic.Stop();
        }

        public void PlaySound(string name,bool isLoop, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "soundMusic";
            }

            ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
            {
                AudioSource source = soundObj.AddComponent<AudioSource>();
                source.clip = clip;
                source.loop = isLoop;
                source.volume = soundValue;
                source.Play();
                soundList.Add(source);
                if (callback != null)
                {
                    callback(source);
                }
            });
        }

        public void ChangeSoundVolume(float v)
        {
            soundValue = v;
            for (int i = 0; i < soundList.Count; i++)
            {
                soundList[i].volume = soundValue;
            }
        }

        public void StopSoundM(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                GameObject.Destroy(source);
            }
        }
    }
}
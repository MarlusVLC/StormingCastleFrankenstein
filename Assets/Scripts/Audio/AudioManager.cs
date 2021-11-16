using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Audio
{
    public class AudioManager : MonoCache
    {
        public Sound[] sounds;

        private int indexSequence = 0;
        private AudioSource currentSource;

        public float ClipLength => currentSource.clip.length;
        public event Action OnClipFinished;

        void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.playOnAwake = false;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        private void OnValidate()
        {
            for (var i = 0; i < sounds.Length; i++)
            {
                var s = sounds[i];
                s.name = $"{i.ToString()}: ";
                if (s.clip == null)
                {
                    s.name += "EmptyFile";
                }
                else
                {
                    s.name += s.clip.name;
                }
            }
        }

        protected int GetSoundsSize()
        {
            return sounds.Length;
        }

        protected void PlaySequence()
        {
            Play(indexSequence);
            indexSequence++;
        
            if (indexSequence == GetSoundsSize())
            {
                indexSequence = 0;
            }
        }
    
        protected void Play(int index, bool ignoreListenerPause = false)
        {
            if (index >= sounds.Length || sounds[index] == null)
            {
                if (Application.isEditor)
                    Debug.LogWarning($"There seems to be no space/file allocated in index: {index.ToString()}"
                            + $" | GameObject: {gameObject.name}");
                return;
            }
        
            currentSource = sounds[index].source;
            currentSource.ignoreListenerPause = ignoreListenerPause;
            currentSource.Play();

            if (currentSource.gameObject.activeSelf == true)
            {
                StartCoroutine(WaitForAudioEnd());
            }
        }
    
        public void Stop()
        {
            if (currentSource)
            {
                currentSource.Stop();   
            }
        }

        public void Toggle(AudioState state, int index, bool ignoreListenerPause = false)
        {
            if (!SoundExists(index))
            {
                Debug.LogWarning("There's no sound associated in index " + index);
                return;
            }
            switch (state)
            {
                case AudioState.Play:
                    Play(index, ignoreListenerPause);
                    break;
                case AudioState.Pause:
                    sounds[index].source.Pause();
                    break;
                case AudioState.Unpause:
                    sounds[index].source.UnPause();
                    break;
                case AudioState.Stop:
                    sounds[index].source.Stop();
                    break;
            }
        }

        public bool SoundExists(int index)
        {
        
            return sounds.Length > index && sounds[index] != null;
        }

        private IEnumerator WaitForAudioEnd()
        {
            while (currentSource.isPlaying)
            {
                yield return null;
            }
            OnClipFinished?.Invoke();
        }
    
        public enum AudioState
        {
            Play,
            Stop,
            Pause,
            Unpause
        }
    }
}
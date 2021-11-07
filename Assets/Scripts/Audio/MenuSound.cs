using System;
using UnityEngine;

namespace Audio
{
    public class MenuSound : AudioManager
    {

        protected void OnEnable()
        {
            // ToggleBackgroundSoundtrack(sounds[4].IsPaused ? AudioState.Unpause : AudioState.Play);
            ToggleBackgroundSoundtrack(AudioState.Play);
        }

        protected void OnDisable()
        {
            ToggleBackgroundSoundtrack(AudioState.Pause);
        }

        public void PlayMouseOver()
        {
            PlayIndex(0);
        }

        public void PlayIndex(int index)
        {
            Play(index, true);

            switch (index)
            {
                case 3:
                    Toggle(AudioState.Stop, 4);
                    break;
            }
        }

        public void ToggleBackgroundSoundtrack(AudioState state)
        {
            Toggle(state, 4, true);
        }
    }
}
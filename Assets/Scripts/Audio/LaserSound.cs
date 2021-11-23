using System.Collections;
using UnityEngine;

namespace Audio
{
    public class LaserSound : AudioManager
    {
        public void PlayLaserSound()
        {
            StartCoroutine(LaserSoundCoroutine());
        }

        private void StopLaserSound()
        {
            Toggle(AudioState.Stop, 0);
            Toggle(AudioState.Stop, 1);
        }
        
        private IEnumerator LaserSoundCoroutine()
        {
            StopLaserSound();
            Play(0);
            yield return new WaitForSeconds(1.5f);
            int index = 1;
            sounds[index].source.loop = true;
            Play(index);
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Audio.OST
{
    public class OSTSound : AudioManager
    {
        public void PlayOST()
        {
            StartCoroutine(OSTCoroutine());
        }

        private void Start()
        {
            PlayOST();
        }
        
        private IEnumerator OSTCoroutine()
        {
            Play(0);
            yield return new WaitForSeconds(sounds[0].clip.length + 30f);
            Play(1);
            yield return new WaitForSeconds(sounds[0].clip.length + 30f);
            Play(2);
        }
    }
}
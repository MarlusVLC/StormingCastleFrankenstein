using UnityEngine;

namespace Audio
{
    public class WendigoSound : AudioManager
    {
        public void PlayWendigoAttackSound()
        {
            var index = Random.Range(0, 6);
            Play(index);
        }

        public void PlayWendigoDeathSound()
        {
            var index = Random.Range(7, 14);
            Play(index);
        }
    }
}
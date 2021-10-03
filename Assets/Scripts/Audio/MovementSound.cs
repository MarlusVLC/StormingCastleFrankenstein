using UnityEngine;

namespace Audio
{
    public class MovementSound : AudioManager
    {
        public void PlayStepSound()
        {
            var index = Random.Range(0, 5);
            Play(index);
        }
        
        public void PlayJumpSound()
        {
            var index = Random.Range(6, 11);
            Play(index);
        }
        
        public void PlayFallSound()
        {
            var index = Random.Range(13, 14);
            Play(index);
        }
    }
}
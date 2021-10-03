using UnityEngine;

namespace Audio
{
    public class MovementSound : AudioManager
    {
        public void PlayStepSound()
        {
            int index = Random.Range(0, 5);
            Play(index);
        }
        
        public void PlayJumpSound()
        {
            int index = Random.Range(6, 11);
            Play(index);
        }
        
        public void PlayFallSound()
        {
            int index = Random.Range(13, 14);
            Play(index);
        }
    }
}
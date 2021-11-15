using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class MovementSound : AudioManager
    {
        [SerializeField] private bool playVoice;
        
        public void PlayStepSound()
        {
            var index = Random.Range(0, 5);
            Play(index);
        }
        
        public void PlayJumpSound()
        {
            int index;
            if (playVoice)
            {
                index = Random.Range(6, 11);
                Play(index);
            }

            index = Random.Range(15, 18);
            Play(index);
        }
        
        public void PlayFallSound()
        {
            int index;
            if (playVoice)
            {
                index = Random.Range(12, 14);
                Play(index);
            }
            
            index = Random.Range(19, 22);
            Play(index);
        }
    }
}
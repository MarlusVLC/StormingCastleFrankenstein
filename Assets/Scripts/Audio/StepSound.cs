using UnityEngine;

namespace Audio
{
    public class StepSound : AudioManager
    {
        public void PlayStepSound()
        {
            int index = Random.Range(0, GetSoundsSize());
            Play(index);
        }
    }
}
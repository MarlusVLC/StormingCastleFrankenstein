using UnityEngine;

namespace Audio
{
    public class AudioLogSound : AudioManager
    {
        public void PlayAudioLogSound(int index)
        {
            Stop();
            Play(index, true);
        }
    }
}
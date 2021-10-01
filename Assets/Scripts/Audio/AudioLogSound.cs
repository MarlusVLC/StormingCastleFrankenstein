namespace Audio
{
    public class AudioLogSound : AudioManager
    {
        public void PlayAudioLogSound(int index)
        {
            Play(index);
        }

        public void StopAudioLogSound()
        {
            Stop();
        }
    }
}
namespace Audio
{
    public class MenuSound : AudioManager
    {
        public void PlayMouseOver()
        {
            Play(0);
        }

        public void PlayIndex(int index)
        {
            Play(index);
        }
    }
}
using UnityEngine;

namespace Audio
{
    public class SpiderSound : AudioManager
    {
        public void PlayAttackSound()
        {
            // var index = Random.Range(0, 3);
            // Play(index);
        }

        public void PlayDeathSound()
        {
            var index = Random.Range(4, 7);
            Play(index);
        }
        
        public void PlayAlertSound()
        {
            var index = 0;
            Play(index);
        }

        public void PlayDamageSound()
        {
            var index = Random.Range(1, 3);
            Play(index);
        }

        public void PlayChaseSound()
        {
            // var index = Random.Range(16, 19);
            // Play(index);
        }

        public void PlayMoveStartSound()
        {
            Play(8);
        }

        public void StopMoveBeginSound()
        {
            Toggle(AudioState.Stop, 8);
        }

        public void PlayMoveMidSound()
        {
            StopMoveBeginSound();
            int index = 9;
            sounds[index].source.loop = true;
            Play(index);
        }

        public void StopMoveMidSound()
        {
            Toggle(AudioState.Stop, 9);
        }

        public void PlayMoveEndSound()
        {
            StopMoveBeginSound();
            StopMoveMidSound();
            Play(10);
        }

        public void PlaySound(EnemyActionType actionType)
        {
            switch (actionType)
            {
                case EnemyActionType.Attack:
                    PlayAttackSound();
                    break;
                case EnemyActionType.Alert:
                    PlayAlertSound();
                    break;
                case EnemyActionType.Chase:
                    PlayChaseSound();
                    break;
                case EnemyActionType.Damage:
                    PlayDamageSound();
                    break;
                case EnemyActionType.Death:
                    PlayDeathSound();
                    break;
            }
        }
    }
}
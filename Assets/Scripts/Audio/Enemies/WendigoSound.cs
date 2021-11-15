using UnityEngine;

namespace Audio
{
    public class WendigoSound : AudioManager
    {
        public void PlayWendigoAttackSound()
        {
            var index = Random.Range(0, 3);
            Play(index);
        }

        public void PlayWendigoDeathSound()
        {
            var index = Random.Range(4, 7);
            Play(index);
        }
        
        public void PlayWendigoAlertSound()
        {
            var index = Random.Range(8, 11);
            Play(index);
        }

        public void PlayWendigoDamageSound()
        {
            var index = Random.Range(12, 15);
            Play(index);
        }

        public void PlayWendigoChaseSound()
        {
            var index = Random.Range(16, 19);
            Play(index);
        }

        public void PlaySound(EnemyActionType actionType)
        {
            switch (actionType)
            {
                case EnemyActionType.Attack:
                    PlayWendigoAttackSound();
                    break;
                case EnemyActionType.Alert:
                    PlayWendigoAlertSound();
                    break;
                case EnemyActionType.Chase:
                    PlayWendigoChaseSound();
                    break;
                case EnemyActionType.Damage:
                    PlayWendigoDamageSound();
                    break;
                case EnemyActionType.Death:
                    PlayWendigoDeathSound();
                    break;
            }
        }
    }
}
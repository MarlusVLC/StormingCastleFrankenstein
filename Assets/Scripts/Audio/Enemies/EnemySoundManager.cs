using AI;
using Utilities;

namespace Audio
{
    public class EnemySoundManager : Singleton<EnemySoundManager>
    {
        private WendigoSound _wendigoSound;

        private void Awake()
        {
            _wendigoSound = GetComponent<WendigoSound>();
        }

        public void PlayAttackSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayWendigoAttackSound();
                    break;
            }
        }

        public void PlayDeathSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayWendigoDeathSound();
                    break;
            }
        }
        
        public void PlayAlertSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayWendigoAlertSound();
                    break;
            }
        }

        public void PlayDamageSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayWendigoDamageSound();
                    break;
            }
        }

        public void PlayChaseSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayWendigoDamageSound();
                    break;
            }
        }
    }
    
    public enum EnemyActionType{
        Attack,
        Alert,
        Chase,
        Damage,
        Death
    }
}
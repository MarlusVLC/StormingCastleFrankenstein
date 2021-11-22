using AI;
using UnityEngine;
using Utilities;

namespace Audio
{
    public class EnemySoundManager : Singleton<EnemySoundManager>
    {
        private WendigoSound _wendigoSound;
        private RobotSound _robotSound;
        

        private void Awake()
        {
            _wendigoSound = GetComponent<WendigoSound>();
            _robotSound = GetComponent<RobotSound>();
        }

        public void PlayAttackSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayAttackSound();
                    break;
                case EnemyType.Robot:
                    _robotSound.PlayAttackSound();
                    break;
            }
            
        }

        public void PlayDeathSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayDeathSound();
                    break;
                case EnemyType.Robot:
                    _robotSound.PlayDeathSound();
                    break;
            }
        }
        
        public void PlayAlertSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayAlertSound();
                    break;
                case EnemyType.Robot:
                    _robotSound.PlayAlertSound();
                    break;
            }
        }

        public void PlayDamageSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayDamageSound();
                    break;
                case EnemyType.Robot:
                    _robotSound.PlayDamageSound();
                    break;
            }
        }

        public void PlayChaseSound(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Wendigo:
                    _wendigoSound.PlayChaseSound();
                    break;
                case EnemyType.Robot:
                    _robotSound.PlayChaseSound();
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
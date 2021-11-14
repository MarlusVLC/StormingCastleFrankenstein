using System;
using AI;
using UnityEngine;

namespace Audio
{
    public class EnemySoundManager : MonoBehaviour
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

        public void PlayDeathSound()
        {
            var index = Random.Range(4, 7);
            Play(index);
        }
        
        public void PlayAlertSound()
        {
            var index = Random.Range(4, 7);
            Play(index);
        }

        public void PlayDamageSound()
        {
            var index = Random.Range(8, 11);
            Play(index);
        }

        public void PlayChaseSound()
        {
            var index = Random.Range(12, 15);
            Play(index);
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
using System;
using AI;
using Audio;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities
{
    public class BotHealth : Health
    {
        [SerializeField] private VisualEffect bloodVFX;

        [SerializeField] private BlinkEffect[] blinkEffects;
        private EnemyType _enemyType;

        protected override void Awake()
        {
            base.Awake();
            _enemyType = GetComponent<EnemyBot>().EnemyType;
            if (blinkEffects.Length < 1)
            {
                blinkEffects = GetComponentsInChildren<BlinkEffect>();
            }
        }

        protected override void Die()
        {
            transform.parent.position = transform.position;
            Instantiate(bloodVFX, transform.parent);
            
            EnemySoundManager.Instance.PlayDeathSound(_enemyType);

            Destroy(gameObject);
        }

        public override void TakeDamage(int damage)
        {
            EnemySoundManager.Instance.PlayDamageSound(_enemyType);
            base.TakeDamage(damage);
            Array.ForEach(blinkEffects, bE => bE.DamageBlink());
        }
    }
}
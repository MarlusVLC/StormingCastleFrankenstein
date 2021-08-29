using Unity.Collections;
using UnityEngine;
using Utilities;

namespace Entities
{
    public abstract class Health : MonoCache
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int startingHealth = 100;
        
        private int _currentHealth;

        protected override void Awake()
        {
            base.Awake();
            _currentHealth = Mathf.Clamp(startingHealth, 0, maxHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        protected abstract void Die();
    }
}
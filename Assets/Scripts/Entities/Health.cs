using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Entities
{
    public abstract class Health : MonoCache
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [SerializeField] private int startingHealth = 100;

        private int _currentHealth;

        protected override void Awake()
        {
            base.Awake();
            _currentHealth = Mathf.Clamp(startingHealth, 0, MaxHealth);
        }

        public void RecoverHealth(int healthAddition)
        {
            _currentHealth += healthAddition;
            if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
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

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
        
        
    }
}
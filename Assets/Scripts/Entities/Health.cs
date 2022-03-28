using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Entities
{
    public abstract class Health : MonoCache, IHealth
    {
        [field: Range(0,9999)][field: SerializeField] public int MaxHealth { get; private set; }
        [SerializeField] private int startingHealth = 100;

        [SerializeField] private int _currentHealth;
        private bool _isImmortal = false;
        
        protected override void Awake()
        {
            base.Awake();
            CurrentHealth = Mathf.Clamp(startingHealth, 0, MaxHealth);
            OnHealthChanged();
        }

        // public void RecoverHealth(int healthAddition)
        public IEnumerable RecoverHealth(int healthAddition)
        {
            _currentHealth += healthAddition;
            if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
            OnHealthChanged();
            yield return null;
        }

        // public virtual void TakeDamage(int damage)
        public virtual IEnumerable TakeDamage(int damage)
        {
            if (_isImmortal) yield break;
            Debug.Log(CurrentHealth.ToString());
            CurrentHealth -= damage;
        }

        protected abstract void Die();

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    OnHealthChanged();
                    Die();
                }
                OnHealthChanged();
            } 

        }

        protected virtual void OnHealthChanged(HealthChangedEventArgs e = null)
        {
            if (e == null)
            {
                HealthChangedEventArgs args = new HealthChangedEventArgs
                {
                    Health = _currentHealth, MaxHealth = MaxHealth
                };
                HealthChanged?.Invoke(this, args);
            }
            else
            {
                HealthChanged?.Invoke(this, e);
            }
        
        }
        
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        public class HealthChangedEventArgs : EventArgs
        {
            public int Health {get; set;}
            public int MaxHealth { get; set; }
        }

        public bool IsFull => _currentHealth == MaxHealth;

        public bool IsImmortal
        {
            get => _isImmortal;
            set => _isImmortal = value;
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace UI.Interfaces
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] int initialHealth = 10;
        private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = Mathf.Max(0, value);
        }

        private void Awake()
        {
            CurrentHealth = initialHealth;
        }

        public IEnumerable TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            yield return new WaitForSeconds(0.1f);
            Debug.Log($"{name} has {CurrentHealth.ToString()} health right now");
            if (CurrentHealth <= 0) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
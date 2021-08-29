using Unity.Collections;
using UnityEngine;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int startingHealth = 100;
        
        private int _currentHealth;

        private void Awake()
        {
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
        
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
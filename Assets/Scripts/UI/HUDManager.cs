using System;
using Entities;
using UnityEngine;

namespace UI
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private ProgressBarPro healthProgressBar;

        private void OnEnable()
        {
            health.HealthChanged += UpdateHealth;
        }
        
        private void OnDisable()
        {
            health.HealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(object sender, Health.HealthChangedEventArgs e)
        {
            healthProgressBar.SetValue(e.Health,e.MaxHealth);
        }
    }
}
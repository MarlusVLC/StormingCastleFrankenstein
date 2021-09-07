using System;
using Drops;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class HudCoin : MonoBehaviour
    {
        [SerializeField] private MoneyHandler moneyHandler;
        
        private TextMeshProUGUI _coinCounter;

        private void Awake()
        {
            _coinCounter = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            moneyHandler.CoinAmountChanged += UpdateCounter;
        }
        
        private void OnDisable()
        {
            moneyHandler.CoinAmountChanged -= UpdateCounter;
        }

        private void UpdateCounter(object sender, MoneyHandler.CoinAmountChangedEventArgs e)
        {
            _coinCounter.text = e.CoinAmount.ToString();
        }
    }
}
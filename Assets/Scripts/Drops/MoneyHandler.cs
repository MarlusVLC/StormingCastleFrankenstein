using System;
using UnityEngine;

namespace Drops
{
    public class MoneyHandler : MonoBehaviour
    {
        [SerializeField] private int maxCoinAmount;
        [SerializeField] private int startingCoinAmount;
        [SerializeField] private bool keepItPositive;
        
        private int _currentCoinAmount;
        
        private void Start()
        {
            CurrentCoinAmount = startingCoinAmount;
        }
        
                
        //TESTING TODO apagar assim que a coleta de itens for totalmente implementada
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                AddCoin(100);
            }

            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                SubtractCoin(100);
            }
        }
        //-------------------------------------//

        public int AddCoin(int coinAddition)
        {
            CurrentCoinAmount += coinAddition;
            return CurrentCoinAmount;
        }
        
        public int SubtractCoin(int subtrahend)
        {
            CurrentCoinAmount -= subtrahend;
            return CurrentCoinAmount;
        }
        
        private int CurrentCoinAmount
        {
            get => _currentCoinAmount;
            set
            {
                _currentCoinAmount = value;
                if (keepItPositive == true)
                {
                    _currentCoinAmount = Mathf.Clamp(_currentCoinAmount, 0, maxCoinAmount);
                }
                OnCoinAmountChanged();

            } 
        }
        
        protected virtual void OnCoinAmountChanged(CoinAmountChangedEventArgs e = null)
        {
            if (e == null)
            {
                var args = new CoinAmountChangedEventArgs
                {
                    CoinAmount = CurrentCoinAmount
                };
                CoinAmountChanged?.Invoke(this, args);
            }
            else
            {
                 CoinAmountChanged?.Invoke(this, e);
            }

        }
        

        
        public event EventHandler<CoinAmountChangedEventArgs> CoinAmountChanged;

        public class CoinAmountChangedEventArgs : EventArgs
        {
            public int CoinAmount {get; set;}
        }
    }
}
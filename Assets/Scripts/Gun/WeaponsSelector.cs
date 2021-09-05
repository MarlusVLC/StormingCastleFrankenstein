using System;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace Gun
{
    public class WeaponsSelector : MonoBehaviour
    {
        private GameObject[] _weapons;

        public event Action<int> OnWeaponChanged;

        private void Awake()
        {
            transform.TryGetChildren(out _weapons);
        }

        private void Start()
        {
            OnWeaponChanged?.Invoke(0);
        }

        private void OnEnable()
        {
            InputFilter.Instance.OnNumericValueReceived += SwitchTo;
        }
        
        private void OnDisable()
        {
            InputFilter.Instance.OnNumericValueReceived -= SwitchTo;
        }
        
        private void SwitchTo(int receivedValue)
        {
            if (receivedValue < 1 || receivedValue > _weapons.Length) return;
            
            var selection = receivedValue - 1;
            ExclusivelyActivate(ref _weapons, selection);
            OnWeaponChanged?.Invoke(selection);
        }

    }
    
}
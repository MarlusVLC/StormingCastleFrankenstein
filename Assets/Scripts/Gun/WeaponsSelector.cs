using System;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace Gun
{
    public class WeaponsSelector : MonoBehaviour
    {
        private GameObject[] _weapons;
        private ProjectileGun _currentWeapon;
        private ProjectileGun _previousWeapon;
        
        public ProjectileGun CurrentWeapon => _currentWeapon;


        private void Awake()
        {
            transform.TryGetChildren(out _weapons);
        }

        private void Start()
        {
            SwitchTo(1);
        }

        private void OnEnable()
        {
            InputFilter.Instance.OnNumericValueReceived += SwitchTo;
        }
        
        private void OnDisable()
        {
            if (InputFilter.HasInstance())
            {
                InputFilter.Instance.OnNumericValueReceived -= SwitchTo;
            }   
        }
        
        private void SwitchTo(int receivedValue)
        {
            if (receivedValue < 1 || receivedValue > _weapons.Length) return;

            var selection = receivedValue - 1;
            ExclusivelyActivate(ref _weapons, selection);
            _previousWeapon = _currentWeapon;
            _currentWeapon = _weapons[selection].GetComponent<ProjectileGun>();
            OnWeaponChanged?.Invoke(new WeaponChangedEventArgs
            {
                Position = selection, PreviousWeapon = _previousWeapon, CurrentWeapon = _currentWeapon
            });
        }

        public event Action<WeaponChangedEventArgs> OnWeaponChanged;
        public class WeaponChangedEventArgs : EventArgs
        {
            public int Position { get; set; }
            public ProjectileGun PreviousWeapon { get; set; }
            public ProjectileGun CurrentWeapon { get; set; }
        }

    }
    
}
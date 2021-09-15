using System;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace Gun
{
    public abstract class WeaponHandler : MonoBehaviour
    {
        private GameObject[] _weapons;
        private ProjectileGun _currentWeapon;
        private ProjectileGun _previousWeapon;
        
        public ProjectileGun CurrentWeapon => _currentWeapon;


        protected virtual void Awake()
        {
            transform.TryGetChildren(out _weapons);
        }

        protected virtual void Start()
        {
            SwitchTo(1);
        }

        protected virtual void SwitchTo(int receivedValue)
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
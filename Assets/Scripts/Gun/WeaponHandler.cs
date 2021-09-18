using System;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace Gun
{
    public class WeaponHandler : MonoBehaviour
    {
        private GameObject[] _weapons;
        private ProjectileGun _previousWeapon;
        
        public ProjectileGun CurrentWeapon { get; private set; }


        protected virtual void Awake()
        {
            transform.TryGetChildren(out _weapons);
        }

        protected virtual void Start()
        {
            SwitchTo(2);
        }

        protected virtual void SwitchTo(int receivedValue)
        {
            if (receivedValue < 1 || receivedValue > _weapons.Length) return;

            var selection = receivedValue - 1;
            ExclusivelyActivate(ref _weapons, selection);
            _previousWeapon = CurrentWeapon;
            CurrentWeapon = _weapons[selection].GetComponent<ProjectileGun>();
            OnWeaponChanged?.Invoke(new WeaponChangedEventArgs
            {
                Position = selection, PreviousWeapon = _previousWeapon, CurrentWeapon = CurrentWeapon
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
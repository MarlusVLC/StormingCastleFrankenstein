using System;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private int initialWeaponIndex = 1;
        [SerializeField] private LayerMask damageableLayer;

        private GameObject[] _weapons;
        private Gun _previousWeapon;
        
        public Gun CurrentWeapon { get; private set; }
        public Gun[] ConfirmedGuns { get; private set; }


        protected virtual void Awake()
        {
            transform.TryGetChildren(out _weapons);
            ConfirmedGuns = new Gun[_weapons.Length];
            for (var i = 0; i < ConfirmedGuns.Length; i++)
            {
                ConfirmedGuns[i] = _weapons[i].GetComponent<Gun>();
            }
        }

        protected virtual void Start()
        {
            SwitchTo(initialWeaponIndex);
        }

        protected virtual void SwitchTo(int receivedValue)
        {
            if (receivedValue < 1 || receivedValue > _weapons.Length)
            {
                Debug.LogException(new ArgumentException("initialWeaponIndex must be greater than 0 and lower than " 
                                                               + gameObject.name +
                                                               "'s child amount"));
            }

            var selection = receivedValue - 1;
            ExclusivelyActivate(ref _weapons, selection);
            _previousWeapon = CurrentWeapon;
            CurrentWeapon = ConfirmedGuns[selection];
            OnWeaponChanged?.Invoke(new WeaponChangedEventArgs
            {
                Position = selection, 
                PreviousWeapon = _previousWeapon, 
                CurrentWeapon = CurrentWeapon
            });
        }
        
        public virtual WeaponHandler Fire(bool continuouslyFire, Ray ray)
        {
            CurrentWeapon.PullTrigger(continuouslyFire, ray, damageableLayer);
            return this;
        }

        public event Action<WeaponChangedEventArgs> OnWeaponChanged;
        public class WeaponChangedEventArgs : EventArgs
        {
            public int Position { get; set; }
            public Gun PreviousWeapon { get; set; }
            public Gun CurrentWeapon { get; set; }
        }

    }
    
}
using System;
using Audio;
using UnityEngine;
using Utilities;

namespace Weapons
{
    public abstract class Gun : MonoCache
    {
        [Header("Gun Stats")]
        [SerializeField] protected Transform attackPoint;
        [SerializeField] protected bool isAutomatic;
        [SerializeField] protected float timeBetweenShooting;
        [SerializeField] protected int damage;
        [Header("Ammo")]
        [SerializeField] protected int magazineSize;
        [SerializeField] protected int startingAmmo;
        [Header("Debugging")] 
        [SerializeField] protected bool allowInvoke = true;
        [SerializeField] protected bool _hasUnlimitedAmmo;

        public bool isUnlocked;
        
        // protected WeaponAudio _weaponAudio;
        protected int _bulletsLeft;
        protected bool _readyToShoot;

        protected override void Awake()
        {
            base.Awake();
            _readyToShoot = true;

            if (_hasUnlimitedAmmo)
            {
                Debug.LogWarning("This weapon: " + gameObject.name + " has unlimited ammo");
            }
        }

        protected virtual void OnDisable()
        {
            ResetShot();
        }

        public abstract Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer);
        protected abstract void Shoot(bool shooting, Ray ray, LayerMask damageableLayer); 
        
        protected virtual void ResetShot()
        {
            _readyToShoot = true;
            allowInvoke = true;
        }
        
        public int AddRelativeAmmo(float valueRelativeToMax)
        {
            _bulletsLeft += Mathf.CeilToInt(valueRelativeToMax * magazineSize);
            _bulletsLeft = Mathf.Clamp(_bulletsLeft, 0, magazineSize);
            OnAmmoChanged();
            return _bulletsLeft;
        }

        public Gun ConsumeAmmo(float quantity = 1)
        {
            if (_hasUnlimitedAmmo) return this;
            _bulletsLeft = (int)(_bulletsLeft-quantity);
            return this;
        }

        public void OnAmmoChanged()
        {
            AmmoChanged?.Invoke(ShotsLeft);
        }
        
        public event Action<int> AmmoChanged;

        public void OnShotFired()
        {
            ShotFired?.Invoke();
        }
        
        public event Action ShotFired;
        
        public abstract int ShotsLeft {get;}

        public int MagazineSize => magazineSize;

        public bool IsEmpty => _bulletsLeft <= 0;
        
        public bool IsAutomatic => isAutomatic;

        public bool HasUnlimitedAmmo 
        {
            get => _hasUnlimitedAmmo;
            set => _hasUnlimitedAmmo = value;
        }
    }
}
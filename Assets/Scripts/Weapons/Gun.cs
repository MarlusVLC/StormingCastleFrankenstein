using System;
using Audio;
using UnityEngine;
using Utilities;

namespace Weapons
{
    [RequireComponent(typeof(WeaponAudio))]
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
        protected WeaponAudio _weaponAudio;
        protected int _bulletsLeft;
        protected bool _readyToShoot;

        protected override void Awake()
        {
            base.Awake();
            _weaponAudio = GetComponent<WeaponAudio>();
            _readyToShoot = true;
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
            Debug.Log((int)(valueRelativeToMax * magazineSize));
            _bulletsLeft += Mathf.CeilToInt(valueRelativeToMax * magazineSize);
            _bulletsLeft = Mathf.Clamp(_bulletsLeft, 0, magazineSize);
            OnAmmoChanged();
            return _bulletsLeft;
        }

        public void OnAmmoChanged()
        {
            AmmoChanged?.Invoke(ShotsLeft);
        }
        public abstract int ShotsLeft {get;}

        public int MagazineSize => magazineSize;

        public bool IsEmpty => _bulletsLeft <= 0;
        public bool IsAutomatic => isAutomatic;
        public event Action<int> AmmoChanged;
    }
}
using System;
using Audio;
using UnityEngine;
using UnityEngine.VFX;
using Utilities;
using Random = UnityEngine.Random;

namespace Weapons
{
        public class ProjectileGun : Gun
    {
        [Header("Bullet")]
        [SerializeField] private GameObject bullet;
        [Header("Bullet Force")]
        [SerializeField] private float shootForce;
        [SerializeField] private float upwardForce;
        [Header("Spreading-Fire Specs")]
        [SerializeField] private float spread;
        [Min(1)][SerializeField] private int bulletFragments;
        [Header("Visual Effects")] 
        [SerializeField] private VisualEffect muzzleFlash;
        
        private int _bulletsShot;
        private bool _hasMuzzleFlash;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            _hasMuzzleFlash = muzzleFlash != null;
            if (_hasMuzzleFlash) muzzleFlash.Stop();
        }

        private void OnEnable()
        {
            if (_hasMuzzleFlash) muzzleFlash.Stop();
        }

        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                // set bullets shot to 0
                _bulletsShot = 0;
                Shoot(shooting, ray, damageableLayer);
                if (_hasMuzzleFlash) muzzleFlash.Play();
                // play shooting sound
                FindObjectOfType<ProjectileSound>().PlayProjectileSound();
            }
            if (shooting && _bulletsLeft <= 0)
            {
                FindObjectOfType<ProjectileSound>().PlayProjectileEmptySound();
            }
            return this;
        }

        protected override void Shoot(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            _readyToShoot = false;
            var attackPosition = attackPoint.position;

            // check if ray hit something
            var targetPoint 
                = Physics.Raycast(ray, out var hit) 
                    ? hit.point 
                    : ray.GetPoint(75);
            
            // calculate direction from attackPoint to targetPoint
            var trajectoryDirection = targetPoint - attackPosition;

            for (var i = 0; i < bulletFragments; i++)
            {
                float spreadX = Random.Range(-spread, spread);
                float spreadY = Random.Range(-spread, spread);
                
                trajectoryDirection += new Vector3(spreadX, spreadY, 0);
                
                var currentBullet = Instantiate(bullet, attackPosition, Quaternion.identity);
                currentBullet.GetComponent<BulletImpact>()
                    .Fire(trajectoryDirection.normalized, shootForce, upwardForce)
                    .SetUncollidableMask(damageableLayer)
                    .SetDamage(damage/bulletFragments);
            }


            ConsumeAmmo();
            OnAmmoChanged();
            
            // invoke resetShot function (if not already invoked)
            if (allowInvoke)
            {
                StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
                allowInvoke = false;
            }
        }

        public override int ShotsLeft => _bulletsLeft;

    }

}

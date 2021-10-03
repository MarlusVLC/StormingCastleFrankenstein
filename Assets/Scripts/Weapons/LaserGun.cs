using System;
using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using Utilities;

namespace Weapons
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserGun : Gun
    {
        // laser
        [SerializeField] private VisualEffect _visualEffect;
        private bool _isVfxGhostgunPlaying;
        private bool _canPlayVfxGhostGunStart;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            _canPlayVfxGhostGunStart = true;
            _visualEffect.enabled = true;
            _visualEffect.Stop();
        }

        private void OnEnable()
        {
            _visualEffect.Stop();
        }

        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                Shoot(shooting, ray, damageableLayer);
                if (_isVfxGhostgunPlaying == false)
                    PlayVfxGhostGun();
                _weaponAudio.ShotWithShell();
            }
            
            if (_isVfxGhostgunPlaying && (!shooting || IsEmpty)) 
                StopVfxGhostGun();

            return this;
        }

        protected override void Shoot(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            _readyToShoot = false;
            // var attackPosition = attackPoint.position;

            // Vector3 targetPoint;
            if (Physics.Raycast(ray, out var hit))
            {
                // targetPoint = hit.point;
                if (hit.collider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(damage);
                }
            }
            // else
            // {
            //     targetPoint = ray.GetPoint(75);
            // }

            // _bulletsLeft = (int) (_bulletsLeft - 1 * Time.deltaTime);
            ConsumeAmmo(1 * Time.deltaTime);
            OnAmmoChanged();


            
            // invoke resetShot function (if not already invoked)
            if (allowInvoke)
            {
                StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
                allowInvoke = false;
            }
        }

        private void PlayVfxGhostGun()
        {
           _visualEffect.Play();
           _isVfxGhostgunPlaying = true;
        }

        private void StopVfxGhostGun()
        {
            _visualEffect.Stop();
            _isVfxGhostgunPlaying = false;
        }

        public override int ShotsLeft => 100*_bulletsLeft/magazineSize;
        // protected bool CanPlayLaserVFX => _isVfxGhostgunPlaying == false && _bulletsLeft > 0;

    }

}
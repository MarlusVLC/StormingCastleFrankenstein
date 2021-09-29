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
        // private LineRenderer line;
        private bool isVfxGhostgunPlaying;
        private bool canPlayVfxGhostGunStart;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            
            // laser
            // line = GetComponent<LineRenderer>();
            // line.positionCount = 2;
            // line.startWidth = 0.2f;
            canPlayVfxGhostGunStart = true;
            
            
            _visualEffect.Stop();
        }

        // private void Update()
        // {
        //     if (isVfxGhostgunPlaying && 
        //         (Input.GetMouseButtonUp(0) || IsEmpty))
        //     {
        //        StopVfxGhostGun();
        //     }
        // }
        
        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                Shoot(shooting, ray, damageableLayer);
                if (isVfxGhostgunPlaying == false)
                    PlayVfxGhostGun();
                _weaponAudio.ShotWithShell();
            }
            
            if (isVfxGhostgunPlaying && (!shooting || IsEmpty)) 
                StopVfxGhostGun();

            return this;
        }

        protected override void Shoot(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            _readyToShoot = false;
            var attackPosition = attackPoint.position;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out var hit))
            {
                targetPoint = hit.point;
                if (hit.collider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(damage);
                }
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            _bulletsLeft = (int) (_bulletsLeft - 1 * Time.deltaTime);
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
           isVfxGhostgunPlaying = true;
        }

        private void StopVfxGhostGun()
        {
            _visualEffect.Stop();
            isVfxGhostgunPlaying = false;
        }

        public override int ShotsLeft => 100*_bulletsLeft/magazineSize;
        protected bool CanPlayLaserVFX => isVfxGhostgunPlaying == false && _bulletsLeft > 0;

    }

}
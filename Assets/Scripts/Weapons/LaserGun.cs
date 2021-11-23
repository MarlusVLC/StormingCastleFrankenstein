using System;
using System.Collections;
using Audio;
using Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;
using Utilities;

namespace Weapons
{
    public class LaserGun : Gun
    {
        // laser
        [SerializeField] private VisualEffect _visualEffect;
        [SerializeField] private GameObject liquidObject;
        private bool _isVfxGhostgunPlaying;
        private bool _canPlayVfxGhostGunStart;
        private bool _canDealDamage;
        
        private Material liquid;

        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            _canPlayVfxGhostGunStart = true;
            _visualEffect.enabled = true;
            _visualEffect.Stop();
            liquid = liquidObject.GetComponent<Renderer>().material;
            liquid.SetFloat("_Fill_Liquid", ShotsLeft);
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
            }

            if (_isVfxGhostgunPlaying && (!shooting || IsEmpty))
            {
                StopVfxGhostGun();
                _canDealDamage = false;
            } 
                

            return this;
        }

        protected override void Shoot(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            _readyToShoot = false;

            if (Physics.Raycast(ray, out var hit))
            {
                // Debug.DrawRay();
                if (hit.collider.TryGetComponent(out Health health))
                {
                    if (_canDealDamage) health.TakeDamage(damage);
                }
            }

            ConsumeAmmo(1 * Time.deltaTime);
            liquid.SetFloat("_Fill_Liquid", 
                FloatExtensions.Remap((float)ShotsLeft/100,0,1,0.5f,0.6f));
            
            OnAmmoChanged();
            OnShotFired();

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
           StartCoroutine(DamageDelay());
           FindObjectOfType<LaserSound>().PlayLaserSound();
        }

        private void StopVfxGhostGun()
        {
            _visualEffect.Stop();
            _isVfxGhostgunPlaying = false;
        }

        public override int ShotsLeft => 100*_bulletsLeft/magazineSize;
        
        private IEnumerator DamageDelay()
        {
            yield return new WaitForSeconds(2f);
            _canDealDamage = true;
        }
    }

}
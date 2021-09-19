using System.Collections;
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
        [SerializeField] private VisualEffect vfxGhostgunLoop;
        [SerializeField] private VisualEffect vfxGhostgunFadeIn;
        [SerializeField] private VisualEffect vfxGhostgunFadeOut;
        [SerializeField] private Material material;
        private LineRenderer line;
        private bool isVfxGhostgunPlaying;
        private bool canPlayVfxGhostGunStart;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            
            // laser
            line = GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.startWidth = 0.2f;
            canPlayVfxGhostGunStart = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isVfxGhostgunPlaying = false;
            }
        }
        
        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);

            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                Shoot(ray, damageableLayer);
                _weaponAudio.ShotWithShell();
            }
            
            if (!shooting || _bulletsLeft <= 0) 
                line.enabled = false;

            return this;
        }

        protected override void Shoot(Ray ray, LayerMask damageableLayer)
        {
            _readyToShoot = false;
            var attackPosition = attackPoint.position;
            
            
            // check if ray hit something
            var targetPoint 
                = Physics.Raycast(ray, out var hit) 
                    ? hit.point 
                    : ray.GetPoint(75);

            // instantiate laser
            if (Input.GetMouseButton(0))
            {
                line.enabled = true;
                line.SetPosition(0, attackPosition);
                line.SetPosition(1, targetPoint);
                line.material = material;
                line.shadowCastingMode = ShadowCastingMode.On;
            }
            
            // if (canPlayVfxGhostGunStart) StartCoroutine(PlayVfxGhostGun());

            _bulletsLeft = (int) (_bulletsLeft - 1 * Time.deltaTime);
            OnAmmoChanged();
            
            // invoke resetShot function (if not already invoked)
            if (allowInvoke)
            {
                StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
                allowInvoke = false;
            }
        }

        private IEnumerator PlayVfxGhostGun()
        {
            print("started coroutine");
            canPlayVfxGhostGunStart = false;
            vfxGhostgunFadeIn.Play();
            
            yield return new WaitForSeconds(1);
            
            print("waited for 1 second");
            if (!isVfxGhostgunPlaying)
            {
                print("vfxGhostgunLoop");
                vfxGhostgunLoop.Play();
                isVfxGhostgunPlaying = true;
                yield return new WaitForSeconds(1);
                isVfxGhostgunPlaying = false;
            }
            
            if (Input.GetMouseButtonUp(1))
            {
                isVfxGhostgunPlaying = false;
                vfxGhostgunFadeOut.Play();
                canPlayVfxGhostGunStart = true;
                StopCoroutine(PlayVfxGhostGun());
            }
        }

        public override int ShotsLeft => 100*_bulletsLeft/magazineSize;

    }

}
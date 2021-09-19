using UnityEngine;
using Utilities;

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
        [SerializeField] private float timeBetweenShots;
        [SerializeField] private int bulletsPerTap;
        
        private int _bulletsShot;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo * bulletsPerTap;
        }

        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                // set bullets shot to 0
                _bulletsShot = 0;
                Shoot(ray, damageableLayer);
                // play shooting sound
                _weaponAudio.ShotWithShell();
            }
            if (shooting && _bulletsLeft <= 0)
            {
                _weaponAudio.EmptySfx();
            }
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
            
            // calculate direction from attackPoint to targetPoint
            var directionWithoutSpread = targetPoint - attackPosition;
            
            // calculate spread
            float x = UnityEngine.Random.Range(-spread, spread);
            float y = UnityEngine.Random.Range(-spread, spread);
            
            // calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
            
            // instantiate bullet/projectile
            var currentBullet = Instantiate(bullet, attackPosition, Quaternion.identity);
            currentBullet.GetComponent<BulletImpact>()
                .Fire(directionWithSpread.normalized, shootForce, upwardForce)
                .SetUncollidableMask(damageableLayer)
                .SetDamage(damage/bulletsPerTap);
            
            _bulletsLeft--;
            _bulletsShot++;
            OnAmmoChanged();
            
            // invoke resetShot function (if not already invoked)
            if (allowInvoke)
            {
                StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
                allowInvoke = false;
            }
            
            // if more than one bulletPerTap make sure to repeat shoot function
            if (_bulletsShot < bulletsPerTap && _bulletsLeft > 0)
                StartCoroutine(Parallel.ExecuteActionWithDelay(() => Shoot(ray, damageableLayer), timeBetweenShots));
        }

        public override int ShotsLeft => _bulletsLeft / bulletsPerTap;

    }

}

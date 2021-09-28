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
        [Min(1)][SerializeField] private int bulletFragments;
        
        private int _bulletsShot;
        
        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
        }

        public override Gun PullTrigger(bool shooting, Ray ray, LayerMask damageableLayer)
        {
            // shooting
            if (_readyToShoot && shooting && _bulletsLeft > 0)
            {
                // set bullets shot to 0
                _bulletsShot = 0;
                Shoot(shooting, ray, damageableLayer);
                // play shooting sound
                FindObjectOfType<AudioManager>().Play("Shot");
            }
            if (shooting && _bulletsLeft <= 0)
            {
                _weaponAudio.EmptySfx();
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


            _bulletsLeft--;
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

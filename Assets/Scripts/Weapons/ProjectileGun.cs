
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
        [Header("Enemy Layer")]
        [SerializeField] private LayerMask enemyLayer;

        private Animator animator;
        
        private GameObject npcObject;
        
        private int _bulletsShot;
        private bool _hasMuzzleFlash;
        private bool _enemyGun;
        private static readonly int PlayShootAnimation = Animator.StringToHash("PlayShootAnimation");

        protected override void Awake()
        {
            base.Awake();
            _bulletsLeft = startingAmmo;
            _hasMuzzleFlash = muzzleFlash != null;
            if (_hasMuzzleFlash) muzzleFlash.Stop();
            
            npcObject = FindParentInLayer(gameObject, enemyLayer);
            
            if (npcObject != null)
            {
                _enemyGun = true;
            }
            else
            {
                _enemyGun = false;
            }

            animator = GetComponent<Animator>();
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
                
                // shooting sounds
                if (_enemyGun)
                {
                    npcObject.GetComponent<AudioSource>().Play();
                }
                else
                {
                    FindObjectOfType<ProjectileSound>().PlayProjectileSound();   
                }
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
            OnShotFired();
            
            // invoke resetShot function (if not already invoked)
            if (allowInvoke)
            {
                StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
                allowInvoke = false;
            }
            
            // animations
            if (animator.gameObject.activeSelf)
            {
                animator.SetTrigger(PlayShootAnimation);
            }
        }

        private static GameObject FindParentInLayer(GameObject childObject, LayerMask layer)
        {
            Transform t = childObject.transform;
            while (t.parent != null)
            {
                if (layer.HasLayerWithin(t.parent.gameObject.layer) && t.parent.gameObject.GetComponent<AudioSource>())
                {
                    return t.parent.gameObject;
                }
                t = t.parent.transform;
            }
            return null;
        }

        public override int ShotsLeft => _bulletsLeft;
    }
}

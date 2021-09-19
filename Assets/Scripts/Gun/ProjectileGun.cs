using System;
using Audio;
using Gun;
using UnityEngine;
using Utilities;

public class ProjectileGun : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    [Header("Bullet Force")]
    [SerializeField] private float shootForce;
    [SerializeField] private float upwardForce;
    [Header("Gun Stats")]
    [SerializeField] private float timeBetweenShooting; 
    [SerializeField] private float spread;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;
    [SerializeField] private int startingAmmo;
    [SerializeField] private int damage;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private bool isAutomatic;
    [Header("Bug Fixing")]
    [SerializeField] private bool allowInvoke = true;
    
    private WeaponAudio _weaponAudio;
    public int bulletsLeft; 
    private int _bulletsShot;
    private bool _readyToShoot;
    
    private void Awake()
    {
        _weaponAudio = GetComponent<WeaponAudio>();
        bulletsLeft = startingAmmo * bulletsPerTap;
        _readyToShoot = true;
    }
    
    private void OnDisable()
    {
        ResetShot();
    }
    
    public void TriggerGun(bool shooting, Ray ray, LayerMask damageableLayer)
    {
        // shooting
        if (_readyToShoot && shooting && bulletsLeft > 0)
        {
            // set bullets shot to 0
            _bulletsShot = 0;
            Shoot(ray, damageableLayer);
            // play shooting sound
            _weaponAudio.ShotWithShell();
        }
        if (shooting && bulletsLeft <= 0)
        {
            _weaponAudio.EmptySfx();
        }
    }

    private void Shoot(Ray ray, LayerMask damageableLayer)
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
        
        bulletsLeft--;
        _bulletsShot++;
        OnAmmoChanged?.Invoke(ShotsLeft);
        
        // invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
            allowInvoke = false;
        }
        
        // if more than one bulletPerTap make sure to repeat shoot function
        if (_bulletsShot < bulletsPerTap && bulletsLeft > 0)
            StartCoroutine(Parallel.ExecuteActionWithDelay(() => Shoot(ray, damageableLayer), timeBetweenShots));
    }

    private void ResetShot()
    {
        _readyToShoot = true;
        allowInvoke = true;
    }
    
    
    public int ShotsLeft => bulletsLeft / bulletsPerTap;
    public bool IsEmpty => bulletsLeft <= 0;
    public bool IsAutomatic => isAutomatic;
    public event Action<int> OnAmmoChanged;
}

using System;
using Audio;
using UI.Menus;
using UnityEngine;
using Utilities;

/// <summary>
/// TODO ALTO! Criar script GunHandler, que se comunique com WeaponSelector pra pegar a arma atual e
///     Lide com o input - Deve ser uma classe abstrata, da qual GunHandler_Player e GunHandler_Bot derivem.
///     
/// </summary>
public class ProjectileGun : MonoBehaviour
{
    
    //

    // bullet
    [SerializeField] private GameObject bullet;
    
    // bullet force
    [SerializeField] private float shootForce, upwardForce;
    
    // TODO BAIXO usar headers pra separar as variáveis no inspetor
    // TODO BAIXO separar variáveis públicas de privadas (serializadas sao consideradas públicas)
    // gun stats
    /// <summary>
    /// TODO BAIXO separar cada variável em sua própria linha (TOC? - sepa)
    /// </summary>
    [SerializeField] private float timeBetweenShooting, spread, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;

    public int bulletsLeft; 
    private int bulletsShot;
    
    // bools
    private bool shooting, readyToShoot;
    
    // reference
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;

    // bug fixing
    [SerializeField] private bool allowInvoke = true;
    
    // audio clips
    private WeaponAudio weaponAudio;

    public event Action<int> OnAmmoChanged;
    public int ShotsLeft => bulletsLeft / bulletsPerTap;


    private void Awake()
    {
        // make sure magazine is full
        weaponAudio = GetComponent<WeaponAudio>();
        bulletsLeft = magazineSize; //TODO criar uma variável startingAmmo para executar essa ação
        readyToShoot = true;
    }

    private void Update()
    {
        if (GamePause.IsPaused) return;
        ClickToShoot();
    }

    private void OnDisable()
    {
        ResetShot();
    }


    private void ClickToShoot()
    {
        //TODO ALTO! jogar essas booleanas pra fora do método, pra liberar outras formas de atirar 
        //Por exemplo: O bot nao usa o teclado para atirar
        
        // check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // shooting
        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            // set bullets shot to 0
            bulletsShot = 0;
            Shoot();
            // play shooting sound
            weaponAudio.ShotWithShell();
        }
        if (shooting && bulletsLeft <= 0)
        {
            weaponAudio.EmptySfx();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // just a ray through the middle of the screen
        RaycastHit hit;
        
        // check if ray hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); // just a point far away from the player
        
        // calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        
        // calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        
        // calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        
        // instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;
        
        // add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;
        OnAmmoChanged?.Invoke(ShotsLeft);
        
        // invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
            // Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        
        // if more than one bulletPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}

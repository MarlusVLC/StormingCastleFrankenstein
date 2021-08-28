using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileGun : MonoBehaviour
{
    // guns
    [SerializeField] private GameObject mainGun;
    [SerializeField] private GameObject shotgun;

    [SerializeField] private string currentGun;
    
    // bullet
    [SerializeField] private GameObject bullet;
    
    // bullet force
    [SerializeField] private float shootForce, upwardForce;
    
    // gun stats
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
    
    // gun UI text
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;

    // bug fixing
    [SerializeField] private bool allowInvoke = true;

    private void Awake()
    {
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        // check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // shooting
        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            // set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
        
        // changing the gun
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainGun.gameObject.SetActive(true);
            shotgun.gameObject.SetActive(false);
            
            text1.color = Color.red;
            text2.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mainGun.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(true);
            
            text1.color = Color.white;
            text2.color = Color.red;
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
        
        // invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
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

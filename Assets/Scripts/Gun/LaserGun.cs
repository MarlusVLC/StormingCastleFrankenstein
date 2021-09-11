using System;
using Audio;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using Utilities;

[RequireComponent(typeof(LineRenderer))]

public class LaserGun : MonoBehaviour
{
    // laser
    [SerializeField] private VisualEffect vfxGhostgun;
    [SerializeField] private Material material;
    private LineRenderer line;
    private bool isVfxGhostgunPlaying = false;

    
    // gun stats
    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private int magazineSize;

    public int bulletsLeft;
    
    // bools
    private bool shooting, readyToShoot;
    
    // reference
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;

    // bug fixing
    [SerializeField] private bool allowInvoke = true;

    public event Action<int> OnAmmoChanged;
    public int ShotsLeft => bulletsLeft;


    private void Awake()
    {
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
        // laser
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.2f;
    }

    private void Update()
    {
        ClickToShoot();

        if (Input.GetMouseButtonUp(0))
        {
            isVfxGhostgunPlaying = false;
        }
    }

    private void OnDisable()
    {
        ResetShot();
    }


    private void ClickToShoot()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);

        // shooting
        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            Shoot();
        }
        
        if (!shooting || bulletsLeft <= 0) 
        line.enabled = false;
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

        // instantiate laser
        if (Input.GetMouseButton(0))
        {
            line.enabled = true;
            line.SetPosition(0, attackPoint.position);
            line.SetPosition(1, targetPoint);
            line.material = material;
            line.shadowCastingMode = ShadowCastingMode.ShadowsOnly;

            if (!isVfxGhostgunPlaying)
            {
                vfxGhostgun.Play();
                isVfxGhostgunPlaying = true;
            }
        }
        
        bulletsLeft = (int) (bulletsLeft - 1 * Time.deltaTime);
        OnAmmoChanged?.Invoke(ShotsLeft);
        
        // invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            StartCoroutine(Parallel.ExecuteActionWithDelay(ResetShot, timeBetweenShooting));
            // Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}

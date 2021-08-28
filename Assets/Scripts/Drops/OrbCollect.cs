using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Debug = UnityEngine.Debug;

public class OrbCollect : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ProjectileGun _projectileGun;
    [SerializeField] private GameObject healthOrb;
    [SerializeField] private GameObject ammoOrb;

    private int healthAddAmount = 10;
    private int ammoAddAmount = 10;

    private void OnControllerColliderHit(ControllerColliderHit collider)
    {
        if (collider.gameObject.Equals(healthOrb))
        {
            Debug.Log("health collected");
            HealthCollect();
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.Equals(ammoOrb))
        {
            Debug.Log("ammo collected");
            AmmoCollect();
            Destroy(collider.gameObject);
        }
    }

    public void HealthCollect()
    {
        if (_playerHealth.currentHealth < _playerHealth.maxHealth)
        {
            _playerHealth.currentHealth += healthAddAmount;
        }
    }
    
    public void AmmoCollect()
    {
        _projectileGun.bulletsLeft += ammoAddAmount;
    }
}

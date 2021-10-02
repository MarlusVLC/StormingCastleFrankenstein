using System;
using UnityEngine;
using Weapons;

namespace GameManagement
{
    public class CheatCodes : MonoBehaviour
    {
        [SerializeField] private GameObject firstPersonPlayer;

        private PlayerHealth _playerHealth;
        private WeaponHandler _weaponHandler;

        private void Awake()
        {
            _playerHealth = firstPersonPlayer.GetComponent<PlayerHealth>();
            _weaponHandler = firstPersonPlayer.GetComponentInChildren<WeaponHandler>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        _playerHealth.IsImmortal = !_playerHealth.IsImmortal;
                        Debug.Log("Infinite health activated");
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        Array.ForEach(_weaponHandler.ConfirmedGuns, g => g.HasUnlimitedAmmo = !g.HasUnlimitedAmmo);
                        Debug.Log("Infinite ammo activated");
                    }
                }
            }
        }
    }
}
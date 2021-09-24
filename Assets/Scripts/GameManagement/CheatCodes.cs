using System;
using UnityEngine;

namespace GameManagement
{
    public class CheatCodes : MonoBehaviour
    {
        [SerializeField] private GameObject firstPersonPlayer;

        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth = firstPersonPlayer.GetComponent<PlayerHealth>();
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
                }
            }
        }
    }
}
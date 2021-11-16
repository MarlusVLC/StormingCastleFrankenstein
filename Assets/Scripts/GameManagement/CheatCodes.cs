using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Weapons;

namespace GameManagement
{
    public class CheatCodes : MonoBehaviour
    {
        [SerializeField] private GameObject firstPersonPlayer;
        [SerializeField] private TextMeshProUGUI cheatText;
        [SerializeField] private float displayTime = 0.2f;

        private PlayerHealth _playerHealth;
        private WeaponHandler _weaponHandler;
        private IEnumerator _displayTextRoutine;
        private WaitForSecondsRealtime _timer;

        private void Awake()
        {
            _playerHealth = firstPersonPlayer.GetComponent<PlayerHealth>();
            _weaponHandler = firstPersonPlayer.GetComponentInChildren<WeaponHandler>();
            _timer = new WaitForSecondsRealtime(displayTime);
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
                        if (_displayTextRoutine != null)
                        {
                            StopCoroutine(_displayTextRoutine);
                        }
                        _displayTextRoutine = DisplayText("health", _playerHealth.IsImmortal);
                        StartCoroutine(_displayTextRoutine);
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        Array.ForEach(_weaponHandler.ConfirmedGuns, g => g.HasUnlimitedAmmo = !g.HasUnlimitedAmmo);
                        if (_displayTextRoutine != null)
                        {
                            StopCoroutine(_displayTextRoutine);
                        }
                        _displayTextRoutine = DisplayText("ammo", _weaponHandler.CurrentWeapon.HasUnlimitedAmmo);
                        StartCoroutine(_displayTextRoutine);
                    }
                }
            }
        }

        private IEnumerator DisplayText(string element, bool isActivated)
        {
            string activetext = isActivated ? "ON" : "OFF";
            cheatText.text = $"Unlimited {element} {activetext}";
            cheatText.enabled = true;
            yield return _timer;
            cheatText.text = " ";
            cheatText.enabled = false;
        }
    }
}
using UI.Menus;
using UnityEngine;
using Utilities;

namespace Gun
{
    public class PlayerWeapons : WeaponHandler
    {
        private void Update()
        {
            if (GamePause.IsPaused) return;
            CurrentWeapon.ClickToShoot(CurrentWeapon.IsAutomatic
                ? Input.GetMouseButton(0)
                : Input.GetMouseButtonDown(0));
        }
        
        private void OnEnable()
        {
            InputFilter.Instance.OnNumericValueReceived += SwitchTo;
        }
        
        private void OnDisable()
        {
            if (InputFilter.HasInstance())
            {
                InputFilter.Instance.OnNumericValueReceived -= SwitchTo;
            }   
        }
    }
}
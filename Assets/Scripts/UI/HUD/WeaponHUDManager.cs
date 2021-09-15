using Gun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using static Utilities.GameObjectUtil;

namespace UI.HUD
{
    public class WeaponHUDManager : Singleton<WeaponHUDManager>
    {
        [SerializeField] private WeaponHandler weaponSelector;
        [SerializeField] private GameObject indexes;
        [SerializeField] private GameObject weaponIcons;
        [SerializeField] private GameObject ammoIcons;
        [SerializeField] private TextMeshProUGUI ammoCounter;


        private GameObject[] _weaponIcons;
        private GameObject[] _ammoIcons;
        private TextMeshProUGUI[] _indexes;


        private void Awake()
        {
            weaponIcons.transform.TryGetChildren(out _weaponIcons);
            ammoIcons.transform.TryGetChildren(out _ammoIcons);
            _indexes = indexes.transform.RetrieveComponentsInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            weaponSelector.OnWeaponChanged += ChangeWeaponIcon;
            weaponSelector.OnWeaponChanged += ChangeAmmoIcon;
            weaponSelector.OnWeaponChanged += ChangeIndex;
            weaponSelector.OnWeaponChanged += UpdateAmmoCounter;
        }
        
        private void OnDisable()
        {
            weaponSelector.OnWeaponChanged -= ChangeWeaponIcon;
            weaponSelector.OnWeaponChanged -= ChangeAmmoIcon;
            weaponSelector.OnWeaponChanged -= ChangeIndex;
            weaponSelector.OnWeaponChanged -= UpdateAmmoCounter;

        }

        private void ChangeWeaponIcon(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _weaponIcons.Length)
            {
                Debug.LogWarning("There's no icon associated with the weapon in the index: " + selection);
            }
            ExclusivelyActivate(ref _weaponIcons, selection);
        }

        private void ChangeAmmoIcon(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _ammoIcons.Length)
            {
                Debug.LogWarning("There's no ammo icon associated with the weapon in the index: " + selection);
            }
            ExclusivelyActivate(ref _ammoIcons, selection);
        }
        private void ChangeIndex(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _weaponIcons.Length)
            {
                Debug.LogWarning("There's no index associated with the weapon in the index: " + selection);
            }
            
            for (var i = 0; i < _indexes.Length; i++)
            {
                if (i != selection)
                {
                    _indexes[i].color = Color.white;
                }
                else
                {
                    _indexes[i].color = Color.red;
                }
            }
        }

        private void UpdateAmmoCounter(WeaponHandler.WeaponChangedEventArgs e)
        {
            ammoCounter.text = e.CurrentWeapon.ShotsLeft.ToString();
            if (e.PreviousWeapon != null)
            {
                e.PreviousWeapon.OnAmmoChanged -= UpdateAmmoCounter;
            }
            e.CurrentWeapon.OnAmmoChanged += UpdateAmmoCounter;
        }
        
        private void UpdateAmmoCounter(int ammo)
        {
            ammoCounter.text = ammo.ToString();
        }
    }
}
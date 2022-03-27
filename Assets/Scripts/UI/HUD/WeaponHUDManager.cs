using Weapons;
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
        private GameObject[] _indexes;
        private int _currentWeaponMagazineSize;


        private new void Awake()
        {
            weaponIcons.transform.TryGetChildren(out _weaponIcons);
            ammoIcons.transform.TryGetChildren(out _ammoIcons);
            indexes.transform.TryGetChildren(out _indexes);
        }

        // private void OnEnable()
        // {
        //     weaponSelector.OnWeaponChanged += ChangeWeaponIcon;
        //     weaponSelector.OnWeaponChanged += ChangeAmmoIcon;
        //     weaponSelector.OnWeaponChanged += ChangeIndex;
        //     weaponSelector.OnWeaponChanged += UpdateAmmoCounter;
        // }
        //
        // private void OnDisable()
        // {
        //     weaponSelector.OnWeaponChanged -= ChangeWeaponIcon;
        //     weaponSelector.OnWeaponChanged -= ChangeAmmoIcon;
        //     weaponSelector.OnWeaponChanged -= ChangeIndex;
        //     weaponSelector.OnWeaponChanged -= UpdateAmmoCounter;
        // }

        public void ChangeWeaponIcon(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _weaponIcons.Length)
            {
                Debug.LogWarning("There's no icon associated with the weapon in the index: " + selection);
            }

            if (weaponSelector.ConfirmedGuns[selection].IsUnlocked)
            {
                ExclusivelyActivate(ref _weaponIcons, selection);
            }
        }

        public void ChangeAmmoIcon(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _ammoIcons.Length)
            {
                Debug.LogWarning("There's no ammo icon associated with the weapon in the index: " + selection);
            }

            if (weaponSelector.ConfirmedGuns[selection].IsUnlocked)
            {
                ExclusivelyActivate(ref _ammoIcons, selection);
            }
        }
        public void ChangeIndex(WeaponHandler.WeaponChangedEventArgs e)
        {
            var selection = e.Position;
            if (selection > _weaponIcons.Length)
            {
                Debug.LogWarning("There's no index associated with the weapon in the index: " + selection);
            }
            
            // for (var i = 0; i < _indexes.Length; i++)
            // {
            //     if (i != selection)
            //     {
            //         _indexes[i].color = Color.white;
            //     }
            //     else
            //     {
            //         _indexes[i].color = Color.red;
            //     }
            // }
            
            ExclusivelyActivate(ref _indexes, selection);
        }

        public void UpdateAmmoCounter(WeaponHandler.WeaponChangedEventArgs e)
        {
            _currentWeaponMagazineSize = e.CurrentWeapon.MagazineSize;
            ammoCounter.text = $"{e.CurrentWeapon.ShotsLeft.ToString()} / {_currentWeaponMagazineSize.ToString()}";
            if (e.PreviousWeapon != null)
            {
                e.PreviousWeapon.AmmoChanged -= UpdateAmmoCounter;
            }
            e.CurrentWeapon.AmmoChanged += UpdateAmmoCounter;
        }
        
        public void UpdateAmmoCounter(int ammo)
        {
            ammoCounter.text = $"{ammo.ToString()} / {_currentWeaponMagazineSize.ToString()}" ;
        }
    }
}
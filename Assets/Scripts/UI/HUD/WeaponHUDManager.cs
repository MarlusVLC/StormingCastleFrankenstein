using Gun;
using TMPro;
using UnityEngine;
using Utilities;
using static Utilities.GameObjectUtil;

namespace UI.HUD
{
    public class WeaponHUDManager : MonoBehaviour
    {
        [SerializeField] private WeaponsSelector weaponSelector;
        [SerializeField] private GameObject indexes;
        [SerializeField] private GameObject icons;

        private GameObject[] _icons;
        private TextMeshProUGUI[] _indexes;

        
        private void Awake()
        {
            icons.transform.TryGetChildren(out _icons);
            _indexes = indexes.transform.RetrieveComponentsInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            weaponSelector.OnWeaponChanged += ChangeIcon;
            weaponSelector.OnWeaponChanged += ChangeIndex;
        }
        
        private void OnDisable()
        {
            weaponSelector.OnWeaponChanged -= ChangeIcon;
            weaponSelector.OnWeaponChanged -= ChangeIndex;

        }

        private void ChangeIcon(int selection)
        {
            if (selection > _icons.Length)
            {
                Debug.LogWarning("There's no icon associated with the weapon in the index: " + selection);
            }
            ExclusivelyActivate(ref _icons, selection);
        }
        
        private void ChangeIndex(int selection)
        {
            if (selection > _icons.Length)
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
    }
}
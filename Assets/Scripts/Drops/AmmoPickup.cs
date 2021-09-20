using Entities;
using UnityEngine;
using Utilities;
using Weapons;

namespace Drops
{
    public class AmmoPickup : MonoCache
    {
        [Range(0,1)][SerializeField] private float ammoPercentageRecover = 0.25f;
        [SerializeField] private LayerMask collectorMask;

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (collectorMask.HasLayerWithin(otherGameObject.layer))
            {
                //TODO corrigir pra um extensions de TryGetComponent
                var weaponHandler = otherGameObject.GetComponentInChildren<WeaponHandler>();
                // if (otherGameObject.TryGetComponent(out WeaponHandler weaponHandler))
                if (weaponHandler != null)
                {
                    weaponHandler.CurrentWeapon.AddRelativeAmmo(ammoPercentageRecover);
                    Destroy(gameObject);
                }
            }
        }
    }
}
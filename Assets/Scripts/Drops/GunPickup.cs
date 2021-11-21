using System;
using Entities;
using UnityEngine;
using Utilities;
using Weapons;

namespace Drops
{
    // fica no pickup
    public class GunPickup : MonoCache
    {
        public LayerMask collectorMask;
        
        [Range(1,4)]public int index;

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (collectorMask.HasLayerWithin(otherGameObject.layer))
            {
                var playerWeapons = otherGameObject.GetComponentInChildren<PlayerWeapons>();
                playerWeapons.Collect(index);
                Destroy(gameObject);
            }
        }
    }
}
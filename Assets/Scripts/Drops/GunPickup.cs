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
        
        public int index;

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (collectorMask.HasLayerWithin(otherGameObject.layer))
            {
                var playerWeapons = otherGameObject.GetComponentInChildren<PlayerWeapons>();
                playerWeapons.Collection((index));
                Destroy(gameObject);
            }
        }
    }
}
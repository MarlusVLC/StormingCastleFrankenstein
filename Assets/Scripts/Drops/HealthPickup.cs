using System;
using Entities;
using UnityEngine;
using Utilities;

namespace Drops
{
    public class HealthPickup : MonoCache
    {
        [SerializeField] private int healthRecover = 25;
        [SerializeField] private LayerMask collectorMask;

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (collectorMask.HasLayerWithin(otherGameObject.layer))
            {
                if (otherGameObject.TryGetComponent(out Health health))
                {
                    if (!health.IsFull)
                    {
                        health.RecoverHealth(healthRecover);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
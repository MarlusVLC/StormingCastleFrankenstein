using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Utilities;

namespace AI
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private LayerMask attackableMask;
        [SerializeField] private int attackDamage;

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("HIT!");

            if (attackableMask.HasLayerWihin(other.gameObject.layer) && other.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }
}


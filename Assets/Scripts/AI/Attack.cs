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
            if (attackableMask.HasLayerWithin(other.gameObject.layer) &&
                other.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(attackDamage);
            }
        }
    }
}


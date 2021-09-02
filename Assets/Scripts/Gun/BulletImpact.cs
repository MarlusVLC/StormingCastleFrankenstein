using Entities;
using UnityEngine;
using Utilities;

namespace Gun
{
    public class BulletImpact : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private LayerMask uncollidableMask;
        private void OnCollisionEnter(Collision collision)
        {
            var other = collision.gameObject;
            if (uncollidableMask.HasLayerWithin(other.layer)) return;
            
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
            Destroy(this.gameObject);

        }
    }
}
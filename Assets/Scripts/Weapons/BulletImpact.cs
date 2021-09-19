using Entities;
using UnityEngine;
using Utilities;

namespace Weapons
{
    public class BulletImpact : MonoBehaviour
    {
        [HideInInspector][SerializeField] private Rigidbody _rb;
        private int _damage;
        private LayerMask _damageableLayer;

        private void OnValidate()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb != null)
            {
                Debug.Log("Rigidbody caught");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var other = collision.gameObject;
            if (_damageableLayer.HasLayerWithin(other.layer) == false) return;
            
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
            Destroy(this.gameObject);
        }

        public BulletImpact Fire(Vector3 direction, float shootForce, float upwardForce)
        {
            transform.forward = direction;
            _rb.AddForce(direction * shootForce, ForceMode.Impulse);
            _rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
            return this;
        }

        public LayerMask UncollidableMask
        {
            get => _damageableLayer;
            set => _damageableLayer = value;
        }

        public BulletImpact SetUncollidableMask(LayerMask value)
        {
            UncollidableMask = value;
            return this;
        }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public BulletImpact SetDamage(int value)
        {
            _damage = value;
            return this;
        }
    }
}
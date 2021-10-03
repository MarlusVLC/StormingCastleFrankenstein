using System.Collections;
using Entities;
using UnityEngine;
using Utilities;

namespace Weapons
{
    public class BulletImpact : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private GameObject _ImpactFx;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private MeshRenderer _renderer;
        
        private int _damage;


        // private void OnValidate()
        // {
        //     _rb = GetComponent<Rigidbody>();
        //     if (_rb != null)
        //     {
        //         Debug.Log("Rigidbody caught");
        //     }
        //
        //     _ImpactFx = transform.GetChild(0).gameObject;
        //     if (_ImpactFx != null)
        //     {
        //         Debug.Log("Child caught");
        //     }
        //     
        //     _collider = GetComponent<SphereCollider>();
        //     if (_ImpactFx != null)
        //     {
        //         Debug.Log("Collider caught");
        //     }
        //     
        //     _renderer = GetComponent<MeshRenderer>();
        //     if (_ImpactFx != null)
        //     {
        //         Debug.Log("Renderer caught");
        //     }
        // }

        private void OnCollisionEnter(Collision collision)
        {
            var other = collision.gameObject;
            if (_damageableLayer.HasLayerWithin(other.layer) == false) return;
            
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }

            StartCoroutine(DisplayAndDestroy(0.5f));
        }

        public BulletImpact Fire(Vector3 direction, float shootForce, float upwardForce)
        {
            transform.forward = direction;
            _rb.AddForce(direction * shootForce, ForceMode.Impulse);
            _rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
            return this;
        }

        private IEnumerator DisplayAndDestroy(float delayTime)
        {
            _collider.enabled = false;
            _renderer.enabled = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _ImpactFx.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            Destroy(gameObject);
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
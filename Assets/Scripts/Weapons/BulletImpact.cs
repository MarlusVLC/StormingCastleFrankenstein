using System;
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
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private bool explodeBullet;
        
        private int _damage;

        // TODO: fix this. make the player not collide with the explosion
        // private void OnControllerColliderHit(ControllerColliderHit hit)
        // {
        //     var other = hit.gameObject;
        //     if (other.CompareTag("Player"))
        //     {
        //         Destroy(this);
        //     }
        // }

        private void OnCollisionEnter(Collision collision)
        {
            var other = collision.gameObject;
            if (_damageableLayer.HasLayerWithin(other.layer) == false) return;
            
            if (explodeBullet)
            {
                transform.localScale = new Vector3(10f, 10f, 10f);
            }
            
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
            _renderer.enabled = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _ImpactFx.SetActive(true);
            _ImpactFx.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

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
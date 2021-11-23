using System.Collections;
using System.Numerics;
using Entities;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Weapons
{
    public class BulletImpact : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private GameObject _ImpactFx;
        [SerializeField] private AudioClip[] impactSFX;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private bool explodeBullet;
        [SerializeField] private float explosionScale = 1000f;
        
        private int _damage;
        private AudioSource _audioSource;
        
        private void OnCollisionEnter(Collision collision)
        {
            TryPlayImpactSfx();
            
            var other = collision.gameObject;
            if (_damageableLayer.HasLayerWithin(other.layer) == false) return;
            
            if (explodeBullet)
            {
                transform.localScale = Vector3.one * explosionScale ;
            }
            
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }

            Instantiate(_ImpactFx, gameObject.transform);
            
            Destroy(gameObject);
        }

        public BulletImpact Fire(Vector3 direction, float shootForce, float upwardForce)
        {
            transform.forward = direction;
            _rb.AddForce(direction * shootForce, ForceMode.Impulse);
            _rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
            return this;
        }

        private bool TryPlayImpactSfx()
        {
            if (TryGetComponent(out _audioSource) && impactSFX.Length != 0)
            {
                _audioSource.PlayOneShot(impactSFX[Random.Range(0,impactSFX.Length)]);
            }

            return true;
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
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
            
            _ImpactFx.transform.position = gameObject.transform.position;
            Instantiate(_ImpactFx, gameObject.transform.parent);
            _ImpactFx.gameObject.SetActive(true);
            _ImpactFx.transform.position = gameObject.transform.position;

            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
            
            if (explodeBullet)
            {
                StartCoroutine(ExpandAndDestroy());
            }
            else
            {
                Destroy(gameObject);
            }
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

        private IEnumerator ExpandAndDestroy()
        {
            transform.localScale = Vector3.one * explosionScale;
            _ImpactFx.gameObject.transform.localScale = Vector3.one/explosionScale;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.3f);
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
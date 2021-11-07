using System.Collections;
using Audio;
using UnityEngine;

namespace AI
{
    public class MeleeBot : EnemyBot
    {
        [SerializeField] private Collider weaponCollider;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float jumpForce;

        private bool isAttacking;
        protected override void Awake()
        {
            base.Awake();
            if (rb == null)
            {
                TryGetComponent(out rb);
            }
        }
        protected override void Attack()
        {
            // _animator.SetBool("IsCloseToTarget", _distanceToTarget < StoppingDistance);
            
            
        }

        protected IEnumerator AttackProcess()
        {
            isAttacking = true;
            weaponCollider.enabled = isAttacking;
            rb.AddForce(Vector3.up * jumpForce);
            
        }
    }
}
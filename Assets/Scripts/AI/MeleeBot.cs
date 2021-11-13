using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.AI;

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
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (rb == null)
            {
                TryGetComponent(out rb);
            }
        }
        protected override void Attack()
        {
            _animator.SetBool("IsCloseToTarget", _distanceToTarget < StoppingDistance);
            // StartCoroutine(AttackProcess());
        }

        // protected IEnumerator AttackProcess()
        // {
        //     weaponCollider.enabled = isAttacking = true;
        //     _navMeshAgent.enabled = false;
        //     rb.AddForce(Vector3.up * jumpForce);
        //     while (isAttacking)
        //     {
        //         if (!isGrounded)
        //         {
        //             yield return new WaitUntil(() => isGrounded);
        //         }
        //         weaponCollider.enabled = isAttacking = false;
        //         _navMeshAgent.enabled = true;
        //     }
        // }
    }
}
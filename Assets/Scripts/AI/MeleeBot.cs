using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class MeleeBot : EnemyBot
    {
        [SerializeField] private Rigidbody rb;
        
        private bool isAttacking;
        private static readonly int IsCloseToTarget = Animator.StringToHash("IsCloseToTarget");

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
            _animator.SetBool(IsCloseToTarget, _distanceToTarget < StoppingDistance);
            // StartCoroutine(AttackProcess());
        }
    }
}
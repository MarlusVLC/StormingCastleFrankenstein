using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace AI
{
    public abstract class EnemyBot : MonoCache
    {
        protected FieldOfView _fov;
        protected NavMeshAgent _navMeshAgent;
        protected Animator _animator;

        protected float _currRotationSpeed;
        protected float _distanceToTarget;

        protected float MaximumRotationSpeed => _navMeshAgent.angularSpeed;
        protected float StoppingDistance => _navMeshAgent.stoppingDistance;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _navMeshAgent);
            TryGetComponent(out _animator);
            _fov = GetComponentInChildren<FieldOfView>();
        }

        protected virtual void OnEnable()
        {
            _fov.OnTargetAcquired += TrySetDestination;
        }

        protected virtual void OnDisable()
        {
            _fov.OnTargetAcquired -= TrySetDestination;
        }

        protected virtual void FixedUpdate()
        {
            if (_fov.HasTarget)
            {
                RotateTowardsTarget();
                Attack();
            }
        }

        protected virtual void TrySetDestination(Transform target)
        {
            var dirToTarget = (target.position - Transform.position).normalized;
            var playerToTargetAngle = Vector3.Angle(Transform.forward, dirToTarget);
            if (playerToTargetAngle < 45f)
            {
                _navMeshAgent.SetDestination(target.position);
            }
        }

        protected virtual void RotateTowardsTarget()
        {
            var targetPosition = _fov.FirstTarget.position;
            _distanceToTarget = Vector3.Distance(targetPosition, Transform.position);
            _currRotationSpeed = MaximumRotationSpeed * StoppingDistance  / _distanceToTarget;
            Transform.RotateTowards(targetPosition, _currRotationSpeed);
        }

        protected abstract void Attack();
    } 
}


using System;
using Audio;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace AI
{
    public abstract class EnemyBot : MonoCache
    {
        protected FieldOfView _fov;
        protected NavMeshAgent _navMeshAgent;
        protected Rigidbody _rb;
        protected Animator _animator;

        protected float _currRotationSpeed;
        protected float _distanceToTarget;
        protected float _angleToTarget;
        // protected bool

        protected float MaximumRotationSpeed => _navMeshAgent.angularSpeed;
        protected float StoppingDistance => _navMeshAgent.stoppingDistance;
        
        // sfx
        private bool _hasSoundBeenPlayed;

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
                if (IsTargetWithinSight(30f))
                {
                    Attack();
                }
            }
        }

        protected virtual void TrySetDestination(Transform target)
        {
            var dirToTarget = (target.position - Transform.position).normalized;
            _angleToTarget = Vector3.Angle(Transform.forward, dirToTarget);
            if (IsTargetWithinSight(45f))
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

        protected virtual bool IsTargetWithinSight(float maximumAngle)
        {
            if (!_hasSoundBeenPlayed)
            {
                FindObjectOfType<WendigoSound>().PlayWendigoAttackSound();
                _hasSoundBeenPlayed = true;
            }
            return _angleToTarget < maximumAngle;
        }

        private void Update()
        {
            if (_hasSoundBeenPlayed && !IsTargetWithinSight(45f))
            {
                _hasSoundBeenPlayed = false;
            }
        }

        protected abstract void Attack();
    } 
}


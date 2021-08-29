using System;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace AI
{
    public class SimpleBot : MonoCache
    {
        // [SerializeField] private float AngularSpeed_WhenTargetAcquired;
        // [SerializeField] private float AngularSpeed_OnTargetAcquired;
        private FieldOfView _fov;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private float _currRotationSpeed;
        private float _distanceToTarget;

        private float MaximumRotationSpeed => _navMeshAgent.angularSpeed;
        private float StoppingDistance => _navMeshAgent.stoppingDistance;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _navMeshAgent);
            TryGetComponent(out _animator);
            _fov = GetComponentInChildren<FieldOfView>();
        }

        private void OnEnable()
        {
            _fov.OnTargetAcquired += SetDestination;
        }

        private void OnDisable()
        {
            _fov.OnTargetAcquired -= SetDestination;

        }

        private void FixedUpdate()
        {
            if (_fov.HasTarget)
            {
                var targetPosition = _fov.FirstTarget.position;
                _distanceToTarget = Vector3.Distance(targetPosition, Transform.position);
                _currRotationSpeed = MaximumRotationSpeed * StoppingDistance  / _distanceToTarget;
                Transform.RotateTowards(targetPosition, _currRotationSpeed);
                _animator.SetBool("IsCloseToTarget", _distanceToTarget < StoppingDistance);
            }
        }

        private void SetDestination(Transform target)
        {
            var dirToTarget = (target.position - Transform.position).normalized;
            var playerToTargetAngle = Vector3.Angle(Transform.forward, dirToTarget);
            if (playerToTargetAngle < 45f)
            {
                _navMeshAgent.SetDestination(target.position);
            }
        }
    } 
}


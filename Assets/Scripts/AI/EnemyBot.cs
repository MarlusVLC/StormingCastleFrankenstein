using System;
using System.Collections;
using Audio;
using Entities;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using Random = UnityEngine.Random;

namespace AI
{
    public abstract class EnemyBot : AgileBeing
    {
        [SerializeField] protected EnemyType enemyType;
        [SerializeField] protected bool canFlee;

        protected FieldOfView _fov;
        protected NavMeshAgent _navMeshAgent;
        protected Animator _animator;
        
        protected Vector3 _targetPosition;
        protected float _currRotationSpeed;
        protected float _distanceToTarget;
        protected float _angleToTarget;
        protected bool _isFleeing;
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
            _fov.OnTargetAcquired += TryPursuit;
        }

        protected virtual void OnDisable()
        {
            _fov.OnTargetAcquired -= TryPursuit;
        }

        private void Update()
        {
            if (_hasSoundBeenPlayed && !IsTargetWithinSight(45f))
            {
                _hasSoundBeenPlayed = false;
            }
        }
        
        protected virtual void FixedUpdate()
        {
            if (_fov.HasTarget)
            {
                _targetPosition = _fov.FirstTarget.position;
                _distanceToTarget = Vector3.Distance(_targetPosition, Transform.position);
                RotateTowardsTarget();
                if (!IsFleeing)
                {
                    if (_distanceToTarget <= StoppingDistance && canFlee)
                    {
                        StartCoroutine(Flee(_targetPosition));
                    }
                    if (IsTargetWithinSight(30f))
                    {
                        Attack();
                    }
                }
            }
        }

        protected virtual void TryPursuit(Transform target)
        {
            if (IsFleeing) return;
            
            var dirToTarget = (target.position - Transform.position).normalized;
            _angleToTarget = Vector3.Angle(Transform.forward, dirToTarget);
            if (IsTargetWithinSight(45f))
            {
                _navMeshAgent.SetDestination(target.position);
                EnemySoundManager.Instance.PlayChaseSound(enemyType);
            }
            else
            {
                RotateTowardsTarget();
            }
        }

        protected virtual IEnumerator Flee(Vector3 targetPosition)
        {
            Vector3 fleeDirection = (Transform.position - targetPosition).normalized;
            _navMeshAgent.SetDestination(Transform.position + fleeDirection * StoppingDistance);
            _isFleeing = true;
            yield return new WaitUntil(_navMeshAgent.HasReachedDestination);
            _isFleeing = false;
        }

        protected virtual void RotateTowardsTarget()
        {
            _currRotationSpeed = MaximumRotationSpeed * StoppingDistance  / _distanceToTarget;
            Transform.RotateTowards(_targetPosition, _currRotationSpeed);
        }

        protected virtual bool IsTargetWithinSight(float maximumAngle)
        {
            if (!_hasSoundBeenPlayed)
            {
                EnemySoundManager.Instance.PlayAlertSound(enemyType);
                _hasSoundBeenPlayed = true;
            }
            return _angleToTarget < maximumAngle;
        }

        protected virtual void PlayRandomAudio(AudioSource audioSource, AudioClip[] sounds)
        {
            if (audioSource != null && sounds.Length > 0)
            {
                audioSource.PlayOneShot(sounds[Random.Range(0,sounds.Length)]);
            }
        }

        protected abstract void Attack();
        
        public EnemyType EnemyType => enemyType;

        protected float MaximumRotationSpeed => _navMeshAgent.angularSpeed;
        protected float StoppingDistance => _navMeshAgent.stoppingDistance;
        
        protected bool IsFleeing
        {
            get => _isFleeing && canFlee;
            set => _isFleeing = value;
        }
    }

    public enum EnemyType
    {
        Wendigo,
        Robot,
        Spider
    }
}


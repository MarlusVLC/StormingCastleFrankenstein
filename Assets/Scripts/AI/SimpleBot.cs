using System;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace AI
{
    public class SimpleBot : MonoCache
    {
        
        /// <summary>
        /// JÁ VORTO. FUI MUÇA///
        /// </summary>
        [SerializeField] private Transform mockDestination;
        
        private FieldOfView _fov;
        private NavMeshAgent _navMeshAgent;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _navMeshAgent);
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

        private void Start()
        {
            _navMeshAgent.SetDestination(mockDestination.position);
        }

        private void SetDestination(Transform transform)
        {
            _navMeshAgent.SetDestination(transform.position);
        }
    } 
}


using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace AI
{
    
    //Fonte: Sebastian Lague
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private string name;
        [SerializeField] private bool showCircumference;
        [SerializeField] private int numberOfExpectedTargets;
        [SerializeField] private float checkInterval;
        [Range(0,360)] [SerializeField] private float viewAngle;
        [SerializeField] float viewRadius; 
        [SerializeField] LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;
    
        public List<Transform> VisibleTargets { get; private set; }
        private Collider[] _targetsInViewRadius;
        private int _numberOfTargetsInRadius;
        
        public event Action<Transform> OnTargetAcquired = delegate {  };
        public event Action OnTargetLost;


        void Start()
        {
            _targetsInViewRadius = new Collider[numberOfExpectedTargets];
            VisibleTargets = new List<Transform>(numberOfExpectedTargets);

            StartCoroutine(Parallel.ExecuteActionWithDelay(FindVisibleTargets, checkInterval, true));
        }
        
        void OnValidate()
        {
            if (Application.isPlaying)
            {
                _targetsInViewRadius = new Collider[numberOfExpectedTargets];
                StartCoroutine(Parallel.ExecuteActionWithDelay(FindVisibleTargets, checkInterval, true));
            }
        }

        public void FindVisibleTargets()
        {
            VisibleTargets.Clear();

            _numberOfTargetsInRadius =
                Physics.OverlapSphereNonAlloc(transform.position, viewRadius, _targetsInViewRadius, targetMask);
            
            
            for (int i = 0; i < _numberOfTargetsInRadius; i++)
            {
                var target = _targetsInViewRadius[i].transform;
                var dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    var distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        VisibleTargets.Add(target);
                        OnTargetAcquired?.Invoke(target);
                    }
                }
            }

            if (VisibleTargets.Count < 1)
            {
                OnTargetLost?.Invoke();
            }
            
            
        }
        
        
        public float ViewAngle => viewAngle;
        public float ViewRadius => viewRadius;
        public bool ShowCircumference => showCircumference;
        public bool HasTarget => VisibleTargets.Count > 0;
        public Transform FirstTarget => VisibleTargets[0];
        public void InitializeTargetCollections()
        {
            _targetsInViewRadius = new Collider[numberOfExpectedTargets];
            VisibleTargets = new List<Transform>(numberOfExpectedTargets);
        }
    }  
}
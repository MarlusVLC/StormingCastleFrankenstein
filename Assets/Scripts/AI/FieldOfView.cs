using System;
using System.Collections;
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
        public bool HasTarget => VisibleTargets.Count > 0;

        private Collider[] _targetsInViewRadius;
        private int _numberOfTargetsInRadius;
        
        public event Action<Transform> OnTargetAcquired = delegate {  };


        void Start()
        {
            _targetsInViewRadius = new Collider[numberOfExpectedTargets];
            VisibleTargets = new List<Transform>(numberOfExpectedTargets);

            // StartCoroutine(FindTargetsWithDelay(checkInterval));
            StartCoroutine(Parallel.ExecuteActionWithDelay(FindVisibleTargets, checkInterval));
        }
        
        void OnValidate()
        {
            if (Application.isPlaying)
            {
                _targetsInViewRadius = new Collider[numberOfExpectedTargets];
                // StartCoroutine(FindTargetsWithDelay(checkInterval));
                StartCoroutine(Parallel.ExecuteActionWithDelay(FindVisibleTargets, checkInterval));
            }
        }
        
        
        
        // public IEnumerator FindTargetsWithDelay(float delay)
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(delay);
        //         FindVisibleTargets();
        //     }
        // }

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
            
            
        }
        

        // public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        // {
        //     if (!angleIsGlobal)
        //     {
        //         angleInDegrees += transform.eulerAngles.y;
        //     }
        //     return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0,
        //         Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        // }
        
        
        
        public float ViewAngle => viewAngle;
        public float ViewRadius => viewRadius;
        // public List<Transform> VisibleTargets => VisibleTargets;

        public bool ShowCircumference => showCircumference;

        public void InitializeTargetCollections()
        {
            _targetsInViewRadius = new Collider[numberOfExpectedTargets];
            VisibleTargets = new List<Transform>(numberOfExpectedTargets);
        }
    }  
}
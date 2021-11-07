using System;
using UnityEngine;
using Utilities;

namespace Entities
{
    public abstract class AgileBeing : MonoCache
    {
        [Header("Agile settings")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
        
        protected bool isGrounded;
        
        protected virtual void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
    }
}
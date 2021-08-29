using UnityEngine;
using UnityEngine.AI;

namespace Utilities
{
    public static class NavMeshAgentExtensions
    {
        public static bool HasReachedDestination(this NavMeshAgent _navMeshAgent)
        {
            return !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance &&
                   (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f);
        }
    }
}
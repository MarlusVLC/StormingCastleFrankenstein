using UnityEngine;

namespace Utilities
{
    public static class TransformExtensions
    {
        public static Vector3 DirFromAngle(this Transform transform, float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0,
                Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        public static void RotateTowards(this Transform transform, Vector3 targetPos, float speed)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPos - transform.position,
                speed * Time.deltaTime, 0.0f));
        }

        public static T[] RetrieveComponentsInChildren<T>(this Transform transform) where T : MonoBehaviour
        {
            var childrenComponents = new List<T>(transform.childCount);
            T component;
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out component))
                {
                    childrenComponents.Add(component);
                }
            }
            return childrenComponents.ToArray();
        }

        public static int TryGetChildren(this Transform transform, out GameObject[] children)
        {
            children = new GameObject[transform.childCount];
            var i = 0;
            foreach (Transform child in transform)
            {
                children[i] = child.gameObject;
                i++;
            }

            return children.Length;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace UI.Interfaces
{
    public class Counter : MonoBehaviour, ICounter
    {
        public int Num { get; set; } = 0;

        // public IEnumerable GetNum() => Num;

        public IEnumerable Count()
        {
            Debug.Log("Num: " + Num++);
            yield return null;
        }
    }
}
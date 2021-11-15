using System;
using System.Collections;
using UnityEngine;

namespace Utilities
{
    public static class Parallel
    {
        public static IEnumerator ExecuteActionWithDelay(Action action, float intervalTime, bool repeat = false)
        {
            do
            {
                yield return new WaitForSeconds(intervalTime);
                action.Invoke();
            } while (repeat);
        }
    }
}
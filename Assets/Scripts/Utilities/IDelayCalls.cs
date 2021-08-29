using System;
using System.Collections;

namespace Utilities
{
    public interface IDelayCalls
    {
        public IEnumerator ExecuteActionWithDelay(Action action, float intervalTime);
    }
}
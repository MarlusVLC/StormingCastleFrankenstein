using System.Collections;
using UnityEngine.EventSystems;

namespace UI.Interfaces
{
    public interface ICounter : IEventSystemHandler
    {
        int Num { set; get; }
        // IEnumerable GetNum();
        IEnumerable Count();
    }
}
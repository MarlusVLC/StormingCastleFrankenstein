using System.Collections;
using UnityEngine.EventSystems;

namespace UI.Interfaces
{
    public interface IHealth : IEventSystemHandler
    {
        public int CurrentHealth { get; }
        public IEnumerable TakeDamage(int damage);
    }
}
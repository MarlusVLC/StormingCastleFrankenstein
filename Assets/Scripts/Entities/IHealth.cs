using System.Collections;
using UnityEngine.EventSystems;

namespace Entities
{
    public interface IHealth : IEventSystemHandler
    {
        public int MaxHealth { get; }
        public int CurrentHealth { get; set; }
        public IEnumerable RecoverHealth(int healthAddition);
        public IEnumerable TakeDamage(int damage);
        
    }
}
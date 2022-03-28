using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Weapons
{
    public interface IBulletImpact : IEventSystemHandler
    {
        public LayerMask UncollidableMask { get; set; }
        public int Damage { set; get; }
        
        public BulletImpact Fire(Vector3 direction, float shootForce, float upwardForce);
        public BulletImpact SetUncollidableMask(LayerMask value);
        public BulletImpact SetDamage(int value);
    }
}
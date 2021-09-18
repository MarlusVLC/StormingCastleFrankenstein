using Gun;
using UnityEngine;

namespace AI
{
    public class GunBot : EnemyBot
    {
        private WeaponHandler _weaponHandler;

        protected override void Awake()
        {
            base.Awake();
            _weaponHandler = GetComponentInChildren<WeaponHandler>();
        }

        protected override void Attack()
        {
            //TODO mudar pra algo mais economico
            _weaponHandler.CurrentWeapon.TriggerGun(true, new Ray(Transform.position, Transform.forward));
        }
    }
}
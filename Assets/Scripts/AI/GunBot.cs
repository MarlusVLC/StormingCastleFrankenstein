using Weapons;
using UnityEngine;
using Utilities;

namespace AI
{
    public class GunBot : EnemyBot
    {
        [SerializeField] private Transform shoulder;
        [SerializeField] private float aimAlignSpeed = 4f;
        [Range(0,90)][SerializeField] private float maxVerticalAimAngle = 60f;
        [Range(0,90)][SerializeField] private float maxHorizontalAimAngle = 30f;

        
        private WeaponHandler _weaponHandler;

        protected override void Awake()
        {
            base.Awake();
            _weaponHandler = GetComponentInChildren<WeaponHandler>();
        }

        protected override void Attack()
        {
            AlignAim();
            var currWeapon = _weaponHandler.CurrentWeapon;
            if (currWeapon.IsEmpty == false)
            {
                
                _weaponHandler.Fire(true, new Ray(shoulder.position, shoulder.forward));
            }
        }

        protected virtual void AlignAim()
        {
            var targetPosition = _fov.FirstTarget.position;
            shoulder.RotateTowards(targetPosition, aimAlignSpeed);
        }
    }
}
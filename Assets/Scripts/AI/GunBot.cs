using System.Collections;
using Weapons;
using UnityEngine;
using Utilities;

namespace AI
{
    public class GunBot : EnemyBot
    {
        [SerializeField] private Transform shoulder;
        [SerializeField] private float aimAlignSpeed = 4f;
        [Range(0,90)][SerializeField] private float maxVerticalAimAngle = 90f;
        [Range(0,90)][SerializeField] private float maxHorizontalAimAngle = 30f;

        [Header("Gun Stats")]
        [Min(0)] [SerializeField] private int minSequentialShots = 1;
        [Min(0)] [SerializeField] private int maxSequentialShots = 6;
        [SerializeField] private float intervalSeconds = 0.2f;

        private WeaponHandler _weaponHandler;
        private int _sequenceThreshold;
        private int _sequentialShots;
        private bool _isAtFiringInterval;
        

        protected override void Awake()
        {
            base.Awake();
            _weaponHandler = GetComponentInChildren<WeaponHandler>();
            _weaponHandler.OnAnyShotFired += () => ++_sequentialShots;
            DefineSequenceThreshold();
        }

        protected override void Attack()
        {
            AlignAim();
            var currWeapon = _weaponHandler.CurrentWeapon;
            if (currWeapon.IsEmpty == false && _isAtFiringInterval == false)
            {
                _weaponHandler.Fire(true, new Ray(shoulder.position, shoulder.forward));
                if (_sequentialShots >= _sequenceThreshold)
                {
                    StartCoroutine(TriggerInterval());
                }
            }
        }

        protected virtual void AlignAim()
        {
            var targetPosition = _fov.FirstTarget.position;
            shoulder.RotateTowards(targetPosition, aimAlignSpeed);
        }

        protected IEnumerator TriggerInterval()
        {
            _sequentialShots = 0;
            _isAtFiringInterval = true;
            yield return new WaitForSeconds(intervalSeconds);
            DefineSequenceThreshold();
            _isAtFiringInterval = false;
        }

        protected void DefineSequenceThreshold()
        {
            _sequenceThreshold = Random.Range(minSequentialShots, maxSequentialShots);
        }
    }
}
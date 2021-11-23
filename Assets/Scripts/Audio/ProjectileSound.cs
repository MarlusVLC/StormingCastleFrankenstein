using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Audio
{
    public class ProjectileSound : AudioManager
    {
        [SerializeField] private GameObject revolver;
        [SerializeField] private GameObject shotgun;
        [SerializeField] private GameObject demonGun;
        
        
        // public void PlayProjectileSound()
        // {
        //     if (revolver.gameObject.activeSelf)
        //     {
        //         StartCoroutine(RevolverShot());
        //     }
        //     else if (shotgun.gameObject.activeSelf)
        //     {
        //         StartCoroutine(ShotgunShot());
        //     }
        //     else if (demonGun.gameObject.activeSelf)
        //     {
        //         StartCoroutine(DemonShot());
        //     }
        // }
        
        public void PlayProjectileSound(Gun.GunType gunType)
        {
            if (gunType == Gun.GunType.Revolver)
            {
                StartCoroutine(RevolverShot());
            }
            else if (gunType == Gun.GunType.Shotgun)
            {
                StartCoroutine(ShotgunShot());
            }
            else if (gunType == Gun.GunType.Demon)
            {
                StartCoroutine(DemonShot());
            }
        }

        public void PlayProjectileEmptySound()
        {
            if (revolver.gameObject.activeSelf)
            {
                var index = Random.Range(1, 2);
                Play(index);
            }
            else if (shotgun.gameObject.activeSelf)
            {
                Play(8);
            }
        }

        private IEnumerator RevolverShot()
        {
            Play(0);
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            var index = Random.Range(3, 6);
            Play(index);
        }

        private IEnumerator ShotgunShot()
        {
            Play(7);
            yield return new WaitForSeconds(0.5f);
            Play(10);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            Play(9);
        }
        
        private IEnumerator DemonShot()
        {
            Play(Random.Range(11, 14));
            yield break;
        }
    }
}
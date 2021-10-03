using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class ProjectileSound : AudioManager
    {
        [SerializeField] private GameObject revolver;
        [SerializeField] private GameObject shotgun;
        public void PlayProjectileSound()
        {
            if (revolver.gameObject.activeSelf)
            {
                StartCoroutine(RevolverShot());
            }
            else if (shotgun.gameObject.activeSelf)
            {
                StartCoroutine(ShotgunShot());
            }
        }

        public void PlayProjectileEmptySound()
        {
            if (revolver.gameObject.activeSelf)
            {
                int index = Random.Range(1, 2);
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
            int index = Random.Range(3, 6);
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
    }
}
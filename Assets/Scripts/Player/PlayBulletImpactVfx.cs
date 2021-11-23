using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{
    public class PlayBulletImpactVfx : MonoBehaviour
    {
        private VisualEffect blood;

        private void Awake()
        {
            blood = GetComponent<VisualEffect>();

            StartCoroutine(PlayAndDestroy());
        }

        private IEnumerator PlayAndDestroy()
        {
            blood.Play();
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace AI
{
    public class PlayBloodVfxPlayer : MonoBehaviour
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
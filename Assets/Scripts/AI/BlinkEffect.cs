using System.Collections;
using UnityEngine;

namespace AI
{
    public class BlinkEffect : MonoBehaviour
    {
        private Color startColor;
        private Color endColor = Color.white;

        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            startColor = _renderer.material.color;
        }

        private IEnumerator BlinkOnce()
        {
            _renderer.material.color = Color.Lerp(startColor, endColor, 0.2f);
            yield return new WaitForSeconds(0.1f);
            _renderer.material.color = startColor;
        }
    
        public void DamageBlink()
        {
            StartCoroutine(BlinkOnce());
        }
    }
}
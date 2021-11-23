using System.Collections;
using UnityEngine;

namespace AI
{
    public class BlinkEffect : MonoBehaviour
    {
        [SerializeField] private Material _newMaterial;
        private Material _material;
        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
        }

        private IEnumerator BlinkOnce()
        {
            _renderer.material = _newMaterial;
            yield return new WaitForSeconds(0.1f);
            _renderer.material = _material;
        }
    
        public void DamageBlink()
        {
            StartCoroutine(BlinkOnce());
        }
    }
}
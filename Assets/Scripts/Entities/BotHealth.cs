using Audio;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities
{
    public class BotHealth : Health
    {
        [SerializeField] private VisualEffect bloodVFX;

        private void Awake()
        {
            bloodVFX.Stop();
        }

        protected override void Die()
        {
            transform.parent.transform.position = transform.position;
            Instantiate(bloodVFX, transform.parent);
            bloodVFX.Play();
            
            Destroy(gameObject);
            
            FindObjectOfType<WendigoSound>().PlayWendigoDeathSound();
        }
    }
}
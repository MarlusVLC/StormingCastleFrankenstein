using Audio;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities
{
    public class BotHealth : Health
    {
        [SerializeField] private VisualEffect bloodVFX;

        protected override void Awake()
        {
            base.Awake();
            bloodVFX.Stop();
        }

        protected override void Die()
        {
            transform.parent.position = transform.position;
            Instantiate(bloodVFX, transform.parent);
            bloodVFX.Play();
            
            FindObjectOfType<WendigoSound>().PlayWendigoDeathSound();

            Destroy(gameObject);
            
        }
    }
}
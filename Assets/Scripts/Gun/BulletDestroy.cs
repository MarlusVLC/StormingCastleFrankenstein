using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            Destroy(this.gameObject);
    }
}

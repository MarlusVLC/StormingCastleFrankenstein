using Entities;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}

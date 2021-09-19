using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex;

    public static void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
    
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneUtil.ResetScene();
            }
        }
    }
#endif

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        Invoke("GoToMainMenu", 68f);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

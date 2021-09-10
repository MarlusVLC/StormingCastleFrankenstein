using System;
using UnityEngine;

namespace UI.Menus
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject[] toBeHidden;
        [SerializeField] private GameObject[] toBeShown;
        
        public static bool IsPaused { get; private set; }

        public void Start()
        {
            IsPaused = true;
            TogglePauseState();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseState();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            AudioListener.pause = true;
            Array.ForEach(toBeHidden, gO =>
            {
                gO.SetActive(false);
            });
            Array.ForEach(toBeShown, gO => gO.SetActive(true));
            IsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
        
        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            AudioListener.pause = false;
            Array.ForEach(toBeHidden, gO => gO.SetActive(true));
            Array.ForEach(toBeShown, gO => gO.SetActive(false));
            IsPaused = false;
            // Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void TogglePauseState()
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
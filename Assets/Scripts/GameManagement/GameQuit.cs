using UnityEngine;
using Utilities;

namespace GameManagement
{
    public class GameQuit : MonoBehaviour
    {
        public void QuitGame()
        {
            AppHelper.Quit();
        }
    }
}
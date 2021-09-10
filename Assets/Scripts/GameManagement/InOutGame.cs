using UnityEngine;
using Utilities;

namespace GameManagement
{
    public class InOutGame : MonoBehaviour
    {
        public void QuitGame()
        {
            AppHelper.Quit();
        }
    }
}
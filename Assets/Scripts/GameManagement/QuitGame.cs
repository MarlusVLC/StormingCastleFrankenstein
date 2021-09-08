using UnityEngine;
using Utilities;

namespace GameManagement
{
    public class QuitGame : MonoBehaviour
    {
        public void Quit()
        {
            AppHelper.Quit();
        }
    }
}
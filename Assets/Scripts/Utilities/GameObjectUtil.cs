using UnityEngine;

namespace Utilities
{
    public static class GameObjectUtil
    {
        public static void ExclusivelyActivate(ref GameObject[] items, int selection)
        {
            items[selection].SetActive(true);
            for (var i = 0; i < items.Length; i++)
            {
                if (i != selection)
                {
                    items[i].SetActive(false);
                }
            }
        }
    }
}
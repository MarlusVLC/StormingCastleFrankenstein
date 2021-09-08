using UnityEngine;

namespace UI.Menus
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject targetSubMenu;

        public void SwitchMenus()
        {
            targetSubMenu.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
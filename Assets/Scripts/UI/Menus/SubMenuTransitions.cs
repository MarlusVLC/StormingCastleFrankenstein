using System;
using TMPro;
using UnityEngine;

namespace UI.Menus
{
    public class SubMenuTransitions : MonoBehaviour
    {
        [SerializeField] private GameObject targetSubMenu;
        [SerializeField] private string displayText;
        [SerializeField] private TextMeshProUGUI subMenuTitleGUI;


        private GameObject _parentSubMenu;

        private void Awake()
        {
            _parentSubMenu = transform.parent.gameObject;
        }

        /**
         * Aloque esse método em um UnityEvent
         */
        public void SwitchMenus()
        {
            targetSubMenu.SetActive(true);
            _parentSubMenu.SetActive(false);
            subMenuTitleGUI.text = displayText;
        }
    }
}
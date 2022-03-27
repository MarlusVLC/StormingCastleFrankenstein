using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Unknown : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private StandaloneInputModule _input;
        [SerializeField] private GameObject clickableObject;
        
        private void Awake()
        {
            _eventSystem.UpdateModules();
            _input.ActivateModule();
            DebugSequentially();
        }

        private void Update()
        {
        }

        private IEnumerator DebugSequentiallyCoroutine()
        {
            int i = 0;
            while (true)
            {
                Debug.Log(i.ToString());
                yield return new WaitForSeconds(i);
                i++;
            }
        }

        private void DebugSequentially() => StartCoroutine(DebugSequentiallyCoroutine());

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(" ButtonPressed: " + eventData.button);
            // ExecuteEvents.Execute(clickableObject, eventData, DebugSequentially(clickableObject, eventData));

        }
    }
}
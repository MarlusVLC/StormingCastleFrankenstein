using System;
using AI;
using TMPro;
using UnityEngine;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(FieldOfView))]
    public abstract class Interact : MonoCache
    {
        [SerializeField] private Transform itemsParent;
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private float range = 3f;
        [SerializeField] private KeyCode activateKey = KeyCode.E;
        [TextArea][SerializeField] private string actionMessage;
        [SerializeField] private TextMeshProUGUI actionMessageHUD;

        private bool canInteract;
        private FieldOfView _fov;

        protected override void Awake()
        {
            base.Awake();
            actionMessageHUD.text = actionMessage;
            _fov = GetComponent<FieldOfView>();
            _fov.OnTargetAcquired += _ => EnableInteraction();
            _fov.OnTargetLost += HideText;
        }

        private void Update()
        {
            if (canInteract)
            {
                if (Input.GetKeyDown(activateKey))
                {
                    Interaction(_fov.FirstTarget);
                }
            }
        }

        protected abstract void Interaction(Transform item);

        protected void EnableInteraction()
        {
            if (_fov.HasTarget && itemLayerMask.HasLayerWithin(_fov.FirstTarget.gameObject.layer))
            {
                actionMessageHUD.text = actionMessage;
                actionMessageHUD.enabled = canInteract = true;
            }
        }

        protected void HideText()
        {
            actionMessageHUD.enabled = canInteract =  false;
        }
    }
}
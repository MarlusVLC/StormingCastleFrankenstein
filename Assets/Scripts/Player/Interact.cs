using TMPro;
using UnityEngine;
using Utilities;

namespace Player
{
    public abstract class Interact : MonoCache
    {
        [SerializeField] private Transform itemsParent;
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private float range = 3f;
        [SerializeField] private KeyCode activateKey = KeyCode.E;
        [TextArea][SerializeField] private string actionMessage;
        [SerializeField] private TextMeshProUGUI actionMessageHUD;
        
        private RaycastHit[] _raycastHit = new RaycastHit[1];

        protected override void Awake()
        {
            base.Awake();
            actionMessageHUD.text = actionMessage;
        }

        private void Update()
        {
            CheckInteractivity();
        }

        private void CheckInteractivity()
        {
            foreach (Transform item in itemsParent)
            {
                if (Vector3.SqrMagnitude(item.transform.position - Transform.position) < range*range
                    && Physics.RaycastNonAlloc(Transform.position, Transform.forward
                        , _raycastHit, range, itemLayerMask) > 0)
                {
                    actionMessageHUD.enabled  = true;
                    if (Input.GetKey(activateKey))
                    {
                        Interaction(item);
                    }
                }
                else
                {
                    actionMessageHUD.enabled =  false;
                }
            }
        }

        protected abstract void Interaction(Transform item);
    }
}
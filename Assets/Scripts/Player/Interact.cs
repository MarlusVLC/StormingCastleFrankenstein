using TMPro;
using UnityEngine;
using Utilities;

namespace Player
{
    public abstract class Interactable : MonoCache
    {
        [SerializeField] private Transform[] itens;
        [SerializeField] private LayerMask portalLayerMask;
        [SerializeField] private float range = 3f;
        [SerializeField] private KeyCode activateKey = KeyCode.E;
        [TextArea][SerializeField] private string actionMessage;
        [SerializeField] private TextMeshProUGUI actionMessageHUD;
        private bool _canBeInteractedWith;
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
            foreach (var portal in portals)
            {
                if (Vector3.SqrMagnitude(portal.transform.position - gameObject.transform.position) < range*range
                    && Physics.RaycastNonAlloc(Transform.position, Transform.forward
                        , _raycastHit, range, ~portalLayerMask) > 0)
                {
                    actionMessageHUD.enabled = _canBeInteractedWith = true;
                    if (Input.GetKey(activateKey))
                    {
                        SceneUtil.ResetScene();
                    }
                }
                else
                {
                    actionMessageHUD.enabled = _canBeInteractedWith =  false;
                }
            }
        }
    }
}
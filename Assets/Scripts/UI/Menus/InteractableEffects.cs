using Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Menus
{
    public class InteractableEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private float transitionTime = 0.125f;
        [SerializeField] private float highlightSpecularPower = 4f;
        [SerializeField] private float lightAngle = 4f;
        [Range(1,3)][SerializeField] private int clickSoundIndex = 1;
        [SerializeField] private bool playSound = true;

        private TextMeshProUGUI _text;
        private float _origSpecularPower;
        private float _origAngle;
        private Color _origColor;
        private MenuSound _menuSound;
        
        private void OnEnable()
        {
            TryGetComponent(out _text);
            _origColor = _text.fontMaterial.GetColor("_FaceColor");
            _origSpecularPower = _text.fontMaterial.GetFloat("_SpecularPower");
            _origAngle = _text.fontMaterial.GetFloat("_LightAngle");
            _menuSound = FindObjectOfType<MenuSound>();
        }

        private void OnDisable()
        {
            _text.fontMaterial.SetFloat("_SpecularPower", _origSpecularPower);
            _text.fontMaterial.SetFloat("_LightAngle", _origAngle);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.fontMaterial.DOFloat(highlightSpecularPower, "_SpecularPower", transitionTime).SetUpdate(true);
            _text.fontMaterial.DOFloat(lightAngle, "_LightAngle", transitionTime).SetUpdate(true);
            _menuSound.PlayMouseOver();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.fontMaterial.DOFloat(_origSpecularPower, "_SpecularPower", transitionTime).SetUpdate(true);
            _text.fontMaterial.DOFloat(_origAngle, "_LightAngle", transitionTime).SetUpdate(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (playSound)
            {
                _menuSound.PlayIndex(clickSoundIndex);
            }
        }
    }
}
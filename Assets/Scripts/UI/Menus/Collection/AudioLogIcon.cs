using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Menus.Collection
{
    [RequireComponent(typeof(Toggle))]
    public class AudioLogIcon : MonoBehaviour
    {
        [SerializeField] private int audioLogIndex;
        [Range(0, 1)] [SerializeField] private float deactivatedAlpha;
        [SerializeField] GramophoneCollector _gramophoneCollector;


        private AudioLogSound _audioLogSound;
        private Toggle _toggle;
        private Image _graphic;
        private bool _hasBeenCollected;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _graphic = transform.RetrieveComponentsInChildren<Image>()[0];
            SetAlpha(_toggle.isOn);
        }
        
        private void OnEnable()
        {
            if (_gramophoneCollector == null)
            {
                _gramophoneCollector = FindObjectOfType<GramophoneCollector>();
            }
            _gramophoneCollector.OnGramophoneCollected += CollectAudioLog;

            IsFullyInteractable = _hasBeenCollected;
        }

        private void Start()
        {
            _audioLogSound = FindObjectOfType<AudioLogSound>();
        }

        public void ToggleAudioLog(bool isOn)
        {
            if (isOn)
            {
                _audioLogSound.PlayAudioLogSound(audioLogIndex);
            }
            else
            {
                _audioLogSound.Stop();
            }
            SetAlpha(isOn);
        }

        private void SetAlpha(bool isOn)
        {
            var newColor = _graphic.color;
            newColor.a = isOn ? 1f : deactivatedAlpha;
            _graphic.color = newColor;
        }

        private void CollectAudioLog(int collectionIndex)
        {
            Debug.Log("Gramophone collected");
            if (collectionIndex == audioLogIndex)
            {
                _hasBeenCollected = true;
                SetAlpha(true);
            }
        }

        private bool IsFullyInteractable
        {
            get => _graphic.enabled && _toggle.interactable;
            set => _graphic.enabled = _toggle.interactable = value;
        }
        public bool HasBeenCollected
        {
            get => _hasBeenCollected;
            set => _hasBeenCollected = value;
        }
    }
}
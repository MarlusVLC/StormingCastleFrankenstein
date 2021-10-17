using System;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace UI.Menus.Collection
{
    [RequireComponent(typeof(Toggle))]
    public class AudioLogIcon : MonoBehaviour
    {
        [SerializeField] private int audioLogIndex;
        [FormerlySerializedAs("_gramophoneCollector")] 
        [SerializeField] GramophoneCollector gramophoneCollector;
        [Range(0, 1)] [SerializeField] private float deactivatedAlpha;



        private AudioLogSound _audioLogSound;
        private Toggle _toggle;
        private Image _graphic;
        private bool _hasBeenCollected;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _graphic = transform.RetrieveComponentsInChildren<Image>()[0];
            SetAlpha(_toggle.isOn);
            
            if (gramophoneCollector == null)
            {
                gramophoneCollector = FindObjectOfType<GramophoneCollector>();
            }
            _audioLogSound = gramophoneCollector.AudioLogSound;
            gramophoneCollector.OnGramophoneCollected += EnableAudioLog;
            _audioLogSound.OnClipFinished += () => ToggleAudioLog(false);
        }

        private void OnEnable()
        {
            IsFullyInteractable = gramophoneCollector.HasAudioLogBeenCollected[audioLogIndex];
        }



        public void ToggleAudioLog(bool isOn)
        {
            if (isOn)
            {
                _audioLogSound.PlayAudioLogSound(audioLogIndex);
            }
            else if (GamePause.IsPaused)
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

        private void EnableAudioLog(int collectionIndex)
        {
            var isMatching = collectionIndex == audioLogIndex;
            if (isMatching)
            {
                ToggleAudioLog(true);
            }
            _toggle.isOn = isMatching;
        }

        #region PROPERTIES

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

        #endregion

    }
}
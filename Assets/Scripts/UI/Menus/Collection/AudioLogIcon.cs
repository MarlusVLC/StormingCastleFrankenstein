using System;
using System.Collections;
using System.Collections.Generic;
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

        private AudioLogSound _audioLogSound;
        private Toggle _toggle;
        private Image _graphic;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _graphic = transform.RetrieveComponentsInChildren<Image>()[0];
            SetAlpha(_toggle.isOn);
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
    }
}
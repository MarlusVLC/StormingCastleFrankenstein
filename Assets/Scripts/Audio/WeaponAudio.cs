using System;
using System.Collections;
using Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class WeaponAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip shotAudioClip;
        [SerializeField] private AudioClip[] empty;
        [SerializeField] private AudioClip[] shell;
        
        // private AudioClip shellAudioClip;
        // private AudioClip emptyAudioClip;

        private IEnumerator SfxShotThenShell()
        {
            if (shotAudioClip == null) yield break;
            AudioSource.PlayClipAtPoint(shotAudioClip, transform.position);
        
            yield return new WaitForSeconds(1);
            
            PlayRandomSound(shell);
        }

        public void ShotWithShell()
        {
            StartCoroutine(SfxShotThenShell());
        }

        public void EmptySfx()
        {
            PlayRandomSound(empty);
        }

        public static void PlayRandomSound(AudioClip[] sounds, Vector3 position)
        {
            // play empty shooting sound
            int index = Random.Range(0, sounds.Length);
            var audioClip = sounds[index];
            if (sounds.Length == 0) return;
            AudioSource.PlayClipAtPoint(audioClip, position);
        }

        public void PlayRandomSound(AudioClip[] sounds)
        {
            PlayRandomSound(sounds, transform.position);
        }
    }
}
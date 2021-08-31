using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAmbience : MonoBehaviour
{
    [SerializeField] private AudioClip ambienceAudioClip;
    
    void Start()
    {
        AudioSource.PlayClipAtPoint(ambienceAudioClip, this.transform.position);
    }
}

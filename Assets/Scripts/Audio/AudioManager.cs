using System;
using System;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private int indexSequence = 0;
    private AudioSource currentSource;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    protected int GetSoundsSize()
    {
        return sounds.Length;
    }

    protected void PlaySequence()
    {
        Play(indexSequence);
        indexSequence++;
        
        if (indexSequence == GetSoundsSize())
        {
            indexSequence = 0;
        }
    }
    
    protected void Play(int index)
    {
        currentSource = sounds[index].source; 
        currentSource.Play();
    }
    
    public void Stop()
    {
        if (currentSource)
        {
            currentSource.Stop();   
        }
    }
}
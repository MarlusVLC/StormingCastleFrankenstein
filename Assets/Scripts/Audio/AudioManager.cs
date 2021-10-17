using System;
using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private int indexSequence = 0;
    private AudioSource currentSource;

    public float ClipLength => currentSource.clip.length;
    public event Action OnClipFinished;

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
    
    protected void Play(int index, bool ignoreListenerPause = false)
    {
        currentSource = sounds[index].source;
        currentSource.ignoreListenerPause = ignoreListenerPause;
        currentSource.Play();
        StartCoroutine(WaitForAudioEnd());
    }
    
    public void Stop()
    {
        if (currentSource)
        {
            currentSource.Stop();   
        }
    }

    private IEnumerator WaitForAudioEnd()
    {
        while (currentSource.isPlaying)
        {
            yield return null;
        }
        OnClipFinished?.Invoke();
    }
}
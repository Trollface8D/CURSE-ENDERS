using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlay : MonoBehaviour
{
    public AudioSource theAudioSource;
    public bool randomPlay = false; // checkbox for random play
    public AudioClip[] clips;
    private AudioSource audioSource;
    AudioClip lastClip;
    int clipOrder = 0; // for ordered playlist

    void Start () 
    {
        audioSource = GetComponent<AudioSource> ();
        audioSource.loop = false;
    }

    void Update () 
    {
        if (!audioSource.isPlaying) 
        {
            // if random play is selected
            if (randomPlay == true)
            {
                audioSource.clip = GetRandomClip ();
                audioSource.Play ();
                // if random play is not selected
            }
            else 
            {
                audioSource.clip = GetNextClip ();
                audioSource.Play ();
            }
        }
    }

    // function to get a random clip
    private AudioClip GetRandomClip ()
    {
        int attempts = 3;
        AudioClip newClip = clips[Random.Range (0, clips.Length)];

        while (newClip == lastClip && attempts > 0) 
        {
            newClip = clips[Random.Range(0, clips.Length)];
            attempts--;
        }
        lastClip = newClip;

        return newClip;
        
    }

    // function to get the next clip in order, then repeat from the beginning of the list.
    private AudioClip GetNextClip ()
    {
        if (clipOrder >= clips.Length - 1)
        {
            clipOrder = 0;
        }
        else
        {
            clipOrder += 1;
        }

        return clips[clipOrder];
    }
}

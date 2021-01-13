using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip main;
    public AudioClip forest;

    public bool soundOn = true;

    private void Start()
    {
        instance = GetComponent<SoundManager>();
    }

    public void TurnToMainMusic()
    {
        GetComponent<AudioSource>().clip = main; 
        if(soundOn)
            GetComponent<AudioSource>().Play();
    }

    public void TurnToForestMusic()
    {
        GetComponent<AudioSource>().clip = forest; 
        if(soundOn)
            GetComponent<AudioSource>().Play();
    }

    public void Sound()
    {
        if(soundOn)
        {
            GetComponent<AudioSource>().Stop();
            soundOn = false;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            soundOn = true;
        }
    }
}

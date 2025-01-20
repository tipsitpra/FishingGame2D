using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;
    private AudioSource audioSrc;
    private AudioClip[] audioSounds;


    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        audioSounds = Resources.LoadAll<AudioClip>("Sounds");
    }   

    public void GameOverSound()
    {

        audioSrc.PlayOneShot(audioSounds[2]);

    }
    public void GameWinSound()
    {

        audioSrc.PlayOneShot(audioSounds[4]);

    }
    public void SoundHook()
    {

        audioSrc.PlayOneShot(audioSounds[5]);

    } 
    public void TakeFish()
    {

        audioSrc.PlayOneShot(audioSounds[1]);

    } 
    public void DamageFishBoss()
    {

        audioSrc.PlayOneShot(audioSounds[3]);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    private AudioSource[] soundEffects; 


    void Start()
    {
        soundEffects = GetComponents<AudioSource>();
    }


    public void StartReloading()
    {
        soundEffects[0].Play();
    }

    public void StopReloading()
    {
        soundEffects[0].Stop();
    }


    ////////////////////////////////////////////////////
    

    public void StartReapairing()
    {
        soundEffects[1].Play();
    }

    public void StopReapairing()
    {
        soundEffects[1].Stop();
    }


    ////////////////////////////////////////////////////


    public void PlayImpact()
    {
        soundEffects[2].Play();
    }
}

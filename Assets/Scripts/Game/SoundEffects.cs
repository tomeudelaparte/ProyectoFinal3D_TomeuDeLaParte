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

    // Plays reload sound
    public void StartReloading()
    {
        soundEffects[0].Play();
    }

    // Stops reload sound
    public void StopReloading()
    {
        soundEffects[0].Stop();
    }

    ////////////////////////////////////////////////////

    // Plays repair sound
    public void StartReapairing()
    {
        soundEffects[1].Play();
    }

    // Stops repair sound
    public void StopReapairing()
    {
        soundEffects[1].Stop();
    }

    ////////////////////////////////////////////////////

    // Plays impact sound
    public void PlayImpact()
    {
        soundEffects[2].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    private AudioSource[] soundEffects; 

    void Start()
    {
        // Gets audiosource
        soundEffects = GetComponents<AudioSource>();
    }

    public void StartReloading()
    {
        // Plays sound from audiosource 0
        soundEffects[0].Play();
    }

    public void StopReloading()
    {
        // Stops sound from audiosource 0
        soundEffects[0].Stop();
    }

    public void StartReapairing()
    {
        // Plays sound from audiosource 1
        soundEffects[1].Play();
    }

    public void StopReapairing()
    {
        // Plays sound from audiosource 1
        soundEffects[1].Stop();
    }

    // Plays impact sound
    public void PlayImpact()
    {
        // Plays sound from audiosource 2
        soundEffects[2].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioOptions : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private DataPersistence dataPersistence;

    [Header("AUDIO SETTINGS")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectSlider;

    [Header("AUDIO MIXER")]
    public AudioMixer audioMixer;

    [Header("DEFAULT AUDIO SETTINGS")]
    private float DefaultMasterVolume = 1f;
    private float DefaultMusicVolume = 1f;
    private float DefaultEffectsVolume = 0.7f;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        // Load the settings
        LoadSavedSettings();
    }

    // Load the settings
    private void LoadSavedSettings()
    {
        GetMasterVolume();
        GetMusicVolume();
        GetEffectsVolume();
    }

    // Gets the General Volume option
    public void GetMasterVolume()
    {
        // If it does not exist, stores a default value
        if (!dataPersistence.HasKey("Master Volume"))
        {
            SetMasterVolume(DefaultMasterVolume);
        }

        // Gets the saved value
        masterSlider.value = dataPersistence.GetFloat("Master Volume");
    }

    // Gets the Music Volume option.
    public void GetMusicVolume()
    {
        // If it does not exist, stores a default value
        if (!dataPersistence.HasKey("Music Volume"))
        {
            SetMusicVolume(DefaultMusicVolume);
        }

        // Gets the saved value
        musicSlider.value = dataPersistence.GetFloat("Music Volume");
    }

    // Gets the Effects Volume option.
    public void GetEffectsVolume()
    {
        // If it does not exist, stores a default value
        if (!dataPersistence.HasKey("Effects Volume"))
        {
            SetEffectsVolume(DefaultEffectsVolume);
        }

        // Gets the saved value
        effectSlider.value = dataPersistence.GetFloat("Effects Volume");
    }

    // Sets the General Volume option.
    public void SetMasterVolume(float volume)
    {
        // Change the volume in the AudioMixer
        audioMixer.SetFloat("Master Volume", Mathf.Log10(volume) * 20);

        // Save the option
        dataPersistence.SetFloat("Master Volume", volume);
    }

    // Set the Music Volume option.
    public void SetMusicVolume(float volume)
    {
        // Change the volume in the AudioMixer
        audioMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);

        // Save the option
        dataPersistence.SetFloat("Music Volume", volume);
    }

    // Set the Effects Volume option.
    public void SetEffectsVolume(float volume)
    {
        // Change the volume in the AudioMixer
        audioMixer.SetFloat("Effects Volume", Mathf.Log10(volume) * 20);

        // Save the option
        dataPersistence.SetFloat("Effects Volume", volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VideoOptions : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private DataPersistence dataPersistence;

    [Header("SETTINGS MANAGER")]
    private OptionsManager settingsManager;

    [Header("VIDEO SETTINGS")]
    public TMP_Dropdown dropdownDisplayModeUI;
    public TMP_Dropdown dropdownResolutionsUI;
    public Toggle toggleVerticalSyncUI;
    public Slider sliderFramerateLimitUI;

    [Header("DEFAULT SETTINGS")]
    private int displayModeIndex = 0;
    private int resolutionIndex = 0;
    private bool verticalSyncOn = false;
    private float framesLimitSize = 60f;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();
        settingsManager = FindObjectOfType<OptionsManager>();

        // Gets the list of available resolutions
        settingsManager.GetResolutionsAvailable(dropdownResolutionsUI);

        // Loads saved options values
        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        // Gets all video saved settings
        GetDisplayModeList();
        GetResolutionsList();
        GetVerticalSync();
        GetFramerateLimit();
    }

    private void GetDisplayModeList()
    {
        // If it does not exist, save a default value
        if (!dataPersistence.HasKey("DISPLAY MODE"))
        {
            SetDisplayModeList(displayModeIndex);
        }

        // Gets the saved value
        dropdownDisplayModeUI.value = dataPersistence.GetInt("DISPLAY MODE");
    }

    private void GetResolutionsList()
    {
        // If it does not exist, save a default value
        if (!dataPersistence.HasKey("RESOLUTION"))
        {
            SetResolutionsList(resolutionIndex);
        }

        // Get the saved value
        dropdownResolutionsUI.value = dataPersistence.GetInt("RESOLUTION");
    }

    private void GetVerticalSync()
    {
        // If it does not exist, save a default value
        if (!dataPersistence.HasKey("VERTICAL SYNC"))
        {
            SetVerticalSync(verticalSyncOn);
        }

        // Gets the saved value
        toggleVerticalSyncUI.isOn = dataPersistence.GetBool("VERTICAL SYNC");
    }

    private void GetFramerateLimit()
    {
        // If it does not exist, save a default value
        if (!dataPersistence.HasKey("FPS LIMIT"))
        {
            SetFramerateLimit(framesLimitSize);
        }

        // Gets the saved value
        sliderFramerateLimitUI.value = dataPersistence.GetInt("FPS LIMIT");
    }

    public void SetDisplayModeList(int index)
    {
        // Gets the saved resolution option
        int resolution = dataPersistence.GetInt("RESOLUTION");

        // Activates the option
        settingsManager.setDisplayModeAndResolution(index, resolution);

        // Saves the option
        dataPersistence.SetInt("DISPLAY MODE", index);
    }

    public void SetResolutionsList(int index)
    {
        // Gets the saved option
        int display = dataPersistence.GetInt("DISPLAY MODE");

        // Activates the option
        settingsManager.setDisplayModeAndResolution(display, index);

        // Saves the option
        dataPersistence.SetInt("RESOLUTION", index);
    }

    public void SetVerticalSync(bool isOn)
    {
        // Activates the option
        settingsManager.SetVerticalSync(isOn);

        // Saves the option
        dataPersistence.SetBool("VERTICAL SYNC", isOn);
    }

    public void SetFramerateLimit(float size)
    {
        // Activates the option
        settingsManager.SetFramerateLimit((int)size);

        // Saves the option
        dataPersistence.SetInt("FPS LIMIT", (int)size);
    }
}

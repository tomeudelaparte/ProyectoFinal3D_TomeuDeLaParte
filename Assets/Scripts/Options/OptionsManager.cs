using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OptionsManager : MonoBehaviour
{
    public TextMeshProUGUI frameLimitText;

    private Resolution[] resolutions;

    public void GetResolutionsAvailable(TMP_Dropdown dropdown)
    {
        // Gets 60Hz resolutions
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();

        // Inverts the array order
        System.Array.Reverse(resolutions);

        // Clears all dropdown options
        dropdown.ClearOptions();

        // Index of the current resolution
        int currentResolutionIndex = 0;

        // List to obtain the available options
        List<string> options = new List<string>();

        // Scrolls through all but the last 3 resolutions
        for (int i = 0; i < resolutions.Length - 3; i++)
        {
            // Creates and adds an option with this format to String
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // If the current resolution is equal to the resolution of the array
            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                // Saves the resolution index
                currentResolutionIndex = i;
            }
        }

        // Adds the options to the list
        dropdown.AddOptions(options);

        // Selects the current resolution in the list
        dropdown.value = currentResolutionIndex;

        // Refresh the selected value
        dropdown.RefreshShownValue();
    }

    // Applies resolution and display mode settings
    public void setDisplayModeAndResolution(int displayIndex, int resolutionIndex)
    {
        // Display mode default value
        FullScreenMode displayMode = FullScreenMode.ExclusiveFullScreen;

        // Selection according to the index obtained
        switch (displayIndex)
        {
            case 0:

                // Saves display mode
                displayMode = FullScreenMode.ExclusiveFullScreen;
                break;

            case 1:

                // Saves display mode
                displayMode = FullScreenMode.FullScreenWindow;
                break;

            case 2:

                // Saves display mode
                displayMode = FullScreenMode.Windowed;
                break;
        }

        // Sets resolution and display mode
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, displayMode);
    }

    // Activates vertical synchronization
    public void SetVerticalSync(bool isOn)
    {
        if (isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    // Enables the frame limit
    public void SetFramerateLimit(int frames)
    {
        Application.targetFrameRate = frames;
    }

    // Changes frames number text from slider
    public void UpdateFrameSliderText(float frames)
    {
        frameLimitText.text = frames.ToString();
    }
}

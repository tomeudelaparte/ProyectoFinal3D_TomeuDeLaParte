using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedOption : MonoBehaviour
{
    private GameObject selected;

    [Header("VIDEO OPTIONS")]
    public GameObject dropdownDisplay;
    public GameObject dropdownResolution;
    public GameObject toggleVSync;
    public GameObject sliderFPSLimit;

    [Header("AUDIO OPTIONS")]
    public GameObject sliderMasterVolume;
    public GameObject sliderMusicVolume;
    public GameObject sliderEffectsVolume;

    private void Update()
    {
        // Gets current select GameObject in event system
        selected = EventSystem.current.currentSelectedGameObject.gameObject;

        // Checks if one of this is selected
        checkIfSelected(dropdownDisplay);
        checkIfSelected(dropdownResolution);
        checkIfSelected(toggleVSync);
        checkIfSelected(sliderFPSLimit);

        checkIfSelected(sliderMasterVolume);
        checkIfSelected(sliderMusicVolume);
        checkIfSelected(sliderEffectsVolume);
    }

    // Checks if current option is selected
    private void checkIfSelected(GameObject obj)
    {
        // If selected is equals this object
        if (selected.name == obj.name)
        {
            // Shows option background
            obj.transform.parent.GetChild(0).gameObject.SetActive(true);
        }

        // If selected is not equals this object
        if (selected.name != obj.name)
        {
            // Hides option background
            obj.transform.parent.GetChild(0).gameObject.SetActive(false);
        }
    }
}

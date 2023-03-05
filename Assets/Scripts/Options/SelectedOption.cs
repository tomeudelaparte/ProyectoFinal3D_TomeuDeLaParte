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
        selected = EventSystem.current.currentSelectedGameObject.gameObject;

        checkIfSelected(dropdownDisplay);
        checkIfSelected(dropdownResolution);
        checkIfSelected(toggleVSync);
        checkIfSelected(sliderFPSLimit);

        checkIfSelected(sliderMasterVolume);
        checkIfSelected(sliderMusicVolume);
        checkIfSelected(sliderEffectsVolume);
    }

    private void checkIfSelected(GameObject obj)
    {
        if (selected.name == obj.name)
        {
            obj.transform.parent.GetChild(0).gameObject.SetActive(true);
        }

        if (selected.name != obj.name)
        {
            obj.transform.parent.GetChild(0).gameObject.SetActive(false);
        }
    }
}

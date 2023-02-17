using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedOption : MonoBehaviour
{
    private GameObject selected;

    public GameObject dropdownDisplay;
    public GameObject dropdownResolution;
    public GameObject toggleVSync;
    public GameObject sliderFPSLimit;

    public GameObject sliderMasterVolume;
    public GameObject sliderMusicVolume;
    public GameObject sliderEffectsVolume;

    private void Update()
    {
        selected = EventSystem.current.currentSelectedGameObject.gameObject;

        checkIsSelected(dropdownDisplay);
        checkIsSelected(dropdownResolution);
        checkIsSelected(toggleVSync);
        checkIsSelected(sliderFPSLimit);

        checkIsSelected(sliderMasterVolume);
        checkIsSelected(sliderMusicVolume);
        checkIsSelected(sliderEffectsVolume);
    }

    private void checkIsSelected(GameObject obj)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OptionsManager : MonoBehaviour
{
    public GameObject framerateCounterUI;
    public TextMeshProUGUI frameLimitUI;

    private Resolution[] resolutions;

    public void GetResolutionsAvailable(TMP_Dropdown dropdown)
    {
        // Obtiene las resoluciones de 60Hz
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();

        // Invierte el orden del array
        System.Array.Reverse(resolutions);

        // Limpia todas las opciones del dropdown
        dropdown.ClearOptions();

        // Index de la resolucion actual
        int currentResolutionIndex = 0;

        // Lista para obtener las opciones disponibles
        List<string> options = new List<string>();

        // Recorre todas las resoluciones menos las 3 ultimas
        for (int i = 0; i < resolutions.Length - 3; i++)
        {
            // Crea y agrega una opcion con este formato en String
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Si la resolucion actual es igual a la resolucion del array
            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                // Guarda el Index de la resolucion
                currentResolutionIndex = i;
            }
        }

        // Añade las opciones a la lista
        dropdown.AddOptions(options);

        // Selecciona la resolución actual en la lista
        dropdown.value = currentResolutionIndex;

        // Refresca el valor seleccionado
        dropdown.RefreshShownValue();
    }

    // Aplica los ajustes de resolucion y modo de pantalla
    public void setDisplayModeAndResolution(int displayIndex, int resolutionIndex)
    {
        // Valor default de displayMode
        FullScreenMode displayMode = FullScreenMode.ExclusiveFullScreen;

        // Seleccion segun el index obtenido
        switch (displayIndex)
        {
            case 0:

                // Guarda en la variable y para la ejecucion del Switch
                displayMode = FullScreenMode.ExclusiveFullScreen;
                break;

            case 1:

                // Guarda en la variable y para la ejecucion del Switch
                displayMode = FullScreenMode.FullScreenWindow;
                break;

            case 2:

                // Guarda en la variable y para la ejecucion del Switch
                displayMode = FullScreenMode.Windowed;
                break;
        }

        // Setea la resolución y el modo de pantalla
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, displayMode);
    }

    // Activa la sincronizacion vertical segun el valor obtenido
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

    // Activa el limite de frames
    public void SetFramerateLimit(int frames)
    {
        Application.targetFrameRate = frames;
    }

    // Activa el contador de frames
    public void SetFramerateShow(bool isOn)
    {
        framerateCounterUI.SetActive(isOn);
    }

    // Actualiza el numero de frames limite del slider
    public void UpdateFramerateSlider(float frames)
    {
        frameLimitUI.text = frames.ToString();
    }
}

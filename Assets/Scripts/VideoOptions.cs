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
    public Toggle toggleFramerateShowUI;

    [Header("DEFAULT SETTINGS")]
    private int displayModeIndex = 0;
    private int resolutionIndex = 0;
    private bool verticalSyncOn = false;
    private float framesLimitSize = 60f;
    private bool showFramesOn = false;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();
        settingsManager = FindObjectOfType<OptionsManager>();

        // Obtiene la lista de resolucion disponibles
        settingsManager.GetResolutionsAvailable(dropdownResolutionsUI);

        // Carga las opciones
        LoadSavedSettings();
    }

    // Carga las opciones
    private void LoadSavedSettings()
    {
        GetDisplayModeList();
        GetResolutionsList();
        GetVerticalSync();
        GetFramerateLimit();
        GetFramerateShow();
    }

    // Obtiene la opcion DisplayMode
    private void GetDisplayModeList()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("DISPLAY MODE"))
        {
            SetDisplayModeList(displayModeIndex);
        }

        // Obtiene el valor guardado
        dropdownDisplayModeUI.value = dataPersistence.GetInt("DISPLAY MODE");
    }

    // Obtiene la opcion ResolutionList
    private void GetResolutionsList()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("RESOLUTION"))
        {
            SetResolutionsList(resolutionIndex);
        }

        // Obtiene el valor guardado
        dropdownResolutionsUI.value = dataPersistence.GetInt("RESOLUTION");
    }

    // Obtiene la opcion VerticalSync
    private void GetVerticalSync()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("VERTICAL SYNC"))
        {
            SetVerticalSync(verticalSyncOn);
        }

        // Obtiene el valor guardado
        toggleVerticalSyncUI.isOn = dataPersistence.GetBool("VERTICAL SYNC");
    }

    // Obtiene la opcion FramerateLimit
    private void GetFramerateLimit()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("FPS LIMIT"))
        {
            SetFramerateLimit(framesLimitSize);
        }

        // Obtiene el valor guardado
        sliderFramerateLimitUI.value = dataPersistence.GetInt("FPS LIMIT");
    }

    // Obtiene la opcion FramerateShow
    private void GetFramerateShow()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("SHOW FPS"))
        {
            SetFramerateShow(showFramesOn);
        }

        // Obtiene el valor guardado
        toggleFramerateShowUI.isOn = dataPersistence.GetBool("SHOW FPS");

        // Activa la opcion segun el valor
        settingsManager.SetFramerateShow(toggleFramerateShowUI.isOn);
    }

    // Setea la opcion DisplayMode
    public void SetDisplayModeList(int index)
    {
        // Obtiene la opcion guardada
        int resolution = dataPersistence.GetInt("RESOLUTION");

        // Activa la opcion
        settingsManager.setDisplayModeAndResolution(index, resolution);

        // Guarda la opcion
        dataPersistence.SetInt("DISPLAY MODE", index);
    }

    // Setea la opcion ResolutionList
    public void SetResolutionsList(int index)
    {
        // Obtiene la opcion guardada
        int display = dataPersistence.GetInt("DISPLAY MODE");

        // Activa la opcion
        settingsManager.setDisplayModeAndResolution(display, index);

        // Guarda la opcion
        dataPersistence.SetInt("RESOLUTION", index);
    }

    // Setea la opcion VerticalSync
    public void SetVerticalSync(bool isOn)
    {
        // Activa la opcion
        settingsManager.SetVerticalSync(isOn);

        // Guarda la opcion
        dataPersistence.SetBool("VERTICAL SYNC", isOn);
    }

    // Setea la opcion FramerateLimit
    public void SetFramerateLimit(float size)
    {
        // Activa la opcion
        settingsManager.SetFramerateLimit((int)size);

        // Guarda la opcion
        dataPersistence.SetInt("FPS LIMIT", (int)size);
    }

    // Setea la opcion FramerateShow
    public void SetFramerateShow(bool isOn)
    {
        // Activa la opcion
        settingsManager.SetFramerateShow(isOn);

        // Guarda la opcion
        dataPersistence.SetBool("SHOW FPS", isOn);
    }
}

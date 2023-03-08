using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject playerInterface;
    public GameObject pauseMenu;
    public Button returnButtonPause;

    private GameManager gameManager;
    private PlayerInput playerInput;

    private AudioSource[] audioSources;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        // If button pressed
        if (playerInput.actions["Pause"].triggered)
        {
            // If game not paused
            if (!gameManager.isPaused)
            {
                // Pause game
                PauseGame();
            }
            else
            {
                // Unpause game
                UnpauseGame();
            }
        }
    }

    private void PauseGame()
    {
        // Pauses all sounds
        AllSoundsPaused();

        // Hides game UI and shows pause menu
        playerInterface.SetActive(false);
        pauseMenu.SetActive(true);

        // Shows only the first child
        pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);

        // Selects button return to game
        returnButtonPause.Select();

        // Pause game time scale
        Time.timeScale = 0;

        // Pause is True
        gameManager.isPaused = true;
    }

    public void UnpauseGame()
    {
        // Unpauses all sounds
        AllSoundsUnpaused();

        // Shows game UI and hide pause menu
        playerInterface.SetActive(true);
        pauseMenu.SetActive(false);

        // Unpauses game time scale
        Time.timeScale = 1;

        // Pause is False
        gameManager.isPaused = false;
    }

    private void AllSoundsPaused()
    {
        // Find and pause all audiosources
        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    private void AllSoundsUnpaused()
    {
        // Find and unpause all audiosources
        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }
}
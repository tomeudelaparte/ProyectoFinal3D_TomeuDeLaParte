using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject playerInterface;
    public GameObject pauseMenu;
    public Button returnButtonPause;

    private GameManager gameManager;

    private AudioSource[] audioSources;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManager.isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    private void PauseGame()
    {
        AllSoundsPaused();

        playerInterface.SetActive(false);
        pauseMenu.SetActive(true);

        pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);

        returnButtonPause.Select();

        Time.timeScale = 0;

        gameManager.isPaused = true;
    }

    public void UnpauseGame()
    {
        AllSoundsUnpaused();

        playerInterface.SetActive(true);
        pauseMenu.SetActive(false);

        pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
        pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
        pauseMenu.transform.GetChild(2).gameObject.SetActive(false);

        returnButtonPause.Select();

        Time.timeScale = 1;

        gameManager.isPaused = false;
    }

    private void AllSoundsPaused()
    {
        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    private void AllSoundsUnpaused()
    {
        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }
}

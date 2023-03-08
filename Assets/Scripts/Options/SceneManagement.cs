using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private string sceneName;

    private void Start()
    {
        // Fade in animation
        OpeningTransition();
    }

    public void LoadScene(string name)
    {
        // Gets name scene
        sceneName = name;

        // Starts transition to scene
        StartCoroutine(EndingTransition());
    }

    public void ExitGame()
    {
        // Exits from unity editor and build game
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private void OpeningTransition()
    {
        // Resets time scale to 1
        Time.timeScale = 1;

        // Plays panel canvas animation (Fade In)
        transform.GetChild(0).GetComponent<Animator>().Play("Opening");
    }

    private IEnumerator EndingTransition()
    {
        // Plays panel canvas animation (Fade Out)
        transform.GetChild(0).GetComponent<Animator>().Play("Ending");

        yield return new WaitForSecondsRealtime(2f);

        // After time, load scene
        SceneManager.LoadScene(sceneName);
    }
}

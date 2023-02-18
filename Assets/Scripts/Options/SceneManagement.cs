using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private string sceneName;

    private void Start()
    {
        OpeningTransition();
    }

    public void LoadScene(string name)
    {
        sceneName = name;

        StartCoroutine(EndingTransition());
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                                         Application.Quit();
#endif
    }

    private void OpeningTransition()
    {
        Time.timeScale = 1;

        transform.GetChild(0).GetComponent<Animator>().Play("Opening");
    }

    private IEnumerator EndingTransition()
    {
        transform.GetChild(0).GetComponent<Animator>().Play("Ending");

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textObjective01, textObjective02;

    [SerializeField] private bool isObjective01Completed = false;
    [SerializeField] private bool isObjective02Completed = false;

    private int totalStormCrows;
    private int destroyedStowmCrows = 0;

    private int totalZeppelingObjectives;
    private int destroyedZeppelingObjectives = 0;

    public bool isPaused = false;

    private SceneManagement sceneManagement;

    private void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();

        totalZeppelingObjectives = FindObjectsOfType<EnemyController>().Length;
        totalStormCrows = FindObjectsOfType<Zeppelin>().Length;
    }

    public void ZeppelinObjectiveDestroyed()
    {
        destroyedZeppelingObjectives++;

        CheckZeppelinObjectives();
    }

    public void StormCrowDestroyed()
    {
        destroyedStowmCrows++;

        CheckStormCrows();
    }

    private void CheckStormCrows()
    {
        textObjective01.text = destroyedStowmCrows + "/" + totalStormCrows;

        if (totalStormCrows == destroyedStowmCrows)
        {
            CompleteObjective01();
        }
    }

    private void CheckZeppelinObjectives()
    {
        textObjective02.text = destroyedZeppelingObjectives + "/" + totalZeppelingObjectives;

        if (totalZeppelingObjectives == destroyedZeppelingObjectives)
        {
            CompleteObjective02();
        }
    }

    private void CheckMission()
    {
        if(isObjective01Completed && isObjective02Completed)
        {
            MissionComplete();
        }
    }

    private void MissionComplete()
    {
        if (isObjective01Completed & isObjective02Completed)
        {
            sceneManagement.LoadScene("Mission Complete");
        }
    }

    public void MissionFailed()
    {
        sceneManagement.LoadScene("Mission Failed");
    }

    public void CompleteObjective01()
    {
        if (!isObjective01Completed)
        {
            isObjective01Completed = true;

            CheckMission();
        }
    }

    public void CompleteObjective02()
    {
        if (!isObjective02Completed)
        {
            isObjective02Completed = true;

            CheckMission();
        }
    }
}

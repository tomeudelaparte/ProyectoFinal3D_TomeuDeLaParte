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

    private PlayerInterface playerInterface;

    private SceneManagement sceneManagement;

    private void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        playerInterface = FindObjectOfType<PlayerInterface>();

        totalZeppelingObjectives = FindObjectsOfType<EnemyController>().Length;
        totalStormCrows = FindObjectsOfType<Zeppelin>().Length;
    }

    public void StormCrowDestroyed()
    {
        destroyedStowmCrows++;

        playerInterface.Objective01();

        CheckStormCrows();
    }

    public void ZeppelinObjectiveDestroyed()
    {
        destroyedZeppelingObjectives++;

        playerInterface.Objective02();

        CheckZeppelinObjectives();
    }

    private void CheckStormCrows()
    {
        textObjective01.text = "Eliminate the Storm Crows " + destroyedStowmCrows + "/" + totalStormCrows;

        if (totalStormCrows == destroyedStowmCrows)
        {
            playerInterface.Objective01Complete();

            CompleteObjective01();
        }
    }

    private void CheckZeppelinObjectives()
    {
        textObjective02.text = "Destroy the Baron's Zeppelin " + destroyedZeppelingObjectives + "/" + totalZeppelingObjectives;

        if (totalZeppelingObjectives == destroyedZeppelingObjectives)
        {
            playerInterface.Objective02Complete();

            CompleteObjective02();
        }
    }

    private void CheckMission()
    {
        if (isObjective01Completed && isObjective02Completed)
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

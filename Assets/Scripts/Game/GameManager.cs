using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textObjective01, textObjective02;

    [Header("Objectives")]
    private bool isObjective01Completed = false;
    private bool isObjective02Completed = false;

    [Header("Storm Crows")]
    private int totalStormCrows;
    private int destroyedStowmCrows = 0;

    [Header("Zeppelin")]
    private int totalZeppelingObjectives;
    private int destroyedZeppelingObjectives = 0;

    [Header("Pause")]
    public bool isPaused = false;

    [Header("Player Interface")]
    private PlayerInterface playerInterface;

    [Header("Scene Management")]
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
}

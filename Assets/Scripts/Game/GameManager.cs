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

        // Get total enemies count
        totalStormCrows = FindObjectsOfType<Zeppelin>().Length;

        // Get total zeppelin objectives count
        totalZeppelingObjectives = FindObjectsOfType<EnemyController>().Length;
    }

    public void StormCrowDestroyed()
    {
        // Enemy destroyed +1
        destroyedStowmCrows++;

        // Play animation
        playerInterface.EnemyObjective();

        // Check enemies alive
        CheckStormCrows();
    }

    public void ZeppelinObjectiveDestroyed()
    {
        // Objective destroyed +1
        destroyedZeppelingObjectives++;

        // Play animation
        playerInterface.ZeppelinObjective();

        // Check zeppelin's objectives alive
        CheckZeppelinObjectives();
    }

    private void CheckStormCrows()
    {
        // Update text objective
        textObjective01.text = "Eliminate the Storm Crows " + destroyedStowmCrows + "/" + totalStormCrows;

        // If all enemies are destroyed
        if (totalStormCrows == destroyedStowmCrows)
        {
            // Play animation
            playerInterface.Objective01Complete();

            // Complete Objective
            CompleteObjective01();
        }
    }

    private void CheckZeppelinObjectives()
    {
        // Update text objective
        textObjective02.text = "Destroy the Baron's Zeppelin " + destroyedZeppelingObjectives + "/" + totalZeppelingObjectives;

        // If all objectives are destroyed
        if (totalZeppelingObjectives == destroyedZeppelingObjectives)
        {
            // Play animation
            playerInterface.Objective02Complete();

            // Complete Objective
            CompleteObjective02();
        }
    }

    public void CompleteObjective01()
    {
        // If not completed
        if (!isObjective01Completed)
        {
            // Complete objective is true
            isObjective01Completed = true;

            // Check mission objectives
            CheckMission();
        }
    }

    public void CompleteObjective02()
    {
        // If not completed
        if (!isObjective02Completed)
        {
            // Complete objective is true
            isObjective02Completed = true;

            // Check mission objectives
            CheckMission();
        }
    }

    private void CheckMission()
    {
        // If all objectives are completed
        if (isObjective01Completed && isObjective02Completed)
        {
            // Mission completed
            MissionCompleted();
        }
    }

    private void MissionCompleted()
    {
        // Loads scene
        sceneManagement.LoadScene("Mission Complete");
    }

    public void MissionFailed()
    {
        // Loads scene
        sceneManagement.LoadScene("Mission Failed");
    }
}

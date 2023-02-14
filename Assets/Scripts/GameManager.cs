using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textObjective01, textObjective02;

    [SerializeField] private bool isObjective01Completed = false;
    [SerializeField] private bool isObjective02Completed = false;

    private void Update()
    {
        MissionComplete();
        MissionFailed();
    }

    private void MissionComplete()
    {
        if (isObjective01Completed & isObjective02Completed)
        {
            Debug.Log("MISSION COMPLETE");
        }
    }

    private void MissionFailed()
    {

    }

    public void CompleteObjective01()
    {
        if (!isObjective01Completed)
        {
            isObjective01Completed = true;
        }
    }

    public void CompleteObjective02()
    {
        if (!isObjective02Completed)
        {
            isObjective02Completed = true;
        }
    }
}

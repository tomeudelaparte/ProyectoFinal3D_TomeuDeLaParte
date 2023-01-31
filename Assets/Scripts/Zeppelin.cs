using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour
{
    public GameObject control;
    public GameObject motor01;
    public GameObject motor02;
    public GameObject turret;

    [SerializeField] public bool isControlDestroyed = false;
    [SerializeField] public bool isMotor01Destroyed = false;
    [SerializeField] public bool isMotor02Destroyed = false;
    [SerializeField] public bool isTurretDestroyed = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckStatus();
    }

    private void CheckStatus()
    {
        if (isControlDestroyed && isMotor01Destroyed && isMotor02Destroyed && isTurretDestroyed)
        {
            gameManager.CompleteObjective01();
        }
    }


    public void ControlDestroyed()
    {
        isControlDestroyed = true;
    }

    public void Motor01Destroyed()
    {
        isMotor01Destroyed = true;
    }

    public void Motor02Destroyed()
    {
        isMotor02Destroyed = true;
    }

    public void TurretDestroyed()
    {
        isTurretDestroyed = true;
    }

}

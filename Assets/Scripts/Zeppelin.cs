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
        CheckControl();
        CheckMotor01();
        CheckMotor02();
        CheckTurret();

        if (isControlDestroyed && isMotor01Destroyed && isMotor02Destroyed && isTurretDestroyed)
        {
            gameManager.CompleteObjective01();
        }
    }

    private void CheckControl()
    {
        if (control.gameObject == null && !isControlDestroyed)
        {
            ControlDestroyed();
        }
    }

    private void CheckMotor01()
    {
        if (motor01.gameObject == null && !isMotor01Destroyed)
        {
            Motor01Destroyed();
        }
    }

    private void CheckMotor02()
    {
        if (motor02.gameObject == null && !isMotor02Destroyed)
        {
            Motor02Destroyed();
        }
    }

    private void CheckTurret()
    {
        if (turret.gameObject == null && !isTurretDestroyed)
        {
            TurretDestroyed();
        }
    }

    private void ControlDestroyed()
    {
        isControlDestroyed = true;
    }

    private void Motor01Destroyed()
    {
        isMotor01Destroyed = true;
    }

    private void Motor02Destroyed()
    {
        isMotor02Destroyed = true;
    }

    private void TurretDestroyed()
    {
        isTurretDestroyed = true;
    }

}

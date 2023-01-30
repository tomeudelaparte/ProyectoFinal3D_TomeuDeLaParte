using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour
{
    public GameObject control;
    public GameObject motor01;
    public GameObject motor02;
    public GameObject canon01;
    public GameObject canon02;

    [SerializeField] public bool isControlDestroyed = false;
    [SerializeField] public bool isMotor01Destroyed = false;
    [SerializeField] public bool isMotor02Destroyed = false;
    [SerializeField] public bool isCanon01Destroyed = false;
    [SerializeField] public bool isCanon02Destroyed = false;

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
        CheckCanon01();
        CheckCanon02();

        if (isControlDestroyed && isMotor01Destroyed && isMotor02Destroyed && isCanon01Destroyed && isCanon02Destroyed)
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

    private void CheckCanon01()
    {
        if (canon01.gameObject == null && !isCanon01Destroyed)
        {
            Canon01Destroyed();
        }
    }

    private void CheckCanon02()
    {
        if (canon02.gameObject == null && !isCanon02Destroyed)
        {
            Canon02Destroyed();
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

    private void Canon01Destroyed()
    {
        isCanon01Destroyed = true;
    }

    private void Canon02Destroyed()
    {
        isCanon02Destroyed = true;
    }
}

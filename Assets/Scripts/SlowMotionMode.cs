using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionMode : MonoBehaviour
{
    [SerializeField] private float timeValue = 1;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isPaused)
        {
            Time.timeScale = timeValue;
        }
    }
}

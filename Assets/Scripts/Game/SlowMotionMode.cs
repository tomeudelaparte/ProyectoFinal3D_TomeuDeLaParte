using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionMode : MonoBehaviour
{
    [Header("Animation Value")]
    [SerializeField] private float timeValue = 1;

    [Header("GameManager")]
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // If game not paused
        if (!gameManager.isPaused)
        {
            // Change time scale
            Time.timeScale = timeValue;
        }
    }
}

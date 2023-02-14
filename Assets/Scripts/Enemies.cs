using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    private int totalEnemies;

    void Start()
    {
        totalEnemies = FindObjectsOfType<EnemyController>().Length;
    }

    void Update()
    {
        totalEnemies = FindObjectsOfType<EnemyController>().Length;
    }
}

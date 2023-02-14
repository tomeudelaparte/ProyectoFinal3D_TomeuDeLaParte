using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    private PlayerController playerController;
    private HealthManager playerHealthManager;
    private Gunner playerGunner;

    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;

    public Slider sliderThrust;
    public Slider sliderAmmo;

    public GameObject enemyIndicatorPrefab;
    private GameObject[] enemyIndicators = new GameObject[4];
    private GameObject[] enemies = new GameObject[4];
 
    private int lastHealthValue = 100;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealthManager = playerController.GetComponent<HealthManager>();
        playerGunner = playerController.GetComponent<Gunner>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        PutIndicators();

        UpdateHealth();
        UpdateAcceleration();
        UpdateAmmunation();
    }

    private void Update()
    {
        UpdateHealth();
        UpdateAcceleration();
        UpdateAmmunation();

        CheckEnemyIndicators();
    }

    public void UpdateHealth()
    {
        if (playerHealthManager.GetCurrentHealth() != lastHealthValue)
        {
            sliderHealth.value = playerHealthManager.GetCurrentHealth();

            textHealth.text = playerHealthManager.GetCurrentHealth().ToString();
        }

        lastHealthValue = playerHealthManager.GetCurrentHealth();
    }

    public void UpdateAcceleration()
    {
        sliderThrust.value = playerController.GetCurrentThrust();
    }

    public void UpdateAmmunation()
    {
        sliderAmmo.value = playerGunner.GetCurrentAmmo();
    }

    public void PutIndicators()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            {
                GameObject d = Instantiate(enemyIndicatorPrefab);

                d.GetComponent<EnemyIndicator>().enemy = enemies[i];

                enemyIndicators[i] = d;
            }
        }
    }

    private void CheckEnemyIndicators()
    {
        for (int i = 0; i < enemyIndicators.Length; i++)
        {
            if (enemyIndicators[i] != null)
            {
                if (enemyIndicators[i].GetComponent<EnemyIndicator>().enemy == null)
                {
                    Destroy(enemyIndicators[i]);
                }
            }
        }
    }
}

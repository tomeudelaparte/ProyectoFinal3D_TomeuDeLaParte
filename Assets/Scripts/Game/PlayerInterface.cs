using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [Header("Player Health")]
    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;

    [Header("Player Thrust")]
    public Slider sliderThrust;

    [Header("Player Ammo")]
    public Slider sliderAmmo;
    public TextMeshProUGUI textAmmo;

    [Header("Player Reload")]
    public Image reloadingImage;

    [Header("Player Repair")]
    public TextMeshProUGUI repairingMessage;

    [Header("Objective Healthbar")]
    public Slider sliderObjectiveHealth;
    public TextMeshProUGUI TextObjectiveHealth;

    [Header("Objective Indicator")]
    public GameObject objectiveIndicatorPrefab;
    public GameObject objectiveIndicatorPrefabVariant;

    [Header("Objectives Text")]
    public GameObject objective01Text, objective02Text;

    [Header("Enemies")]
    private GameObject[] enemies = new GameObject[4];

    [Header("Enemies UI")]
    private GameObject[] enemiesUI = new GameObject[4];

    [Header("Player")]
    private PlayerController playerController;

    private HealthManager _playerHealthManager;
    private Gunner _playerGunner;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        _playerHealthManager = playerController.GetComponent<HealthManager>();
        _playerGunner = playerController.GetComponent<Gunner>();


        AddIndicatorToEnemies();
    }

    private void Update()
    {
        UpdateHealth();
        UpdateAcceleration();
        UpdateAmmunation();

        CheckIndicators();
    }

    // Change healthbar value and text
    public void UpdateHealth()
    {
        sliderHealth.value = _playerHealthManager.GetCurrentHealth();

        textHealth.text = _playerHealthManager.GetCurrentHealth() + "%";
    }

    // Change acceleration bar value
    public void UpdateAcceleration()
    {
        sliderThrust.value = playerController.GetCurrentThrust();
    }

    // Change ammo value and text
    public void UpdateAmmunation()
    {
        sliderAmmo.value = _playerGunner.GetCurrentAmmo();

        textAmmo.text = _playerGunner.GetCurrentAmmo().ToString();
    }

    // Start reloading
    public void StartReloading()
    {
        // Hide ammo text
        textAmmo.gameObject.SetActive(false);

        // Show reloading image
        reloadingImage.gameObject.SetActive(true);
    }

    // Stop reloading
    public void StopReloading()
    {
        // Show ammo text
        textAmmo.gameObject.SetActive(true);

        // Hide reloading image
        reloadingImage.gameObject.SetActive(false);
    }

    // Start repairing
    public void StartRepairing()
    {
        // Show message
        repairingMessage.gameObject.SetActive(true);
    }

    // Stop repairing
    public void StopRepairing()
    {
        // Hide message
        repairingMessage.gameObject.SetActive(false);
    }

    // Play animation enemy objective
    public void EnemyObjective()
    {
        objective01Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    // Play animation zeppelin objective
    public void ZeppelinObjective()
    {
        objective02Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    // Play animation Objective 01
    public void Objective01Complete()
    {
        objective01Text.GetComponent<Animator>().Play("ObjectiveComplete");
    }

    // Play animation Objective 02
    public void Objective02Complete()
    {
        objective02Text.GetComponent<Animator>().Play("ObjectiveComplete");
    }

    // Add UI to enemies
    public void AddIndicatorToEnemies()
    {
        // Get all enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            {
                // Get instance GameObject
                GameObject d = Instantiate(objectiveIndicatorPrefab);

                // Add indicator to enemy
                d.GetComponent<EnemyIndicator>().enemy = enemies[i];

                // Save indicator to array
                enemiesUI[i] = d;
            }
        }
    }

    // Check enemies UI
    private void CheckIndicators()
    {
        for (int i = 0; i < enemiesUI.Length; i++)
        {
            // If UI is not null
            if (enemiesUI[i] != null)
            {
                // If UI not have enemy
                if (enemiesUI[i].GetComponent<EnemyIndicator>().enemy == null)
                {
                    // Destroy UI
                    Destroy(enemiesUI[i]);
                }
            }
        }
    }

    // Get enemy healthbar
    public void GetEnemyHealth(string name, float currentHealth, float totalHealth)
    {
        // Show healtbar
        sliderObjectiveHealth.gameObject.SetActive(true);

        // Change values
        sliderObjectiveHealth.maxValue = totalHealth;
        sliderObjectiveHealth.value = currentHealth;

        // Change healtbar text
        TextObjectiveHealth.text = name + " " + currentHealth + "/" + totalHealth;
    }

    // Hide enemy healthbar
    public void HideEnemyHealth()
    {
        sliderObjectiveHealth.gameObject.SetActive(false);
    }
}

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


        // Adds UI to enemies
        AddIndicatorToEnemies();
    }

    private void Update()
    {
        // Updating healthbar
        UpdateHealth();

        // Updating thrustbar
        UpdateAcceleration();

        // Updating ammobar
        UpdateAmmunation();

        // Checking enemy UI
        CheckIndicators();
    }

    public void UpdateHealth()
    {
        // Changes healthbar value
        sliderHealth.value = _playerHealthManager.GetCurrentHealth();

        // Changes healthbar text
        textHealth.text = _playerHealthManager.GetCurrentHealth() + "%";
    }

    public void UpdateAcceleration()
    {
        // Changes thrust bar value
        sliderThrust.value = playerController.GetCurrentThrust();
    }

    public void UpdateAmmunation()
    {
        // Changes ammo value
        sliderAmmo.value = _playerGunner.GetCurrentAmmo();

        // Changes ammo text
        textAmmo.text = _playerGunner.GetCurrentAmmo().ToString();
    }

    public void StartReloading()
    {
        // Hides ammo text
        textAmmo.gameObject.SetActive(false);

        // Shows reloading image
        reloadingImage.gameObject.SetActive(true);
    }

    public void StopReloading()
    {
        // Shows ammo text
        textAmmo.gameObject.SetActive(true);

        // Hides reloading image
        reloadingImage.gameObject.SetActive(false);
    }

    public void StartRepairing()
    {
        // Shows message
        repairingMessage.gameObject.SetActive(true);
    }

    public void StopRepairing()
    {
        // Hides message
        repairingMessage.gameObject.SetActive(false);
    }

    public void EnemyObjective()
    {
        // Plays animation
        objective01Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    public void ZeppelinObjective()
    {
        // Plays animation
        objective02Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    public void Objective01Complete()
    {
        // Plays animation
        objective01Text.GetComponent<Animator>().Play("ObjectiveComplete");
    }

    public void Objective02Complete()
    {
        // Plays animation
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

    // Shows enemy healthbar
    public void GetEnemyHealth(string name, float currentHealth, float totalHealth)
    {
        // Shows healtbar
        sliderObjectiveHealth.gameObject.SetActive(true);

        // Changes values
        sliderObjectiveHealth.maxValue = totalHealth;
        sliderObjectiveHealth.value = currentHealth;

        // Changes healtbar text
        TextObjectiveHealth.text = name + " " + currentHealth + "/" + totalHealth;
    }

    // Hides enemy healthbar
    public void HideEnemyHealth()
    {
        sliderObjectiveHealth.gameObject.SetActive(false);
    }
}

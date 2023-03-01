using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;

    public Slider sliderThrust;
    public Slider sliderAmmo;
    public TextMeshProUGUI textAmmo;
    public Image reloadingImage;
    public TextMeshProUGUI repairingMessage;

    public Slider sliderObjectiveHealth;
    public TextMeshProUGUI TextObjectiveHealth;

    public GameObject enemyIndicatorPrefab;

    public GameObject objective01Text, objective02Text;

    private GameObject[] enemyIndicators = new GameObject[4];
    private GameObject[] enemies = new GameObject[4];

    private PlayerController playerController;
    private HealthManager playerHealthManager;
    private Gunner playerGunner;

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

        StopReloading();
        StopRepairing();
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
        sliderHealth.value = playerHealthManager.GetCurrentHealth();

        textHealth.text = playerHealthManager.GetCurrentHealth() + "%";
    }

    public void UpdateAcceleration()
    {
        sliderThrust.value = playerController.GetCurrentThrust();
    }

    public void UpdateAmmunation()
    {
        sliderAmmo.value = playerGunner.GetCurrentAmmo();

        textAmmo.text = playerGunner.GetCurrentAmmo().ToString();
    }

    public void StartReloading()
    {
        textAmmo.gameObject.SetActive(false);

        reloadingImage.gameObject.SetActive(true);
    }

    public void StopReloading()
    {
        textAmmo.gameObject.SetActive(true);
        reloadingImage.gameObject.SetActive(false);
    }

    public void StartRepairing()
    {
        repairingMessage.gameObject.SetActive(true);
    }

    public void StopRepairing()
    {
        repairingMessage.gameObject.SetActive(false);
    }

    public void Objective01()
    {
        objective01Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    public void Objective02()
    {
        objective02Text.GetComponent<Animator>().Play("Objective", -1, 0.0f);
    }

    public void Objective01Complete()
    {
        objective01Text.GetComponent<Animator>().Play("ObjectiveComplete");
    }

    public void Objective02Complete()
    {
        objective02Text.GetComponent<Animator>().Play("ObjectiveComplete");
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

    public void GetEnemyHealth(string name, float currentHealth, float totalHealth)
    {
        sliderObjectiveHealth.gameObject.SetActive(true);

        sliderObjectiveHealth.maxValue = totalHealth;
        sliderObjectiveHealth.value = currentHealth;

        TextObjectiveHealth.text = name + " " + currentHealth + "/" + totalHealth;
    }

    public void HideEnemyHealth()
    {
        sliderObjectiveHealth.gameObject.SetActive(false);
    }
}

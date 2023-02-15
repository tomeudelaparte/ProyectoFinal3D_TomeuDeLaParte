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
    public TextMeshProUGUI textAmmo;
    public Image reloadingImage;
    public TextMeshProUGUI repairingMessage;

    public GameObject enemyIndicatorPrefab;
    private GameObject[] enemyIndicators = new GameObject[4];
    private GameObject[] enemies = new GameObject[4];

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    private PlayerController playerController;
    private HealthManager playerHealthManager;
    private GunnerBehaviour playerGunner;

    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;

    public Slider sliderThrust;
    public Slider sliderAmmo;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealthManager = playerController.GetComponent<HealthManager>();
        playerGunner = playerController.GetComponent<GunnerBehaviour>();

        UpdateHealth();
        UpdateAcceleration();
        UpdateAmmunation();
    }

    private void Update()
    {
        UpdateHealth();
        UpdateAcceleration();
        UpdateAmmunation();
    }

    public void UpdateHealth()
    {
        sliderHealth.value = playerHealthManager.GetCurrentHealth();
        textHealth.text = playerHealthManager.GetCurrentHealth().ToString();
    }

    public void UpdateAcceleration()
    {
        sliderThrust.value = playerController.GetCurrentThrust();
    }

    public void UpdateAmmunation()
    {
        sliderAmmo.value = playerGunner.GetCurrentAmmo();
    }
}

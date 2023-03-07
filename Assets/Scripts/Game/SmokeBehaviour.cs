using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    public GameObject smoke;

    private ParticleSystem smokeParticle;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();

        smokeParticle = smoke.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Current health to value between 0 and 1
        float value = float.Parse(healthManager.GetCurrentHealth().ToString()) / 100f;

        // Gets particle system main
        var main = smokeParticle.main;

        // If value is less than 1
        if (value < 1)
        {
            // Play smoke trail
            smoke.SetActive(true);
        }

        // Changes color according to health
        main.startColor = new Color(value, value, value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    public GameObject smokeParticles;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {
        float value = float.Parse(healthManager.GetCurrentHealth().ToString()) * 255f / 100f;

        smokeParticles.GetComponent<ParticleSystem>().startColor = new Color(value, value, value, 64);
    }
}

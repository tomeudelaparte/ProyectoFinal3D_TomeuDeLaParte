using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    public GameObject smokeParticles;

    private ParticleSystem smokePs;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();

        smokePs = smokeParticles.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        float value = float.Parse(healthManager.GetCurrentHealth().ToString()) / 100f;

        var main = smokePs.main;

        if(value<1)
        {
            smokeParticles.SetActive(true);
        }


        main.startColor = new Color(value, value, value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    public ParticleSystem smokeParticles;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {
        float value = float.Parse(healthManager.GetCurrentHealth().ToString()) * 255f / 100f;

        var ps = new ParticleSystem.EmitParams();

        ps.startColor = new Color(value, value, value, 64);



        smokeParticles.Emit(ps,200);
    }
}

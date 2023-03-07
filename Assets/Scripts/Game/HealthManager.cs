using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool isPlayer;

    public string objectiveName;

    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int currentHealth;

    [Header("Prefabs")]
    public GameObject planeExplosion;

    [Header("Visuals")]
    private VisualEffects visualEffects;

    [Header("GameManager")]
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        visualEffects = FindObjectOfType<VisualEffects>();

        UpdateMaxHealth(maxHealth);
    }

    public string GetName()
    {
        return objectiveName;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;

        UpdateSaturation();
    }

    public void DamageThis(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            UpdateSaturation();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            DestroyPlane();
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    private void UpdateSaturation()
    {
        if (isPlayer)
        {
            int tmp = (100 - currentHealth) * -1;

            visualEffects.UpdateSaturation(tmp / 2);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && IsGroundInFront())
        {
            DestroyPlane();
        }
    }

    private void DestroyPlane()
    {
        Instantiate(planeExplosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

        if (isPlayer)
        {
            gameManager.MissionFailed();
        }

        if (!isPlayer)
        {
            gameManager.StormCrowDestroyed();

            UnparentSmoke();

            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }

        Destroy(gameObject);
    }

    private void UnparentSmoke()
    {
        GameObject smoke = gameObject.transform.Find("Smoke").gameObject;

        smoke.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        smoke.transform.parent = null;

        Destroy(smoke, 20f);
    }

    private bool IsGroundInFront()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, 5f);

        if (hitData.collider != null)
        {
            if (hitData.collider.CompareTag("Ground"))
            {
                return true;
            }

            return false;
        }

        return false;
    }
}
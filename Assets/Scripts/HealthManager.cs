using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool isPlayer;

    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int currentHealth;

    public GameObject planeExplosion;

    private VisualEffects visualEffects;

    private void Start()
    {
        visualEffects = FindObjectOfType<VisualEffects>();

        UpdateMaxHealth(maxHealth);
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

    public void DamageCharacter(int damage)
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
        if (other.gameObject.CompareTag("Ground"))
        {
            DestroyPlane();
        }
    }

    private void DestroyPlane()
    {
        Instantiate(planeExplosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

        if (!isPlayer)
        {
            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }

        Destroy(gameObject);
    }
}

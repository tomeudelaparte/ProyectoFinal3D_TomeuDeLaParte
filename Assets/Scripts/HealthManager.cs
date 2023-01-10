using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int currentHealth;

    [Header("Trigger Fix")]
    public bool isTriggering = false;

    private void Start()
    {
        UpdateMaxHealth(maxHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        if (!isTriggering)
        {
            maxHealth = newMaxHealth;
            currentHealth = maxHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blast"))
        {
            if (isTriggering) return;
            isTriggering = true;

            StartCoroutine(TriggerEnterOn());
        }
    }

    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isTriggering = false;
    }
}

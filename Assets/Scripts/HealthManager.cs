using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int currentHealth;

    public GameObject planeExplosion;

    private void Start()
    {
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
    }

    public void DamageCharacter(int damage)
    {

        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            DestroyPlane();
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
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

        FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");

        Destroy(gameObject);
    }
}

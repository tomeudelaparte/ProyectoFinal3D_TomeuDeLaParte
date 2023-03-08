using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string objectiveName;
    [SerializeField] private int maxHealth = 200;
    [SerializeField] private int currentHealth;

    [Header("Prefabs")]
    public GameObject explosion;

    [Header("GameObjects")]
    public GameObject smoke01;
    public GameObject smoke02;

    [Header("Destroyed")]
    public bool isDestroyed = false;

    [Header("GameManager")]
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Updates current health
        UpdateCurrentHealth();
    }

    public void AddDamage(int value)
    {
        // If current health is more than 0
        if (currentHealth > 0)
        {
            // Current health -value
            currentHealth -= value;
        }

        // If current health is less than or equals 0
        if (currentHealth <= 0)
        {
            // Destroy
            DestroyObjective();
        }
    }

    public void UpdateCurrentHealth()
    {
        // Gets max health
        currentHealth = maxHealth;
    }

    private void DestroyObjective()
    {
        // If not destroyed
        if (!isDestroyed)
        {
            // Destroy
            IsDestroyed();

            // Instance explosion
            Instantiate(explosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

            // Plays smoke
            SmokeActive();

            // Plays slow motion
            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }
    }

    private void SmokeActive()
    {
        // If smoke01 not null
        if (smoke01 != null)
        {
            // Smoke 01 active
            smoke01.gameObject.SetActive(true);
        }

        // If smoke02 not null
        if (smoke02 != null)
        {
            // Smoke 02 active
            smoke02.gameObject.SetActive(true);
        }
    }

    private void IsDestroyed()
    {
        // Destroy
        isDestroyed = true;

        gameManager.ZeppelinObjectiveDestroyed();
    }

    // Return name
    public string GetName()
    {
        return objectiveName;
    }

    // Return current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Return max health
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // Add current health
    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;
    }
}



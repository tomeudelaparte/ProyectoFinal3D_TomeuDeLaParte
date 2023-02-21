using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int maxHealth = 200;

    [SerializeField] private int currentHealth;

    public GameObject explosion;

    public GameObject smoke01;
    public GameObject smoke02;

    public bool isDestroyed = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
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

    private void DestroyPlane()
    {
        if (!isDestroyed)
        {
            Instantiate(explosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

            SmokeActive();
            IsDestroyed();

            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }
    }

    private void SmokeActive()
    {
        if (smoke01 != null)
        {
            smoke01.gameObject.SetActive(true);
        }

        if (smoke02 != null)
        {
            smoke02.gameObject.SetActive(true);
        }
    }

    private void IsDestroyed()
    {
        isDestroyed = true;

        gameManager.ZeppelinObjectiveDestroyed();
    }
}



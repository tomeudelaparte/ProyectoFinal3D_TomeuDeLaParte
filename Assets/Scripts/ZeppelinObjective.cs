using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinObjective : MonoBehaviour
{
    private Zeppelin zeppelin;

    [SerializeField] private int id;
    [SerializeField] private int maxHealth = 200;

    [SerializeField] private int currentHealth;

    public GameObject explosion;

    public GameObject smoke01;
    public GameObject smoke02;

    private bool destroyed = false;

    private void Start()
    {
        zeppelin = FindObjectOfType<Zeppelin>();

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
        if (!destroyed)
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
        if (id == 1)
        {
            zeppelin.ControlDestroyed();
        }

        if (id == 2)
        {
            zeppelin.TurretDestroyed();
        }
        if (id == 3)
        {
            zeppelin.Motor01Destroyed();
        }
        if (id == 4)
        {
            zeppelin.Motor02Destroyed();
        }

        destroyed = true;
    }
}



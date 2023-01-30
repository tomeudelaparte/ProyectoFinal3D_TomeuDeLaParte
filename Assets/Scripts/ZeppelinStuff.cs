using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinStuff : MonoBehaviour
{
    private Zeppelin zeppelin;



    [SerializeField] private int maxHealth = 500;

    [SerializeField] private int currentHealth;

    public GameObject planeExplosion;

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
        //Instantiate(planeExplosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

        FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");

        Destroy(gameObject);
    }
}

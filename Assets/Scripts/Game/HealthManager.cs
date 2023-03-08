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

        // Updates current health
        UpdateCurrentHealth();
    }

    public void AddHealth(int value)
    {
        currentHealth += value;

        // Changes post-processing saturation
        UpdateSaturation();
    }

    public void AddDamage(int value)
    {
        // If current health is more than 0
        if (currentHealth > 0)
        {
            // Current health -value
            currentHealth -= value;

            // Change post-processing saturation
            UpdateSaturation();
        }

        // If current health is less than or equals 0
        if (currentHealth <= 0)
        {
            // Current health to zero
            currentHealth = 0;

            // Destroy
            DestroyPlane();
        }
    }

    public void UpdateCurrentHealth()
    {
        // Gets max health
        currentHealth = maxHealth;
    }

    private void UpdateSaturation()
    {
        // If is Player
        if (isPlayer)
        {
            // Gets negative value from current health
            int tmp = (100 - currentHealth) * -1;

            // Changes saturation
            visualEffects.UpdateSaturation(tmp / 2);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If collision is GROUND and is in front
        if (other.gameObject.CompareTag("Ground") && IsGroundInFront())
        {
            // Destroy this GameObject
            DestroyPlane();
        }
    }

    private void DestroyPlane()
    {
        // If is player
        if (isPlayer)
        {
            // Mission failed
            gameManager.MissionFailed();
        }

        // If not player
        if (!isPlayer)
        {
            // Enemy destroyed
            gameManager.StormCrowDestroyed();

            // Unparent the trail smoke GameObject
            UnparentTrailSmoke();

            // Plays slow motion
            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }

        // Instance explosion
        Instantiate(planeExplosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

        // Destroy
        Destroy(gameObject);
    }

    private void UnparentTrailSmoke()
    {
        // Gets smoke object
        GameObject smoke = gameObject.transform.Find("Smoke").gameObject;

        // Stops emmiting smoke
        smoke.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);

        // Unparent smoke
        smoke.transform.parent = null;

        // Destroy after time
        Destroy(smoke, 20f);
    }

    private bool IsGroundInFront()
    {
        // Raycast
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, 5f);

        // If collider not null
        if (hitData.collider != null)
        {
            // If collider is GROUND
            if (hitData.collider.CompareTag("Ground"))
            {
                return true;
            }

            return false;
        }

        return false;
    }

    // Return name
    public string GetName()
    {
        return objectiveName;
    }

    // Return max health
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // Get current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

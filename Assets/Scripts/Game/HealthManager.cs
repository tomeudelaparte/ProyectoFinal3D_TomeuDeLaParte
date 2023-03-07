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

        // Update max health
        UpdateMaxHealth(maxHealth);
    }

    // Add current health
    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;

        // Change post-processing saturation
        UpdateSaturation();
    }

    // Add damage
    public void Damage(int value)
    {
        // If current health is more than 0
        if (currentHealth > 0)
        {
            // Current health -value
            currentHealth -= value;

            // Change post-processing saturation
            UpdateSaturation();
        }

        // If current health is minor than or equals 0
        if (currentHealth <= 0)
        {
            // Current health to zero
            currentHealth = 0;

            // Destroy
            DestroyPlane();
        }
    }

    // Change current health to max health
    public void UpdateMaxHealth(int value)
    {
        maxHealth = value;
        currentHealth = maxHealth;
    }

    // Change post-processing saturation
    private void UpdateSaturation()
    {
        // If is Player
        if (isPlayer)
        {
            // Get negative value from current health
            int tmp = (100 - currentHealth) * -1;

            // Change saturation
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

    // Destroy this GameObject
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

            // Play slow motion
            FindObjectOfType<SlowMotionMode>().GetComponent<Animator>().Play("SlowMotion");
        }

        // Instance explosion
        Instantiate(planeExplosion, transform.GetChild(0).position, transform.GetChild(0).rotation);

        // Destroy
        Destroy(gameObject);
    }

    // Unparent the trail smoke GameObject
    private void UnparentTrailSmoke()
    {
        // Get smoke object
        GameObject smoke = gameObject.transform.Find("Smoke").gameObject;

        // Stop emmiting smoke
        smoke.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);

        // Unparent smoke
        smoke.transform.parent = null;

        // Destroy after time
        Destroy(smoke, 20f);
    }

    // Check ground in front
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

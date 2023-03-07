using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Physics Core")]
    public Transform planeCore;

    [Header("Physics Values")]
    public float thrust = 100f;
    public Vector3 turnTorque = new Vector3(25f, 10f, 42f);
    public float forceMult = 100f;

    [Header("Rotation Values")]
    private float pitch = 0f;
    private float yaw = 0f;
    private float roll = 0f;

    [Header("Thrust")]
    private bool accelerateTrigger = true;
    private float thrustCooldownTime = 0.001f;

    [Header("Repair")]
    private bool repairTrigger = true;
    private float repairCooldownTime = 1f;

    [Header("Rigidbody")]
    private Rigidbody _playerRigidbody;

    [Header("HealthManager")]
    private HealthManager _healthManager;

    [Header("Gunner")]
    private Gunner _gunner;

    [Header("Visuals")]
    private VisualEffects visualEffects;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Interface")]
    private PlayerInterface playerInterface;

    [Header("GameManager")]
    private GameManager gameManager;

    [Header("Control Inputs")]
    private PlayerInput playerInput;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInput = FindObjectOfType<PlayerInput>();

        _playerRigidbody = GetComponent<Rigidbody>();
        _gunner = GetComponent<Gunner>();
        _healthManager = GetComponent<HealthManager>();

        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();
    }

    void Update()
    {
        // If the game is not paused
        if (!gameManager.isPaused)
        {
            // Player can shoot, repair, reload and get enemy health
            Shoot();
            RepairSystem();
            ReloadingSystem();
            GetObjectiveHealth();
        }
    }

    private void FixedUpdate()
    {
        // If the game is not paused
        if (!gameManager.isPaused)
        {
            // Player can move and accelerate
            Movement();
            ThrustSystem();
        }
    }

    // MOVEMENT
    private void Movement()
    {
        // If not repairing
        if (repairTrigger)
        {
            // Values (X, Y, Z)
            yaw = playerInput.actions["Yaw"].ReadValue<float>(); ;
            pitch = playerInput.actions["Vertical"].ReadValue<float>();
            roll = playerInput.actions["Horizontal"].ReadValue<float>();

            // Torque force (X, Y, Z)
            _playerRigidbody.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
        }

        // Forward force
        _playerRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
    }

    // SHOOT
    private void Shoot()
    {
        // If button is pressed (hold) and Player not repairing
        if (playerInput.actions["Shoot"].IsPressed() && repairTrigger)
        {
            // SHOOT
            _gunner.Shoot();
        }
    }

    // ACCELERATION
    private void ThrustSystem()
    {
        // If button is pressed (hold) and Player not accelerating
        if (playerInput.actions["Thrust"].IsPressed() && accelerateTrigger)
        {
            // Acceleration is less than 300
            if (thrust < 300)
            {
                // Acceleration +1
                thrust += 1;

                // Increase FOV
                visualEffects.IncreaseFOV(0.08f);

                // Acceleration Cooldown
                StartCoroutine(ThrustCooldown());
            }
        }
        else
        {
            // Acceleration is greater than 150
            if (thrust > 150)
            {
                // Acceleration -1
                thrust -= 1;

                // Decrease FOV
                visualEffects.DecreaseFOV(0.08f);

                // Acceleration Cooldown
                StartCoroutine(ThrustCooldown());
            }
        }
    }

    // REPAIR
    private void RepairSystem()
    {
        // Save coroutine
        IEnumerator co = ReapairCooldown();

        // If button is pressed (hold) and Player not repairing
        if (playerInput.actions["Repair"].IsPressed() && repairTrigger)
        {
            // If health is less than 100
            if (_healthManager.GetCurrentHealth() < 100)
            {
                // Start repairing
                StartCoroutine(co);
            }
        }

        // If button is not pressed (hold) and Player not repairing
        if (!playerInput.actions["Repair"].IsPressed() && repairTrigger)
        {
            // Stop repairing
            StopCoroutine(co);
        }
    }

    // RELOADING
    private void ReloadingSystem()
    {
        // If button is pressed and Player not repairing
        if (playerInput.actions["Reload"].triggered)
        {
            // RELOAD
            _gunner.Reload();
        }
    }

    // THRUST COOLDOWN
    private IEnumerator ThrustCooldown()
    {
        // False to True
        accelerateTrigger = false;
        yield return new WaitForSeconds(thrustCooldownTime);
        accelerateTrigger = true;
    }

    // REPAIR COOLDOWN
    private IEnumerator ReapairCooldown()
    {
        // Play sound
        soundEffects.StartReapairing();

        // Play messages
        playerInterface.StartRepairing();

        // False to True
        repairTrigger = false;
        yield return new WaitForSeconds(repairCooldownTime);
        repairTrigger = true;

        // Add health
        _healthManager.AddToCurrentHealth(5);

        // Stop sound
        soundEffects.StopReapairing();

        // Stop message
        playerInterface.StopRepairing();
    }

    // GET ENEMY HEALTH
    private void GetObjectiveHealth()
    {
        // Raycast forward
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, 3000);
        Debug.DrawRay(transform.position, transform.forward * 3000, Color.cyan);

        // If collider is not null
        if (hitData.collider != null)
        {
            // If collider is HealthTrigger
            if (hitData.collider.CompareTag("HealthTrigger"))
            {
                // Get health data from enemy
                HealthManager enemyHealthManager = hitData.collider.GetComponentInParent<HealthManager>();

                // Get Name, Current Health, Max Health
                playerInterface.GetEnemyHealth(enemyHealthManager.GetName(), enemyHealthManager.GetCurrentHealth(), enemyHealthManager.GetMaxHealth());
            }

            // If collider is HealthTrigger
            else if (hitData.collider.CompareTag("Zeppelin"))
            {
                // Get health data from Zeppelin
                Zeppelin enemyHealthManager = hitData.collider.GetComponent<Zeppelin>();

                // Get Name, Current Health, Max Health
                playerInterface.GetEnemyHealth(enemyHealthManager.GetName(), enemyHealthManager.GetCurrentHealth(), enemyHealthManager.GetMaxHealth());
            }
            else
            {
                // Hide enemy healthbar
                playerInterface.HideEnemyHealth();
            }
        }
        else
        {
            // Hide enemy healthbar
            playerInterface.HideEnemyHealth();
        }
    }

    // Return thrust value
    public float GetCurrentThrust()
    {
        return thrust;
    }
}

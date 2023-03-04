using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform planeCore;

    [Header("Physics Values")]
    public float thrust = 100f;
    public Vector3 turnTorque = new Vector3(25f, 10f, 42f);
    public float forceMult = 100f;

    [Header("Rotation Values")]
    [SerializeField] [Range(-1f, 1f)] private float pitch = 0f;
    [SerializeField] [Range(-1f, 1f)] private float yaw = 0f;
    [SerializeField] [Range(-1f, 1f)] private float roll = 0f;

    [Header("Gunner System")]
    private Gunner gunnerSystem;

    [Header("Rigidbody")]
    private Rigidbody playerRigidbody;

    [Header("HealthManager")]
    private HealthManager healthManager;

    [Header("Control Inputs")]
    private PlayerInput playerInput;

    [Header("Thrust")]
    private bool accelerateTrigger = true;
    private float thrustCooldownTime = 0.001f;

    [Header("Repair")]
    private bool repairTrigger = true;
    private float repairCooldownTime = 1f;

    [Header("Visuals")]
    private VisualEffects visualEffects;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Interface")]
    private PlayerInterface playerInterface;

    [Header("GameManager")]
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerInput = FindObjectOfType<PlayerInput>();

        playerRigidbody = GetComponent<Rigidbody>();
        gunnerSystem = GetComponent<Gunner>();
        healthManager = GetComponent<HealthManager>();

        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();
    }

    void Update()
    {
        if (!gameManager.isPaused)
        {
            Shoot();
            ThrustSystem();
            RepairSystem();
            ReloadingSystem();
            GetObjectiveHealth();
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.isPaused)
        {
            Movement();
        }
    }

    private void Shoot()
    {
        if (playerInput.actions["Shoot"].IsPressed() && repairTrigger)
        {
            gunnerSystem.Shoot();
        }
    }

    private void Movement()
    {
        if (repairTrigger)
        {
            roll = playerInput.actions["Horizontal"].ReadValue<float>();
            pitch = playerInput.actions["Vertical"].ReadValue<float>();
            yaw = playerInput.actions["Yaw"].ReadValue<float>();;

            playerRigidbody.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
        }

        playerRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
    }

    private void ThrustSystem()
    {
        if (playerInput.actions["Thrust"].IsPressed() && accelerateTrigger)
        {
            if (thrust < 300)
            {
                thrust += 1;

                visualEffects.IncreaseFOV(0.08f);

                StartCoroutine(ThrustCooldown());
            }
        }
        else
        {
            if (thrust > 150)
            {
                thrust -= 1;

                visualEffects.DecreaseFOV(0.08f);

                StartCoroutine(ThrustCooldown());
            }
        }
    }

    private void RepairSystem()
    {
        IEnumerator co = ReapairCooldown();

        if (playerInput.actions["Repair"].IsPressed() && repairTrigger)
        {

            if (healthManager.GetCurrentHealth() < 100)
            {
                StartCoroutine(co);
            }
        }

        if (!playerInput.actions["Repair"].IsPressed() && repairTrigger)
        {
            StopCoroutine(co);
        }
    }

    private void ReloadingSystem()
    {
        if (playerInput.actions["Reload"].triggered)
        {
            gunnerSystem.Reload();
        }
    }

    private IEnumerator ThrustCooldown()
    {
        accelerateTrigger = false;
        yield return new WaitForSeconds(thrustCooldownTime);
        accelerateTrigger = true;
    }

    private IEnumerator ReapairCooldown()
    {
        soundEffects.StartReapairing();
        playerInterface.StartRepairing();

        repairTrigger = false;
        yield return new WaitForSeconds(repairCooldownTime);
        repairTrigger = true;

        healthManager.AddToCurrentHealth(5);

        soundEffects.StopReapairing();
        playerInterface.StopRepairing();
    }

    public float GetCurrentThrust()
    {
        return thrust;
    }

    private void GetObjectiveHealth()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, 3000);
        Debug.DrawRay(transform.position, transform.forward * 3000, Color.cyan);

        if (hitData.collider != null)
        {
            if (hitData.collider.CompareTag("HealthTrigger"))
            {
                HealthManager enemyHealthManager = hitData.collider.GetComponentInParent<HealthManager>();

                playerInterface.GetEnemyHealth(enemyHealthManager.GetName(), enemyHealthManager.GetCurrentHealth(), enemyHealthManager.GetMaxHealth());
            }
            else if (hitData.collider.CompareTag("Zeppelin"))
            {
                Zeppelin enemyHealthManager = hitData.collider.GetComponent<Zeppelin>();

                playerInterface.GetEnemyHealth(enemyHealthManager.GetName(), enemyHealthManager.GetCurrentHealth(), enemyHealthManager.GetMaxHealth());
            }
            else
            {
                playerInterface.HideEnemyHealth();
            }
        }
        else
        {
            playerInterface.HideEnemyHealth();
        }
    }
}

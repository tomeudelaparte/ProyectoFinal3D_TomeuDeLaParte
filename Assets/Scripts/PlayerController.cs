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
    [SerializeField] private float horizontalInput, verticalInput, yawInput;
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

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        gunnerSystem = GetComponent<Gunner>();
        healthManager = GetComponent<HealthManager>();

        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();
    }
    private void OnShoot(InputValue value)
    {
        Shoot();
    }

    void Update()
    {
        /*
        ThrustSystem();
        RepairSystem();
        ReloadingSystem();
        */
    }

    private void FixedUpdate()
    {
        //Movement();
    }

    private void Shoot()
    {
        if (repairTrigger)
        {
            gunnerSystem.Shoot();
        }
    }

    private void Movement()
    {
        if (repairTrigger)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            yawInput = Input.GetAxis("Yaw");

            pitch = verticalInput;
            roll = horizontalInput;
            yaw = yawInput;

            playerRigidbody.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
        }

        playerRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
    }

    private void ThrustSystem()
    {
        if ((Input.GetAxisRaw("Thrust") > 0 || Input.GetKey(KeyCode.W)) && accelerateTrigger)
        {
            if (thrust < 300)
            {
                thrust += 1;

                visualEffects.IncreaseFOV(0.08f);

                StartCoroutine(ThrustCooldown());
            }
        }

        if ((Input.GetAxisRaw("Thrust") <= 0 || Input.GetKey(KeyCode.W)) && accelerateTrigger)
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

        if ((Input.GetButton("Repair") || Input.GetKey(KeyCode.X)) && repairTrigger)
        {

            if (healthManager.GetCurrentHealth() < 100)
            {
                StartCoroutine(co);
            }
        }

        if ((Input.GetButtonUp("Repair") || Input.GetKeyUp(KeyCode.X)) && repairTrigger)
        {
            StopCoroutine(co);
        }
    }

    private void ReloadingSystem()
    {
        if (Input.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.R))
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
}

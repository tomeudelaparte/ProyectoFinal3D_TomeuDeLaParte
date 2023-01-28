using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GunnerBehaviour gunnerSystem;

    [Header("Rigidbody")]
    private Rigidbody playerRigidbody;

    [Header("HealthManager")]
    private HealthManager healthManager;

    [Header("Control Inputs")]
    private float horizontalInput, verticalInput, yawInput;

    [Header("Thrust")]
    private bool accelerateTrigger = true;
    private float thrustCooldownTime = 0.001f;

    [Header("Repair")]
    private bool repairTrigger = true;
    private float repairCooldownTime = 1f;

    [Header("Conditionals")]
    private bool isRepairing = false;

    [Header("Visuals")]
    private VisualEffects visualEffects;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        gunnerSystem = GetComponent<GunnerBehaviour>();
        healthManager = GetComponent<HealthManager>();

        visualEffects = FindObjectOfType<VisualEffects>();
    }

    void Update()
    {
        Shoot();
        ThrustSystem();
        RepairSystem();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Shoot()
    {
        if (Input.GetAxisRaw("Shoot") > 0 || Input.GetKey(KeyCode.Space))
        {
            gunnerSystem.Shoot();
        }
    }

    private void Movement()
    {
        if (!isRepairing)
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
        if ((Input.GetButton("Repair") || Input.GetKey(KeyCode.X)) && repairTrigger)
        {
            if (healthManager.GetCurrentHealth() < 100)
            {
                StartCoroutine(ReapairCooldown());

                healthManager.AddToCurrentHealth(5);

                isRepairing = true;
            }
            else
            {
                isRepairing = false;
            }
        }
        else
        {
            isRepairing = false;
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
        repairTrigger = false;
        yield return new WaitForSeconds(repairCooldownTime);
        repairTrigger = true;
    }

    public float GetCurrentThrust()
    {
        return thrust;
    }
}

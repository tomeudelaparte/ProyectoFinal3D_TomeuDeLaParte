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
    private float horizontalInput, verticalInput;

    [Header("Acceleration")]
    private bool accelerateTrigger = true;
    private float thrustCooldownTime = 0.01f;

    [Header("Repair")]
    private bool repairTrigger = true;
    private float repairCooldownTime = 0.5f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        gunnerSystem = GetComponent<GunnerBehaviour>();
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {
        Shoot();
        Accelerate();
        Repair();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Shoot()
    {
        if (Input.GetAxisRaw("Shoot") > 0 && gunnerSystem.shootTrigger)
        {
            gunnerSystem.Shoot();
        }
    }
    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        pitch = verticalInput;
        roll = horizontalInput;

        yaw = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            yaw = -1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            yaw = 1f;
        }

        playerRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
        playerRigidbody.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && accelerateTrigger)
        {
            if (thrust < 250)
            {
                thrust += 1;

                StartCoroutine(ThrustCooldown());
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && accelerateTrigger)
        {
            if (thrust > 100)
            {
                thrust -= 1;

                StartCoroutine(ThrustCooldown());
            }
        }
    }

    private void Repair()
    {
        if (Input.GetKey(KeyCode.X) && repairTrigger)
        {
            if (healthManager.GetCurrentHealth() < 100)
            {
                healthManager.AddToCurrentHealth(2);

                StartCoroutine(ReapairCooldown());
            }
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
}

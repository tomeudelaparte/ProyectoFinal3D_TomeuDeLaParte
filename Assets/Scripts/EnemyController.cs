using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform planeCore;
    public GameObject enemyRaycast;

    [Header("Physics Values")]
    public float torque = 10f;
    public float thrust = 100f;
    private float forceMult = 100f;

    [Header("Gunner System")]
    private GunnerBehaviour gunnerSystem;

    [Header("Player Position")]
    private Transform playerCore;

    [Header("Rigidbody")]
    private Rigidbody enemyRigidbody;

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        gunnerSystem = GetComponent<GunnerBehaviour>();

        playerCore = FindObjectOfType<PlayerController>().planeCore;
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Shoot()
    {
        if (gunnerSystem.shootTrigger && enemyRaycast.GetComponent<RaycastSystem>().isColliding)
        {
            gunnerSystem.Shoot();
        }
    }

    private void Movement()
    {
        Vector3 targetDir = playerCore.position - planeCore.position;

        float xyAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.forward, planeCore.up);
        float yzAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.right, planeCore.forward);

        if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
        {
            enemyRigidbody.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));
        }
        else if (yzAngle >= 1f)
        {
            enemyRigidbody.AddRelativeTorque(Vector3.right * -torque);
        }

        enemyRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult);
    }

    private float Vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }
}

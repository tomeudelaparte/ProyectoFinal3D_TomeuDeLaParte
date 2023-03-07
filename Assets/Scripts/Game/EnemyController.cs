using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Physics Core")]
    public Transform planeCore;

    [Header("Raycast")]
    public GameObject enemyRaycast;

    [Header("Physics Values")]
    public float torque = 10f;
    public float thrust = 100f;
    public float forceMult = 100f;

    [Header("Rigidbody")]
    private Rigidbody _enemyRigidbody;

    [Header("Gunner")]
    private Gunner _gunner;

    [Header("Player Position")]
    private Transform playerCore;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
        _gunner = GetComponent<Gunner>();

        playerCore = FindObjectOfType<PlayerController>().planeCore;
    }

    private void Update()
    {
        // SHOOT
        Shoot();
    }

    private void FixedUpdate()
    {
        // MOVEMENT
        Movement();
    }

    // SHOOT
    private void Shoot()
    {
        // If canShoot and player is on sight
        if (_gunner.shootTrigger && enemyRaycast.GetComponent<RaycastCustom>().isColliding)
        {
            // Shoot
            _gunner.Shoot();
        }
    }

    // MOVEMENT
    private void Movement()
    {
        // If player is alive
        if (playerCore)
        {
            // Get projected vector angles between enemy and player
            float xyAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.forward, planeCore.up);
            float yzAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.right, planeCore.forward);

            // If xy angle (absolute value) is more than or equals 1 and yz angle (absolute value) is more than or equals 1
            if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
            {
                // Add relative torque, forward according to angle
                _enemyRigidbody.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));
            }
            // If yz angle (absolute value) is more than or equals 1
            else if (yzAngle >= 1f)
            {
                // Add relative torque, right force
                _enemyRigidbody.AddRelativeTorque(Vector3.right * -torque);
            }

            // Add relative force, forward force
            _enemyRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult);
        }
    }

    // Get projected vector angle
    private float Vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        // Projects a vector onto a plane defined by a normal orthogonal to the plane.
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);

        // Calculates the signed angle between vectors from a to in relation to axis
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }
}

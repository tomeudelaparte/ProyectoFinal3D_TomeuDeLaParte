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
        Shoot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Shoot()
    {
        if (_gunner.shootTrigger && enemyRaycast.GetComponent<RaycastCustom>().isColliding)
        {
            _gunner.Shoot();
        }
    }

    private void Movement()
    {
        if (playerCore)
        {
            float xyAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.forward, planeCore.up);
            float yzAngle = Vector3AngleOnPlane(playerCore.position, planeCore.position, planeCore.right, planeCore.forward);

            if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
            {
                _enemyRigidbody.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));
            }
            else if (yzAngle >= 1f)
            {
                _enemyRigidbody.AddRelativeTorque(Vector3.right * -torque);
            }

            _enemyRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult);
        }
    }

    private float Vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public GameObject raycastObject;
    public Transform core;

    // Cooldown del disparo
    private bool shootTrigger = true;
    private float shootCooldown = 0.05f;
    private bool sequence = true;

    public float torque = 10f;
    public float thrust = 100f;
    private float forceMult = 100f;
    private Rigidbody rb;
    public GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Fly();

        if (shootTrigger && raycastObject.GetComponent<RaycastSystem>().isColliding)
        {
            // Dispara
            WeaponShoot();
        }
    }

    private void Fly()
    {
        Vector3 targetDir = player.transform.position - core.position;

        float xyAngle = vector3AngleOnPlane(player.transform.position, core.position, core.forward, core.up);
        float yzAngle = vector3AngleOnPlane(player.transform.position, core.position, core.right, core.forward);

        if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
        {
            rb.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));
        }
        else if (yzAngle >= 1f)
        {
            rb.AddRelativeTorque(Vector3.right * -torque);
        }

        rb.AddRelativeForce(Vector3.forward * thrust * forceMult);

    }

    private float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }

    private void WeaponShoot()
    {
        if (sequence)
        {
            Instantiate(blastPrefab, weaponPosition[0].transform.position, weaponPosition[0].transform.rotation);
            sequence = false;

        }
        else
        {
            Instantiate(blastPrefab, weaponPosition[1].transform.position, weaponPosition[1].transform.rotation);
            sequence = true;
        }

        StartCoroutine(WeaponCooldown());
    }

    private IEnumerator WeaponCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }
}

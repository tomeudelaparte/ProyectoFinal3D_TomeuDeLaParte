using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask layerMask;

    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public Zeppelin healthTurret;

    [Header("Shoot")]
    private bool shootTrigger = true;
    private float shootCooldown = 2f;

    private bool sequence = true;

    [Header("Detection")]
    private int distanceDetection = 10000;

    [Header("Rotation")]
    private float rotationClampX = 0;

    private int rotationMin = 0;
    private int rotationMax = 65;

    private int rotationMinAlt = 360;
    private int rotationMaxAlt = 305;

    public bool altRotation = false;

    [Header("Player")]
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (!healthTurret.isDestroyed)
        {
            LookAtPlayer();
            ShootAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            transform.LookAt(player.transform.position);

            if (!altRotation)
            {
                rotationClampX = (transform.localEulerAngles.x <= rotationMin) ? rotationMin : transform.localEulerAngles.x;
                rotationClampX = (transform.localEulerAngles.x >= rotationMax) ? rotationMax : transform.localEulerAngles.x;
            }

            if (altRotation)
            {
                rotationClampX = (transform.localEulerAngles.x >= rotationMinAlt) ? rotationMinAlt : transform.localEulerAngles.x;
                rotationClampX = (transform.localEulerAngles.x <= rotationMaxAlt) ? rotationMaxAlt : transform.localEulerAngles.x;
            }

            transform.localEulerAngles = new Vector3(rotationClampX, transform.localEulerAngles.y, 0);
        }
    }

    private void ShootAtPlayer()
    {
        if (IsPlayerOnSight())
        {
            if (shootTrigger)
            {
                if (sequence)
                {
                    ShootCanon(weaponPosition[0]);

                    sequence = false;
                }
                else
                {
                    ShootCanon(weaponPosition[1]);

                    sequence = true;
                }

                StartCoroutine(Cooldown());
            }
        }
    }

    private void ShootCanon(GameObject weaponPos)
    {
        Instantiate(blastPrefab, weaponPos.transform.position, weaponPos.transform.rotation);

        weaponPos.GetComponentInParent<Animator>().Play("CanonShoot");
    }

    private IEnumerator Cooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    private bool IsPlayerOnSight()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, distanceDetection, layerMask);

        Debug.DrawRay(transform.position, transform.forward * distanceDetection, Color.cyan);

        return hitData.collider != null;
    }
}

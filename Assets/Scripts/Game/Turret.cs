using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask layerMask;

    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public Zeppelin turret;

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
        // Gets player
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // If turret not destroyed
        if (!turret.isDestroyed)
        {
            // Look at and shoot Player
            LookAtPlayer();
            ShootAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        // If Player not null
        if (player != null)
        {
            // Look at to player
            transform.LookAt(player.transform.position);

            // Turret 01
            if (!altRotation)
            {
                // Clamp rotation X (Min, Max)
                rotationClampX = (transform.localEulerAngles.x <= rotationMin) ? rotationMin : transform.localEulerAngles.x;
                rotationClampX = (transform.localEulerAngles.x >= rotationMax) ? rotationMax : transform.localEulerAngles.x;
            }

            // Turret 02
            if (altRotation)
            {
                // Clamp rotation X (Min, Max)
                rotationClampX = (transform.localEulerAngles.x >= rotationMinAlt) ? rotationMinAlt : transform.localEulerAngles.x;
                rotationClampX = (transform.localEulerAngles.x <= rotationMaxAlt) ? rotationMaxAlt : transform.localEulerAngles.x;
            }

            // Change turret rotation
            transform.localEulerAngles = new Vector3(rotationClampX, transform.localEulerAngles.y, 0);
        }
    }

    private void ShootAtPlayer()
    {
        // If player on sight
        if (IsPlayerOnSight())
        {
            // If can shoot
            if (shootTrigger)
            {
                // Bool sequence
                if (sequence)
                {
                    // Shoot from position 0
                    ShootCanon(weaponPosition[0]);

                    sequence = false;
                }
                else
                {
                    // Shoot from position 1
                    ShootCanon(weaponPosition[1]);

                    sequence = true;
                }

                // Cooldown
                StartCoroutine(ShootCooldown());
            }
        }
    }

    private void ShootCanon(GameObject weaponPos)
    {
        // Instance blast from position
        Instantiate(blastPrefab, weaponPos.transform.position, weaponPos.transform.rotation);

        // Plays animation position
        weaponPos.GetComponentInParent<Animator>().Play("CanonShoot");
    }

    // Cooldown
    private IEnumerator ShootCooldown()
    {
        // False to True
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    private bool IsPlayerOnSight()
    {
        // Raycast forward
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, distanceDetection, layerMask);

        Debug.DrawRay(transform.position, transform.forward * distanceDetection, Color.cyan);

        return hitData.collider != null;
    }
}

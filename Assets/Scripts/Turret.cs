using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask layerMask;

    public Zeppelin healthTurret;

    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public bool negativeRotation = false;

    private bool shootTrigger = true;
    private float shootCooldown = 2f;

    private bool sequence = true;

    private float rotationClampX = 0;

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
        transform.LookAt(player.transform.position);

        if (!negativeRotation)
        {
            rotationClampX = (transform.localEulerAngles.x <= 0) ? 0 : transform.localEulerAngles.x;
            rotationClampX = (transform.localEulerAngles.x >= 65) ? 65 : transform.localEulerAngles.x;
        }

        if (negativeRotation)
        {
            rotationClampX = (transform.localEulerAngles.x >= 360) ? 360 : transform.localEulerAngles.x;
            rotationClampX = (transform.localEulerAngles.x <= 305) ? 305 : transform.localEulerAngles.x;
        }

        transform.localEulerAngles = new Vector3(rotationClampX, transform.localEulerAngles.y, 0);
    }

    private void ShootAtPlayer()
    {
        if (IsPlayerOnSight())
        {
            if (shootTrigger)
            {
                if (sequence)
                {
                    Instantiate(blastPrefab, weaponPosition[0].transform.position, weaponPosition[0].transform.rotation);

                    weaponPosition[0].GetComponentInParent<Animator>().Play("CanonShoot");

                    sequence = false;
                }
                else
                {
                    Instantiate(blastPrefab, weaponPosition[1].transform.position, weaponPosition[1].transform.rotation);

                    weaponPosition[1].GetComponentInParent<Animator>().Play("CanonShoot");

                    sequence = true;
                }

                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    private bool IsPlayerOnSight()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitData, 10000f, layerMask);

        Debug.DrawRay(transform.position, transform.forward * 10000, Color.cyan);

        return hitData.collider != null;
    }
}

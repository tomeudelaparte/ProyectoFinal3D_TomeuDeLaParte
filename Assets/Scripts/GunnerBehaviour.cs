using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBehaviour : MonoBehaviour
{
    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public bool shootTrigger = true;
    private float shootCooldown = 0.05f;

    private int currentAmmo = 100;
    private int wastedAmmo = 0;

    private bool sequence = true;

    public void Shoot()
    {
        if (shootTrigger)
        {
            if (sequence)
            {
                Instantiate(blastPrefab, weaponPosition[0].transform.position, weaponPosition[0].transform.rotation);
                sequence = false;

                WastingAmmo();
            }
            else
            {
                Instantiate(blastPrefab, weaponPosition[1].transform.position, weaponPosition[1].transform.rotation);
                sequence = true;

                WastingAmmo();
            }

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    public int GetCurrentAmmo()
    {
        return wastedAmmo;
    }

    private void WastingAmmo()
    {
        wastedAmmo++;

        if (wastedAmmo >= currentAmmo)
        {
            wastedAmmo = 0;
        }
    }
}

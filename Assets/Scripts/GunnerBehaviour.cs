using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBehaviour : MonoBehaviour
{
    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    public bool shootTrigger = true;
    private float shootCooldown = 0.05f;

    private int maxAmmo = 100;
    private int currentAmmo = 100;
    private float reloadingTime = 5;
    private bool isReloading = false;

    private bool sequence = true;

    public void Shoot()
    {
        if (shootTrigger && !isReloading)
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

            WastingAmmo();

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    private IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;

        currentAmmo = maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    private void WastingAmmo()
    {
        currentAmmo--;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reloading());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public bool isPlayer = false;

    public GameObject[] weaponPosition;

    public bool shootTrigger = true;
    private float shootCooldown = 0.05f;

    private int maxAmmo = 200;
    private int currentAmmo = 200;
    private float reloadingTime = 5;
    public bool isReloading = false;

    private bool sequence = true;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Interface")]
    private PlayerInterface playerInterface;

    [Header("Pool")]
    private BlastObjectPool blastObjectPool;


    private void Start()
    {
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();

        blastObjectPool = GetComponent<BlastObjectPool>();
    }

    public void Shoot()
    {
        if (shootTrigger && !isReloading)
        {
            if (sequence)
            {
                GameObject bullet = blastObjectPool.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = weaponPosition[0].transform.position;
                    bullet.transform.rotation = weaponPosition[0].transform.rotation;
                    bullet.SetActive(true);
                }

                sequence = false;
            }
            else
            {
                GameObject bullet = blastObjectPool.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = weaponPosition[1].transform.position;
                    bullet.transform.rotation = weaponPosition[1].transform.rotation;
                    bullet.SetActive(true);
                }
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

        if (!isReloading)
        {
            if (isPlayer)
            {
                soundEffects.StartReloading();

                playerInterface.StartReloading();
            }

            isReloading = true;
            yield return new WaitForSeconds(reloadingTime);
            isReloading = false;

            if (isPlayer)
            {
                soundEffects.StopReloading();

                playerInterface.StopReloading();
            }

            currentAmmo = maxAmmo;
        }
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
            Reload();
        }
    }

    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            StartCoroutine(Reloading());
        }
    }
}

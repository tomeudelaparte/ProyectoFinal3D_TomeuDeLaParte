using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public bool isPlayer = false;

    public GameObject[] weaponPosition;

    [Header("Shoot")]
    public bool shootTrigger = true;
    private float shootCooldown = 0.05f;
    private bool sequence = true;

    [Header("Ammo")]
    private int maxAmmo = 200;
    private int currentAmmo = 200;

    [Header("Reload")]
    private float reloadingTime = 5;
    public bool isReloading = false;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Interface")]
    private PlayerInterface playerInterface;

    [Header("Pool")]
    private BlastObjectPool _blastObjectPool;

    private void Start()
    {
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();

        _blastObjectPool = GetComponent<BlastObjectPool>();
    }

    public void Shoot()
    {
        if (shootTrigger && !isReloading)
        {
            if (sequence)
            {
                ShootBlast(weaponPosition[0]);

                sequence = false;
            }
            else
            {
                ShootBlast(weaponPosition[1]);

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

    private void ShootBlast(GameObject weaponPos)
    {
        GameObject blast = _blastObjectPool.GetPooledObject();

        if (blast != null)
        {
            blast.transform.position = weaponPos.transform.position;
            blast.transform.rotation = weaponPos.transform.rotation;
            blast.SetActive(true);
        }
    }

    private IEnumerator Reloading()
    {
        if (!isReloading)
        {
            isReloading = true;

            if (isPlayer)
            {
                soundEffects.StartReloading();
                playerInterface.StartReloading();
            }

            yield return new WaitForSeconds(reloadingTime);

            isReloading = false;

            currentAmmo = maxAmmo;

            if (isPlayer)
            {
                soundEffects.StopReloading();
                playerInterface.StopReloading();
            }
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

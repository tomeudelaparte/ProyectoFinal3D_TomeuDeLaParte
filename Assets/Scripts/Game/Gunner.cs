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

    // SHOOT
    public void Shoot()
    {
        // If can shoot and not reloading
        if (shootTrigger && !isReloading)
        {
            // SEQUENCE TRUE, FALSE
            if (sequence)
            {
                // Shoot from position 0
                ShootBlast(weaponPosition[0]);

                // Change to false
                sequence = false;
            }
            else
            {
                // Shoot from position 1
                ShootBlast(weaponPosition[1]);

                // Change to true
                sequence = true;
            }

            // Waste ammo
            WastingAmmo();

            // Cooldown
            StartCoroutine(Cooldown());
        }
    }

    // COOLDOWN
    private IEnumerator Cooldown()
    {
        // False to true
        shootTrigger = false;

        yield return new WaitForSeconds(shootCooldown);

        shootTrigger = true;
    }

    // SHOOT BLAST FROM POOL
    private void ShootBlast(GameObject weaponPos)
    {
        // Save GameObject in pool
        GameObject blast = _blastObjectPool.GetPooledObject();

        // If not null
        if (blast != null)
        {
            // Move to position, rotation
            blast.transform.position = weaponPos.transform.position;
            blast.transform.rotation = weaponPos.transform.rotation;

            // Active GameObject
            blast.SetActive(true);
        }
    }

    // RELOADING
    private IEnumerator Reloading()
    {
        // If not reloading
        if (!isReloading)
        {
            // Reload true
            isReloading = true;

            // If gunner is player
            if (isPlayer)
            {
                // Play sound
                soundEffects.StartReloading();

                // Play animation
                playerInterface.StartReloading();
            }

            yield return new WaitForSeconds(reloadingTime);

            // Reload false
            isReloading = false;

            // Reload ammo
            currentAmmo = maxAmmo;

            // If gunner is player
            if (isPlayer)
            {
                // Stop sound
                soundEffects.StopReloading();

                // Play animation
                playerInterface.StopReloading();
            }
        }
    }

    // RETURN CURRENT AMMO
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    // WASTE AMMO
    private void WastingAmmo()
    {
        // Current ammo -1
        currentAmmo--;

        // If current ammo is less than or equals 0
        if (currentAmmo <= 0)
        {
            // RELOAD
            Reload();
        }
    }

    // RELOAD
    public void Reload()
    {
        // If current ammo is less than max ammo
        if (currentAmmo < maxAmmo)
        {
            // Start reloading
            StartCoroutine(Reloading());
        }
    }
}

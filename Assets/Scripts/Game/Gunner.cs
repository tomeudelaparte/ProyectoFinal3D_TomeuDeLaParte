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

        // Gets pool
        _blastObjectPool = GetComponent<BlastObjectPool>();
    }

    public void Shoot()
    {
        // If can shoot and not reloading
        if (shootTrigger && !isReloading)
        {
            // Sequence true false
            if (sequence)
            {
                // Shoots from position 0
                ShootBlast(weaponPosition[0]);

                // Change to false
                sequence = false;
            }
            else
            {
                // Shoots from position 1
                ShootBlast(weaponPosition[1]);

                // Changes to true
                sequence = true;
            }

            // Wastes ammo
            WastingAmmo();

            // Cooldown
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        // False to true
        shootTrigger = false;

        yield return new WaitForSeconds(shootCooldown);

        shootTrigger = true;
    }

    private void ShootBlast(GameObject weaponPos)
    {
        // Saves blast in pool
        GameObject blast = _blastObjectPool.GetPooledObject();

        // If blast not null
        if (blast != null)
        {
            // Move to weaponPos position, rotation
            blast.transform.position = weaponPos.transform.position;
            blast.transform.rotation = weaponPos.transform.rotation;

            // Active blast
            blast.SetActive(true);
        }
    }

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

    // Return current ammo
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    private void WastingAmmo()
    {
        // Current ammo -1
        currentAmmo--;

        // If current ammo is less than or equals 0
        if (currentAmmo <= 0)
        {
            // Reload
            Reload();
        }
    }

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

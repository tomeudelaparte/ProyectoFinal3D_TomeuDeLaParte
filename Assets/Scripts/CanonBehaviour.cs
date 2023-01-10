using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBehaviour : MonoBehaviour
{
    private PlayerController player;
    public GameObject ballPrefab;

    private bool shootTrigger = true;
    private float shootCooldown = 2f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        transform.LookAt(player.transform.position);

        if (shootTrigger)
        {
            // Dispara
            WeaponShoot();
        }
    }
    private void WeaponShoot()
    {
        Instantiate(ballPrefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
        StartCoroutine(WeaponCooldown());
    }

    private IEnumerator WeaponCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

}

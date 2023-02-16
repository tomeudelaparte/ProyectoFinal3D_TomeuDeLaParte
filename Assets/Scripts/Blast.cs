using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public bool isPlayer = false;

    public GameObject blastPlaneExplosion;
    public GameObject blastGroundExplosion;

    private VisualEffects visualEffects;

    private int damage = 0;
    private float speed = 800f;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    private void Start()
    {
        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();

    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(blastGroundExplosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (isPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                damage = 5;

                soundEffects.PlaytHit();

                DamagePlane(other);
            }

            if (other.gameObject.CompareTag("Boss"))
            {
                damage = 1;

                soundEffects.PlaytHit();

                DamageZeppelin(other);
            }
        }

        if (!isPlayer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                damage = 5;

                soundEffects.PlayImpact();

                visualEffects.GetComponent<Animator>().Play("Hit");

                DamagePlane(other);
            }

            if (other.gameObject.CompareTag("Boss"))
            {
                DamageZeppelin(other);
            }
        }
    }

    private void DamagePlane(Collider other)
    {
        other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

        Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void DamageZeppelin(Collider other)
    {
        if (isPlayer)
        {
            other.gameObject.GetComponent<ZeppelinObjective>().DamageCharacter(damage);
        }

        Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

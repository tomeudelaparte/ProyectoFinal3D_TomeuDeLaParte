using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public bool isPlayer = false;

    public GameObject blastPlaneExplosion;
    public GameObject blastGroundExplosion;
    public GameObject hitmarkerAudio;

    private VisualEffects visualEffects;

    private int damage = 0;
    private float speed = 800f;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Feedback")]
    private PlayerInterface playerInterface;

    private void Start()
    {
        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();
        playerInterface = FindObjectOfType<PlayerInterface>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (isPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Instantiate(hitmarkerAudio, transform.position, transform.rotation);

                playerInterface.GetComponent<Animator>().Play("Hitmarker", -1, 0.0f);

                damage = 5;

                DamagePlane(other);
            }

            if (other.gameObject.CompareTag("Zeppelin"))
            {
                if (!other.gameObject.GetComponent<Zeppelin>().isDestroyed)
                {
                    Instantiate(hitmarkerAudio, transform.position, transform.rotation);

                    playerInterface.GetComponent<Animator>().Play("Hitmarker", -1, 0.0f);

                    damage = 1;
                }

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

            if (other.gameObject.CompareTag("Zeppelin"))
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
            other.gameObject.GetComponent<Zeppelin>().DamageCharacter(damage);
        }

        Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

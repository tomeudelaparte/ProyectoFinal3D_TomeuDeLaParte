using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public bool isPlayer = false;

    [Header("Prefabs")]
    public GameObject blastPlaneExplosion;
    public GameObject blastGroundExplosion;
    public GameObject hitmarkerAudio;

    [Header("Values")]
    private float lifeTime = 5f;
    private int damage = 0;
    private float speed = 800f;

    [Header("DestroyActive")]
    private IEnumerator destroyAfterTime;

    [Header("Visuals")]
    private VisualEffects visualEffects;

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

    private void OnEnable()
    {
        destroyAfterTime = DestroyAfterTime();

        StartCoroutine(destroyAfterTime);
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

            DesactivateBlast();
        }

        if (isPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                damage = 5;

                DamagePlane(other);

                Hitmarker();
            }

            if (other.gameObject.CompareTag("Zeppelin"))
            {
                if (!other.gameObject.GetComponent<Zeppelin>().isDestroyed)
                {
                    damage = 1;

                    DamageZeppelin(other);

                    Hitmarker();
                }
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

        DesactivateBlast();
    }

    private void DamageZeppelin(Collider other)
    {
        if (isPlayer)
        {
            other.gameObject.GetComponent<Zeppelin>().DamageCharacter(damage);
        }

        Instantiate(blastGroundExplosion, transform.position, transform.rotation);

        DesactivateBlast();
    }

    private void Hitmarker()
    {
        Instantiate(hitmarkerAudio, transform.position, transform.rotation);

        playerInterface.GetComponent<Animator>().Play("Hitmarker", -1, 0.0f);
    }

    private void DesactivateBlast()
    {
        StopCoroutine(destroyAfterTime);
        gameObject.SetActive(false);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        DesactivateBlast();
    }
}

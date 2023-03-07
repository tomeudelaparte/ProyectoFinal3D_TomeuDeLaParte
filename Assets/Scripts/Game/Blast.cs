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

    private int damageToPlayer = 5;
    private int damageToEnemy = 5;
    private int damageToZeppelin = 1;

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

    // ON ENABLE
    private void OnEnable()
    {
        // Save coroutine
        destroyAfterTime = DesactivateAfterTime();

        // Start coroutine
        StartCoroutine(destroyAfterTime);
    }

    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // ON TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        // If collider is GROUND
        if (other.gameObject.CompareTag("Ground"))
        {
            // Instantiate explosion
            Instantiate(blastGroundExplosion, transform.position, transform.rotation);

            // Desactivate this blast
            DesactivateBlast();
        }

        // If this Blast is Player
        if (isPlayer)
        {
            // If collider is ENEMY
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Change damage value
                damage = damageToEnemy;

                // Damage enemy
                DamagePlane(other);

                // Hitmarker
                Hitmarker();
            }

            // If collider is ZEPPELIN
            if (other.gameObject.CompareTag("Zeppelin"))
            {
                // If not destroyed
                if (!other.gameObject.GetComponent<Zeppelin>().isDestroyed)
                {
                    // Change damage value
                    damage = damageToZeppelin;

                    // Damage zeppelin
                    DamageZeppelin(other);

                    // Hitmarker
                    Hitmarker();
                }
            }
        }

        // If this Blast is NOT Player
        if (!isPlayer)
        {
            // If collider is Player
            if (other.gameObject.CompareTag("Player"))
            {
                // Change damage value
                damage = damageToPlayer;

                // Damage player
                DamagePlane(other);

                // Play sound Impact
                soundEffects.PlayImpact();

                // Play animation Impact
                visualEffects.GetComponent<Animator>().Play("Impact");
            }

            // If collider is Zeppelin
            if (other.gameObject.CompareTag("Zeppelin"))
            {
                // Damage Zeppelin
                DamageZeppelin(other);
            }
        }
    }

    // DAMAGE PLANE
    private void DamagePlane(Collider other)
    {
        // Damage
        other.gameObject.GetComponent<HealthManager>().DamageThis(damage);

        // Instantiate explosion
        Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

        // Desactivate this blast
        DesactivateBlast();
    }

    // DAMAGE ZEPPELIN
    private void DamageZeppelin(Collider other)
    {
        // If this Blast is Player
        if (isPlayer)
        {
            // Damage
            other.gameObject.GetComponent<Zeppelin>().DamageThis(damage);
        }

        // Instantiate explosion
        Instantiate(blastGroundExplosion, transform.position, transform.rotation);

        // Desactivate this blast
        DesactivateBlast();
    }

    // HITMARKER
    private void Hitmarker()
    {
        // Instantiate audio
        Instantiate(hitmarkerAudio, transform.position, transform.rotation);

        // Play animation
        playerInterface.GetComponent<Animator>().Play("Hitmarker", -1, 0.0f);
    }

    // DESACTIVATE
    private void DesactivateBlast()
    {
        // Stop coroutine
        StopCoroutine(destroyAfterTime);

        // Desactivate GameObject
        gameObject.SetActive(false);
    }

    // DESACTIVATE AFTER TIME
    private IEnumerator DesactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        // Desactivate this blast
        DesactivateBlast();
    }
}

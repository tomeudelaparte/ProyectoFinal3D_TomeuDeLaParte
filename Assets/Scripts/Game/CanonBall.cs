using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public GameObject canonballExplosion;

    [Header("Values")]
    private int damage = 10;
    private float time = 0;
    private float maxTime = 3f;
    private float maxDistance = 1200f;

    [Header("Rigidbody")]
    private Rigidbody _rigidbody;

    [Header("Visuals")]
    private VisualEffects visualEffects;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    [Header("Player")]
    private PlayerController player;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerController>();
        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();

        // Adds forward force
        AddForwardForce();

        // Sets explosion timer
        Timer();
    }

    private void AddForwardForce()
    {
        // Adds relative forward force
        _rigidbody.AddRelativeForce(Vector3.forward * 10000 * 10, ForceMode.Impulse);
    }

    private void Timer()
    {
        // Gets distance from player
        float distanceFromPlayer = Vector3.Distance(player.planeCore.position, transform.position);

        // Gets a lower value as time between distance and max distance
        time = distanceFromPlayer / maxDistance;

        // If time is more than max time
        if (time >= maxTime)
        {
            // Gets max time
            time = maxTime;
        }

        // Adds explosion timer
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        // After time
        yield return new WaitForSeconds(time);

        // Instance explosion
        Instantiate(canonballExplosion, transform.position, transform.rotation);

        // Disables gravity and velocity
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;

        // Disables render and collider
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponents<Collider>()[0].enabled = false;

        // Damage enabled
        StartCoroutine(EnableDamage());

        // Destroys after time
        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        // If collider is Player
        if (other.gameObject.CompareTag("Player"))
        {
            // Plays animation
            visualEffects.GetComponent<Animator>().Play("Impact");

            // Plays sound
            soundEffects.PlayImpact();

            // Adds damage
            other.GetComponent<HealthManager>().AddDamage(damage);
        }
    }

    private IEnumerator EnableDamage()
    {
        // Enables collider
        gameObject.GetComponents<Collider>()[1].enabled = true;

        yield return new WaitForSeconds(0.1f);

        // Disables collider after time
        gameObject.GetComponents<Collider>()[1].enabled = false;
    }
}

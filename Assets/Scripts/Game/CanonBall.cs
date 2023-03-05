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

        _rigidbody.AddRelativeForce(Vector3.forward * 10000 * 10, ForceMode.Impulse);

        Timer();
    }

    private void Timer()
    {
        float distanceFromPlayer = Vector3.Distance(player.planeCore.position, transform.position);
        time = distanceFromPlayer / maxDistance;

        if (time > maxTime)
        {
            time = maxTime;
        }

        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(time);

        Instantiate(canonballExplosion, transform.position, transform.rotation);

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponents<Collider>()[0].enabled = false;

        StartCoroutine(Damage());

        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            visualEffects.GetComponent<Animator>().Play("Hit");

            soundEffects.PlayImpact();

            other.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

    private IEnumerator Damage()
    {
        gameObject.GetComponents<Collider>()[1].enabled = true;

        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponents<Collider>()[1].enabled = false;
    }
}

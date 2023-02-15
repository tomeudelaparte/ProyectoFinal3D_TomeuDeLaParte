using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody rb;

    private VisualEffects visualEffects;

    private int damage = 10;
    private float timer = 5f;

    public GameObject canonballExplosion;

    [Header("Sounds")]
    private SoundEffects soundEffects;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerController>();
        visualEffects = FindObjectOfType<VisualEffects>();
        soundEffects = FindObjectOfType<SoundEffects>();

        rb.AddRelativeForce(Vector3.forward * 10000 * 10, ForceMode.Impulse);

        float distance = Vector3.Distance(player.planeCore.position, transform.position);
        timer = distance / 1200;

        if (timer > 5)
        {
            timer = 5;
        }

        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(timer);

        Instantiate(canonballExplosion, transform.position, transform.rotation);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

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

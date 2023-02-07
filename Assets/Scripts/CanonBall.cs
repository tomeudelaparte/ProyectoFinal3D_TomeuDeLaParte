using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody rb;

    private float timer = 5f;

    public GameObject canonballExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerController>();

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
        gameObject.GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 3);
    }
}

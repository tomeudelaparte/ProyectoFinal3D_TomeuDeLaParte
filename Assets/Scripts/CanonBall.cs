using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;

    private float timer = 2f;

    public GameObject canonballExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<Player>();

        rb.AddRelativeForce(Vector3.forward * 30000, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.up * 2000, ForceMode.Impulse);


        float distance = Vector3.Distance(player.transform.position, transform.position);
        timer = distance / 190;

        if(timer > 3)
        {
            timer = 3;
        }

        StartCoroutine(ExplosionTimer());

    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(timer);

        Instantiate(canonballExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

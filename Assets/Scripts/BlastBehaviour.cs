using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBehaviour : MonoBehaviour
{
    public int blastID = 0;

    private int damage = 5;


    public GameObject blastExplosion;

    private float speed = 400f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(blastExplosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (blastID == 0)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

                Instantiate(blastExplosion, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }

        if (blastID == 1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

                Instantiate(blastExplosion, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }

    }
}

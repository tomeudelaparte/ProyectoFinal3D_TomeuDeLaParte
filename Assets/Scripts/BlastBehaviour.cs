using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBehaviour : MonoBehaviour
{
    public int damage = 1;

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


        if (transform.parent.tag == "Player")
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

                Instantiate(blastExplosion, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }

        if (transform.parent.tag == "Enemy")
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

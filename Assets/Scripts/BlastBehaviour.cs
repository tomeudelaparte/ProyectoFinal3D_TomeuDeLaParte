using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBehaviour : MonoBehaviour
{
    public int blastID = 0;

    public GameObject blastPlaneExplosion;
    public GameObject blastGroundExplosion;

    private int damage = 0;
    private float speed = 800f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(blastGroundExplosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (blastID == 0)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                damage = 5;

                DamagePlane(other);
            }

            if (other.gameObject.CompareTag("Boss"))
            {
                damage = 1;

                DamageZeppelin(other);
            }
        }

        if (blastID == 1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                damage = 5;

                DamagePlane(other);
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
        other.gameObject.GetComponent<ZeppelinStuff>().DamageCharacter(damage);

        Instantiate(blastPlaneExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

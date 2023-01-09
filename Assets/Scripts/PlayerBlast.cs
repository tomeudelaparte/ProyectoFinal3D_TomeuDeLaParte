using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour
{
    public GameObject blastHitPrefab;

    private float speed = 400f;

    void Update()
    {
        // Mueve el GameObject hacia delante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(blastHitPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}

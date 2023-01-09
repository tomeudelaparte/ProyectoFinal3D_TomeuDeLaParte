using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con Enemy
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si colisiona con Enemy
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}

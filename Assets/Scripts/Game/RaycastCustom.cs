using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCustom : MonoBehaviour
{
    public bool isColliding;

    // Checks if player is on sight
    private void OnTriggerEnter(Collider other)
    {
        // If collider is PLAYER
        if (other.gameObject.CompareTag("Player"))
        {
            // True
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If collider is PLAYER
        if (other.gameObject.CompareTag("Player"))
        {
            // False
            isColliding = false;
        }
    }
}

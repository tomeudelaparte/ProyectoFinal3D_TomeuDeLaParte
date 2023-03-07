using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Animator transition;

    private void OnTriggerExit(Collider other)
    {
        // If the player exits from the collider
        if (other.gameObject.CompareTag("Player"))
        {
            //// Not working (Fade in)
            transition.Play("Opening");

            // Respawn player to position
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
        }
    }
}

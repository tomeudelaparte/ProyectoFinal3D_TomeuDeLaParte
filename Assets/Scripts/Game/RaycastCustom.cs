using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCustom : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}

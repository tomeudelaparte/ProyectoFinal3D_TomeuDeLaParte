using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRotation : MonoBehaviour
{
    private float speed = 3000f;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, speed * Time.deltaTime);
    }
}

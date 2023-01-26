using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionMode : MonoBehaviour
{
   [SerializeField] private float timeValue = 1;

    void Update()
    {
        Time.timeScale = timeValue;
    }
}

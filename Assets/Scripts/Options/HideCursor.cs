using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    void Start()
    {
        // Hides and locks mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstSelectedButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        // Selects button at Start
        button = GetComponent<Button>();
        button.Select();
    }
}

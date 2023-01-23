using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyIndicator : MonoBehaviour
{
    private GameObject mainCamera;

    private GameObject enemy;

    void Start()
    {
        mainCamera = GameObject.Find("Virtual Camera");
        enemy = GameObject.Find("Enemy");
    }

    void Update()
    {
        Vector3 offset = new Vector3(0, 0, 0);

        transform.position = enemy.transform.position + offset;

        transform.LookAt(mainCamera.transform.position);

        transform.rotation = mainCamera.transform.rotation;


        FontSize();

    }


    private void FontSize()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        TextMeshProUGUI text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        GameObject point = transform.GetChild(1).gameObject;

        float value = 0.01f * distance;

        if (value >= 20)
        {
            text.fontSize = 20;
            point.transform.localScale = new Vector3(15, 15, 0);
        }
        else if (value <= 0)
        {
            text.fontSize = 0;
            point.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            text.fontSize = value;
            point.transform.localScale = new Vector3(value, value, 0);
        }
    }
}


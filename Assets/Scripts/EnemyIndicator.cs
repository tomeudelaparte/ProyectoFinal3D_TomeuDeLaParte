using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject enemy;

    private GameObject mainCamera;
    private TextMeshProUGUI textIndicator;
    private GameObject pointIndicator;

    private float distanceFromCamera, valueFromDistance;

    void Start()
    {
        mainCamera = GameObject.Find("==== VIRTUAL CAMERA ====");
    }

    void Update()
    {
        LookAtCamera();
        ResizeToDistance();
    }

    private void LookAtCamera()
    {
        transform.position = enemy.transform.position;

        transform.LookAt(mainCamera.transform.position);

        transform.rotation = mainCamera.transform.rotation;
    }

    private void ResizeToDistance()
    {
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);

        textIndicator = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        pointIndicator = transform.GetChild(1).gameObject;

        valueFromDistance = 0.01f * distanceFromCamera;

        if (valueFromDistance >= 20)
        {
            textIndicator.fontSize = 20;
            pointIndicator.transform.localScale = new Vector3(15, 15, 0);
        }
        else if (valueFromDistance <= 0)
        {
            textIndicator.fontSize = 0;
            pointIndicator.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            textIndicator.fontSize = valueFromDistance;
            pointIndicator.transform.localScale = new Vector3(valueFromDistance, valueFromDistance, 0);
        }
    }
}


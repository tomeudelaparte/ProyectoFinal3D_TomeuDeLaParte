using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject enemy;

    private GameObject mainCamera;
    private GameObject pointIndicator;

    private float distanceFromCamera, valueFromDistance;

    void Start()
    {
        mainCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
    }

    void Update()
    {
        LookAtCamera();
        ResizeToDistance();
    }

    private void LookAtCamera()
    {
        if (enemy != null)
        {
            transform.position = enemy.transform.position;

            transform.LookAt(mainCamera.transform.position);

            transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void ResizeToDistance()
    {
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);

        pointIndicator = transform.GetChild(0).gameObject;

        valueFromDistance = 0.1f * distanceFromCamera;

        if (valueFromDistance >= 60)
        {
            pointIndicator.transform.localScale = new Vector3(60, 60, 0);
        }
        else if (valueFromDistance <= 20)
        {
            pointIndicator.transform.localScale = new Vector3(20, 20, 0);
        }
        else
        {
            pointIndicator.transform.localScale = new Vector3(valueFromDistance, valueFromDistance, 0);
        }

        ChangeTransparency(valueFromDistance / 200);
    }

    private void ChangeTransparency(float value)
    {
        pointIndicator.GetComponent<Image>().color = new Vector4(
       pointIndicator.GetComponent<Image>().color.r,
       pointIndicator.GetComponent<Image>().color.g,
       pointIndicator.GetComponent<Image>().color.b, value);
    }
}


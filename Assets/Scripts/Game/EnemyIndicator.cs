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

    private float distanceFromCamera;
    private float distance;

    private float maxDistance = 60f;
    private float minDistance = 20f;

    private int maxScale = 60;
    private int minScale = 20;

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

        distance = 0.1f * distanceFromCamera;

        if (distance >= maxDistance)
        {
            pointIndicator.transform.localScale = new Vector3(maxScale, maxScale, 0);
        }
        else if (distance <= minDistance)
        {
            pointIndicator.transform.localScale = new Vector3(minScale, minScale, 0);
        }
        else
        {
            pointIndicator.transform.localScale = new Vector3(distance, distance, 0);
        }

        ChangeTransparency(distance / 200);
    }

    private void ChangeTransparency(float value)
    {
        pointIndicator.GetComponent<Image>().color = new Vector4(
        pointIndicator.GetComponent<Image>().color.r,
        pointIndicator.GetComponent<Image>().color.g,
        pointIndicator.GetComponent<Image>().color.b, value);
    }
}


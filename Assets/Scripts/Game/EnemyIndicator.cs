using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

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
        // Get camera
        mainCamera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
    }

    void Update()
    {
        // Look at camera
        LookAtCamera();

        // Change on distance
        ChangeOnDistance();
    }

    private void LookAtCamera()
    {
        // If enemy not null
        if (enemy != null)
        {
            // Move to enemy position
            transform.position = enemy.transform.position;

            // Look at camera
            transform.LookAt(mainCamera.transform.position);

            // Change to camera rotation
            transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void ChangeOnDistance()
    {
        // Get distance from camera
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Get ui enemy indicator
        pointIndicator = transform.GetChild(0).gameObject;

        // Reduce distance to some normal value
        distance = 0.1f * distanceFromCamera;

        // If distance is more than max distance
        if (distance >= maxDistance)
        {
            // Maximum image scale
            pointIndicator.transform.localScale = new Vector3(maxScale, maxScale, 0);
        }
        // If distance is less than max distance
        else if (distance <= minDistance)
        {
            // Minimum image scale
            pointIndicator.transform.localScale = new Vector3(minScale, minScale, 0);
        }
        else
        {
            // Image scale from distance
            pointIndicator.transform.localScale = new Vector3(distance, distance, 0);
        }

        // Change image transparency
        ChangeTransparency(distance / 200);
    }

    // Change image transparency
    private void ChangeTransparency(float value)
    {
        pointIndicator.GetComponent<Image>().color = new Vector4(
        pointIndicator.GetComponent<Image>().color.r,
        pointIndicator.GetComponent<Image>().color.g,
        pointIndicator.GetComponent<Image>().color.b, value);
    }
}


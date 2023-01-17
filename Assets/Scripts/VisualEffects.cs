using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VisualEffects : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    void Start()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView += value;
    }

    public void DecreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView -= value;
    }
}

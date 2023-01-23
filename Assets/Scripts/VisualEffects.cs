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

        cinemachineVirtualCamera.m_Lens.FieldOfView = 75;
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

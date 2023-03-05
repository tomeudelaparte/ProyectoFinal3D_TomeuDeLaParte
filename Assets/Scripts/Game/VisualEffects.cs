using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualEffects : MonoBehaviour
{
    public float vignetteAnimationValue;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private Volume postProcessing;
    private Vignette vignetteTMP;
    private ColorAdjustments colorAdjustmentsTMP;

    void Start()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        postProcessing = FindObjectOfType<Volume>();

        cinemachineVirtualCamera.m_Lens.FieldOfView = 75;
    }

    private void Update()
    {
        VignetteAnimation(vignetteAnimationValue);
    }

    public void IncreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView += value;
    }

    public void DecreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView -= value;
    }

    public void UpdateSaturation(float value)
    {
        if (postProcessing.profile.TryGet<ColorAdjustments>(out colorAdjustmentsTMP))
        {
            colorAdjustmentsTMP.saturation.value = value;
        }
    }

    public void VignetteAnimation(float value)
    {
        if (postProcessing.profile.TryGet<Vignette>(out vignetteTMP))
        {
            vignetteTMP.intensity.value = value;
        }
    }
}

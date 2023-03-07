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
        // Gets cinemachine virtual camera
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        // Gets volume
        postProcessing = FindObjectOfType<Volume>();

        // Default Field of View
        cinemachineVirtualCamera.m_Lens.FieldOfView = 75;
    }

    private void Update()
    {
        // Vignette animation (updates value animated)
        VignetteAnimation(vignetteAnimationValue);
    }

    // Increases camera field of view
    public void IncreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView += value;
    }

    // Decreases camera field of view
    public void DecreaseFOV(float value)
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView -= value;
    }

    // Updates post-processing saturation
    public void UpdateSaturation(float value)
    {
        // Gets component from volume profile
        if (postProcessing.profile.TryGet<ColorAdjustments>(out colorAdjustmentsTMP))
        {
            // Changes value
            colorAdjustmentsTMP.saturation.value = value;
        }
    }

    // Vignette animation
    public void VignetteAnimation(float value)
    {
        // Gets component from volume profile
        if (postProcessing.profile.TryGet<Vignette>(out vignetteTMP))
        {
            // Changes value
            vignetteTMP.intensity.value = value;
        }
    }
}

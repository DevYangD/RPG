using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class VolumeCtrl : MonoBehaviour
{
    private Volume globalVolume;
    private Vignette vignette;

    void Start()
    {
        // Global Volume 오브젝트 가져오기
        globalVolume = FindObjectOfType<Volume>();

        if (globalVolume != null && globalVolume.profile.TryGet(out vignette))
        {
            // Vignette가 있는 경우 초기 Intensity 설정
            vignette.intensity.value = 0.0f; // 원하는 초기값으로 설정
        }
    }

    // Vignette Intensity 값을 설정하는 메서드
    public void SetVignetteIntensity()
    {
        if (vignette != null)
        {
            vignette.intensity.value = 0.3f;
            
        }
        else
        {
            Debug.LogWarning("Vignette effect not found in the Global Volume.");
        }
    }

    public void GetVignetteIntensity()
    {
        if (vignette != null)
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("Vignette effect not found in the Global Volume.");
        }
    }
}


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
        // Global Volume ������Ʈ ��������
        globalVolume = FindObjectOfType<Volume>();

        if (globalVolume != null && globalVolume.profile.TryGet(out vignette))
        {
            // Vignette�� �ִ� ��� �ʱ� Intensity ����
            vignette.intensity.value = 0.0f; // ���ϴ� �ʱⰪ���� ����
        }
    }

    // Vignette Intensity ���� �����ϴ� �޼���
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


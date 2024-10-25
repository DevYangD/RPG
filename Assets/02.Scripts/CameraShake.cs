using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    public float shakeIntensity = 1f;
    public float shakeTime = 0.2f;

    public CinemachineBasicMultiChannelPerlin _cbmcp;
    public Cinemachine3rdPersonFollow _tran;

    public NoiseSettings customNoiseProfile;
    public NoiseSettings noiseSettings;
    void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        _cbmcp = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _tran = _cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (_cbmcp == null)
        {
            Debug.LogError("missing");
        }
        if (_tran == null) Debug.Log("trans missing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoShake()
    {
        Debug.Log("doShake");
        _cbmcp.m_NoiseProfile = customNoiseProfile;
        _cbmcp.m_AmplitudeGain = 2f;
    }

    public void FinishShake()
    {
        Debug.Log("dofinish");
        _cbmcp.m_NoiseProfile = customNoiseProfile;
        _cbmcp.m_AmplitudeGain = 7f;
    }

    public void StopShake()
    {
        Debug.Log("stopShake");
        _cbmcp.m_NoiseProfile = noiseSettings; 
        _cbmcp.m_AmplitudeGain = 0.5f;
    }
    public void MeleeDoShake()
    {

        _cbmcp.m_NoiseProfile = customNoiseProfile;
        _cbmcp.m_AmplitudeGain = 20f;
    }

    public void HitShake()
    {
        _cbmcp.m_NoiseProfile = customNoiseProfile;
        _cbmcp.m_AmplitudeGain = 10f;
    }

    public void StartDamping()
    {
        _tran.Damping.y = 10f; 
    }

    public void EndDamping()
    {
        _tran.Damping.y = 0.25f;
    }
}

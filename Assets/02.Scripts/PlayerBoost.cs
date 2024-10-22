using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
public class PlayerBoost : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public float normalFOV = 40f; // 기본 FOV
    public float boostedFOV = 60f; // 속도 증가 시 FOV
    public float fovChangeSpeed = 5f; // FOV 변화 속도

    private ThirdPersonController playerController;

    private void Start()
    {
        playerController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        // Shift 키를 눌렀는지 확인
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Shift 키가 눌리면 FOV 증가, 아니면 기본 FOV
        float targetFOV = isSprinting ? boostedFOV : normalFOV;

        // FOV를 부드럽게 변화시킴
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, fovChangeSpeed * Time.deltaTime);
    }
}

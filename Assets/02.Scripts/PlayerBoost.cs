using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
public class PlayerBoost : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public float normalFOV = 40f; // �⺻ FOV
    public float boostedFOV = 60f; // �ӵ� ���� �� FOV
    public float fovChangeSpeed = 5f; // FOV ��ȭ �ӵ�

    private ThirdPersonController playerController;

    private void Start()
    {
        playerController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        // Shift Ű�� �������� Ȯ��
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Shift Ű�� ������ FOV ����, �ƴϸ� �⺻ FOV
        float targetFOV = isSprinting ? boostedFOV : normalFOV;

        // FOV�� �ε巴�� ��ȭ��Ŵ
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, fovChangeSpeed * Time.deltaTime);
    }
}

using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; // DOTween 네임스페이스 추가

public class UIMgr : MonoBehaviour
{
    public GameObject gameobject; // 팝업 게임 오브젝트
    public GameObject bossHpSlider; // 보스 HP 슬라이더
    public ThirdPersonController playerController; // 플레이어 컨트롤러
    public GameObject text; // 기타 UI 텍스트
    public GameObject text2;
    private bool isPopup = false; // 팝업 상태

    void Start()
    {
        playerController = FindObjectOfType<ThirdPersonController>();
        playerController.isPopup = false;
        gameobject.SetActive(false);
        bossHpSlider.SetActive(false);
    }

    void Update()
    {
        // R 키 입력 시 팝업 토글
        if (Input.GetKeyDown(KeyCode.R))
        {
            TogglePopup();
            HideText();
        }
    }

    public void TogglePopup()
    {
        isPopup = !isPopup; // 팝업 상태 토글
        playerController.isPopup = isPopup; // 플레이어의 팝업 상태도 동기화

        if (isPopup)
        {
            ShowPopup();
        }
        else
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        gameobject.SetActive(true);
        gameobject.transform.localScale = Vector3.one * 0.1f; // 초기 스케일 설정

        // DOTween을 사용하여 애니메이션 효과 추가
        gameobject.transform.DOScale(1.1f, 0.2f).OnComplete(() =>
        {
            gameobject.transform.DOScale(1f, 0.1f);
        });
    }

    private void HidePopup()
    {
        // DOTween을 사용하여 애니메이션 효과 추가
        gameobject.transform.DOScale(1.1f, 0.1f).OnComplete(() =>
        {
            gameobject.transform.DOScale(0.1f, 0.2f).OnComplete(() =>
            {
                gameobject.SetActive(false); // 애니메이션이 끝난 후 비활성화
            });
        });
    }

    public void BossTag()
    {
        bossHpSlider.SetActive(true);
        text2.SetActive(true);
    }

    public void ShowText()
    {
        text.SetActive(true); // 텍스트를 보여주기 위한 메서드
    }

    public void HideText()
    {
        text.SetActive(false); // 텍스트를 숨기기 위한 메서드

    }
}

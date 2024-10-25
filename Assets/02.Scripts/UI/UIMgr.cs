using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class UIMgr : MonoBehaviour
{
    public GameObject gameobject; // �˾� ���� ������Ʈ
    public GameObject bossHpSlider; // ���� HP �����̴�
    public ThirdPersonController playerController; // �÷��̾� ��Ʈ�ѷ�
    public GameObject text; // ��Ÿ UI �ؽ�Ʈ
    public GameObject text2;
    private bool isPopup = false; // �˾� ����

    void Start()
    {
        playerController = FindObjectOfType<ThirdPersonController>();
        playerController.isPopup = false;
        gameobject.SetActive(false);
        bossHpSlider.SetActive(false);
    }

    void Update()
    {
        // R Ű �Է� �� �˾� ���
        if (Input.GetKeyDown(KeyCode.R))
        {
            TogglePopup();
            HideText();
        }
    }

    public void TogglePopup()
    {
        isPopup = !isPopup; // �˾� ���� ���
        playerController.isPopup = isPopup; // �÷��̾��� �˾� ���µ� ����ȭ

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
        gameobject.transform.localScale = Vector3.one * 0.1f; // �ʱ� ������ ����

        // DOTween�� ����Ͽ� �ִϸ��̼� ȿ�� �߰�
        gameobject.transform.DOScale(1.1f, 0.2f).OnComplete(() =>
        {
            gameobject.transform.DOScale(1f, 0.1f);
        });
    }

    private void HidePopup()
    {
        // DOTween�� ����Ͽ� �ִϸ��̼� ȿ�� �߰�
        gameobject.transform.DOScale(1.1f, 0.1f).OnComplete(() =>
        {
            gameobject.transform.DOScale(0.1f, 0.2f).OnComplete(() =>
            {
                gameobject.SetActive(false); // �ִϸ��̼��� ���� �� ��Ȱ��ȭ
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
        text.SetActive(true); // �ؽ�Ʈ�� �����ֱ� ���� �޼���
    }

    public void HideText()
    {
        text.SetActive(false); // �ؽ�Ʈ�� ����� ���� �޼���

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using StarterAssets;
public class Panel_Ctrl : MonoBehaviour
{
    public ThirdPersonController playerctrl;
    // Start is called before the first frame update
    void Start()
    {
        playerctrl = FindObjectOfType<ThirdPersonController>();
        DOTween.Init();

        transform.localScale = Vector3.one * 0.1f;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        playerctrl.isPopup = true;
        gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play();
    }

    public void Hide()
    {
        playerctrl.isPopup = false;
        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.2f, 0.2f));

        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_UI : MonoBehaviour
{
    public Panel_Ctrl popUpWindow;


    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.1f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popUpWindow.Show();
        });
    }
}

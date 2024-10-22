using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_UI : MonoBehaviour
{
    public Slider hpSlider;
    public Slider spSlider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
    }

    public void CheckHp()
    {
        if(hpSlider != null)
        {
            hpSlider.value = PlayerCtrl.Instance.CurHpValue / PlayerCtrl.Instance.HpValue;
        }
    }

    public void CheckSp()
    {
        if (spSlider != null)
        {
            spSlider.value = PlayerCtrl.Instance.SpdValue / 5;
        }
    }
}

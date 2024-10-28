using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI atkTxt;
    public TextMeshProUGUI atk_valueTxt;
    // Start is called before the first frame update

    public void SetUpToolTip(string name, string des, int atk)
    {
        nameTxt.text = name;
        descriptionTxt.text = des;

        if(atk == 0)
        {
            atkTxt.gameObject.SetActive(false);
            atk_valueTxt.gameObject.SetActive(false);
        }
        else
        {
            atkTxt.gameObject.SetActive(true);
            atk_valueTxt.gameObject.SetActive(true);
            atk_valueTxt.text = atk.ToString();
        }
        atk_valueTxt.text = atk.ToString();
    }
}

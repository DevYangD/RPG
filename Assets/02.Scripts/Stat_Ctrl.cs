using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stat_Ctrl : MonoBehaviour
{

    public GameObject alertpanel;
    public TMP_Text alertMsg;
    public TMP_Text alertMsg2;

    public TMP_Text HpValue;
    public TMP_Text AtkValue;
    public TMP_Text DefValue;
    public TMP_Text SpdValue;
    public TMP_Text SkillPoint;

    public TMP_Text HpValue2;
    public TMP_Text AtkValue2;
    public TMP_Text DefValue2;
    public TMP_Text SpdValue2;

    private float preHpValue;
    private int preAtkValue;
    private int preDefValue;
    private float preSpdValue;
    private int preSkillPoint;

    public void SkillPointBtn(string param)
    {
        if(preSkillPoint > 0)
        {
            switch(param)
            {
                case "Hp":
                    {
                        HpValue.text = (preHpValue + 50).ToString();
                        preHpValue = preHpValue +50;
                    }
                    break;
                case "Atk":
                    {
                        AtkValue.text = (preAtkValue + 3).ToString();
                        preAtkValue = preAtkValue + 3;
                    }
                    break;
                case "Def":
                    {
                        DefValue.text = (preDefValue + 2).ToString();
                        preDefValue = preDefValue + 2;
                    }
                    break;
                case "Spd":
                    {
                        SpdValue.text = (preSpdValue + 0.1f).ToString();
                        preSpdValue = preSpdValue + 0.2f;
                    }
                    break;
            }
            SkillPoint.text = (preSkillPoint - 1).ToString();
            preSkillPoint--;
        }
        else
        {
            StartCoroutine(AlerCoroutine2());
            return;
            
        }

    }

    IEnumerator AlerCoroutine()
    {
        alertMsg.text = "STAT COMPLETE";
        alertMsg.color = Color.white;
        alertpanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        alertpanel.SetActive(false);
    }

    IEnumerator AlerCoroutine2()
    {
        alertMsg.text = "SP IS NOT ENOUGH";
        alertMsg.color = Color.gray;
        alertpanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        alertpanel.SetActive(false);
    }



    public void SkillPointSave()
    {
        PlayerCtrl.Instance.HpValue = preHpValue;
        PlayerCtrl.Instance.AtkValue = preAtkValue;
        PlayerCtrl.Instance.DefValue = preDefValue;
        PlayerCtrl.Instance.SpdValue = preSpdValue;
        PlayerCtrl.Instance.SkillPoint = preSkillPoint;

        AbilityTextSetting();
        //PopupPanelsControl();

        StartCoroutine(AlerCoroutine());
        StopCoroutine(AlerCoroutine());
    }

    public void SkillPointCancel()
    {
        preHpValue = PlayerCtrl.Instance.HpValue;
        preAtkValue = PlayerCtrl.Instance.AtkValue;
        preDefValue = PlayerCtrl.Instance.DefValue;
        preSpdValue = PlayerCtrl.Instance.SpdValue;
        preSkillPoint = PlayerCtrl.Instance.SkillPoint;

        AbilityTextSetting();
    }

    public void AbilityTextSetting()
    {
        HpValue.text = PlayerCtrl.Instance.HpValue.ToString();
        AtkValue.text = PlayerCtrl.Instance.AtkValue.ToString();
        DefValue.text = PlayerCtrl.Instance.DefValue.ToString();
        SpdValue.text = PlayerCtrl.Instance.SpdValue.ToString();
        SkillPoint.text = PlayerCtrl.Instance.SkillPoint.ToString();

    }

    public void Awake()
    {
        AbilityTextSetting();

        preHpValue = PlayerCtrl.Instance.HpValue;
        preAtkValue = PlayerCtrl.Instance.AtkValue;
        preDefValue = PlayerCtrl.Instance.DefValue;
        preSpdValue = PlayerCtrl.Instance.SpdValue;
        preSkillPoint = PlayerCtrl.Instance.SkillPoint;
    }
    // Start is called before the first frame update
    void Start()
    {
        AbilityTextSetting();

        preHpValue = PlayerCtrl.Instance.HpValue;
        preAtkValue = PlayerCtrl.Instance.AtkValue;
        preDefValue = PlayerCtrl.Instance.DefValue;
        preSpdValue = PlayerCtrl.Instance.SpdValue;
        preSkillPoint = PlayerCtrl.Instance.SkillPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (preSkillPoint <= 0)
        {
            HpValue2.color = Color.gray;
            AtkValue2.color = Color.gray;
            DefValue2.color = Color.gray;
            SpdValue2.color = Color.gray;
        }
        if (preSkillPoint > 0)
        {
            HpValue2.color = Color.yellow;
            AtkValue2.color = Color.yellow;
            DefValue2.color = Color.yellow;
            SpdValue2.color = Color.yellow;
        }
    }
}

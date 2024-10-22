using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIMgr : MonoBehaviour
{
    public bool isPopup = false;
    public GameObject gameobject;
    public GameObject bossHpSilder;
    public ThirdPersonController playctrl;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {

        gameobject.SetActive(false);
        playctrl = FindObjectOfType<ThirdPersonController>();
        playctrl.isPopup = false;
        bossHpSilder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            POPUP();
            Show();
        }
        
    }

    public void POPUP()
    {
        isPopup =!isPopup;
        playctrl.isPopup =!playctrl.isPopup;
        gameobject.SetActive(isPopup);
    }



    public void OnClickStat()
    {
        isPopup = !isPopup;
        playctrl.isPopup = !playctrl.isPopup;
    }

    public void OnClickInventory()
    {
        isPopup = !isPopup;
        playctrl.isPopup = !playctrl.isPopup;
    }

    public void OnOption()
    {
        isPopup = !isPopup;
        playctrl.isPopup = !playctrl.isPopup;
    }

    public void BossTag()
    {
        bossHpSilder.SetActive(true);
    }

    public void Show()
    {
        text.SetActive(false);
    }
}

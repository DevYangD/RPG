using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionCtrl : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할 시 True 

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private TMP_Text actionText;  // 행동을 보여 줄 텍스트
    public GameObject alertpanel;

    private Item_Pickup currentItem;  // 현재 상호작용 중인 아이템
    [SerializeField]
    private Inventory theinventory;

    [SerializeField]
    private string GetHp;


    Sword sword;

    private void Start()
    {
        sword = FindObjectOfType<Sword>();
    }
    void Update()
    {
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated && currentItem != null)
        {
            if (currentItem.item.itemName != "검")
            {
                SoundMgr.instance.PlaySE(GetHp);
                Debug.Log(currentItem.item.itemName + " 획득 했습니다.");  // 인벤토리 넣기
                theinventory.AcquireItem(currentItem.transform.GetComponent<Item_Pickup>().item);
                Destroy(currentItem.gameObject);
                ItemInfoDisappear();
            }
            else if(currentItem.item.itemName == "검")
            {
                Debug.Log(currentItem.item.itemName + " 획득 했습니다.");  // 인벤토리 넣기
                theinventory.AcquireItem(currentItem.transform.GetComponent<Item_Pickup>().item);
                Destroy(currentItem.gameObject);
                ItemInfoDisappear();
                sword.showSword();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && ((1 << other.gameObject.layer) & layerMask) != 0)
        {
            currentItem = other.GetComponent<Item_Pickup>();
            ItemInfoAppear();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && currentItem != null && other.gameObject == currentItem.gameObject)
        {
            ItemInfoDisappear();
            currentItem = null;
        }

    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        alertpanel.SetActive(true);
        actionText.text = currentItem.item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        alertpanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionCtrl : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� ������ �� True 

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    [SerializeField]
    private TMP_Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ
    public GameObject alertpanel;

    private Item_Pickup currentItem;  // ���� ��ȣ�ۿ� ���� ������
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
            if (currentItem.item.itemName != "��")
            {
                SoundMgr.instance.PlaySE(GetHp);
                Debug.Log(currentItem.item.itemName + " ȹ�� �߽��ϴ�.");  // �κ��丮 �ֱ�
                theinventory.AcquireItem(currentItem.transform.GetComponent<Item_Pickup>().item);
                Destroy(currentItem.gameObject);
                ItemInfoDisappear();
            }
            else if(currentItem.item.itemName == "��")
            {
                Debug.Log(currentItem.item.itemName + " ȹ�� �߽��ϴ�.");  // �κ��丮 �ֱ�
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
        actionText.text = currentItem.item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        alertpanel.SetActive(false);
    }
}

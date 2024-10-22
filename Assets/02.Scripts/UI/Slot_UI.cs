using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot_UI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    public Item item;
    public int itemCount;
    public Image itemImg;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImg;
    [SerializeField]
    private string hpSound;
    [SerializeField]
    private string EndDragSound;
    [SerializeField]
    private string beginDragSound;


    void Start()
    {
        
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImg.color;
        color.a = _alpha;
        itemImg.color = color;
    }
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.sprite = item.itemimg;

        if(item.itemtype != Item.ItemType.Equipment)
        {
            go_CountImg.SetActive(true);
            text_Count.text = "x" + itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImg.SetActive(false);
        }


        SetColor(1);

    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = "x"+itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImg.SetActive(false);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                if(item.itemtype == Item.ItemType.Equipment)
                {
                    //장착
                }
                else
                {
                    PlayerCtrl.Instance.HpPotion(200);
                    SoundMgr.instance.PlaySE(hpSound);
                    Debug.Log(item.itemName + " 을 사용했습니다");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // ondrop 다른슬롯 위에서만 호출
        if(DragSlot_UI.instance.dragSlot != null)   // null 참조 오류방지
        {
            ChangeSlot();
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 어디서든 호출
        SoundMgr.instance.PlaySE(EndDragSound);
        DragSlot_UI.instance.SetColor(0);
        DragSlot_UI.instance.dragSlot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SoundMgr.instance.PlaySE(beginDragSound);
            DragSlot_UI.instance.dragSlot = this;
            DragSlot_UI.instance.DragSetImage(itemImg);

            DragSlot_UI.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot_UI.instance.transform.position = eventData.position;
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot_UI.instance.dragSlot.item, DragSlot_UI.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot_UI.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot_UI.instance.dragSlot.ClearSlot();
    }
}

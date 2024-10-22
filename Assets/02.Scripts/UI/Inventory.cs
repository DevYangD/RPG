using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot_UI[] slots;
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(Item _item, int _count =1)
    {
        if (Item.ItemType.Equipment != _item.itemtype)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                    slots[i].AddItem(_item, _count);
                    return;
            }
        }
    }
}

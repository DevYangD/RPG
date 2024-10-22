using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragSlot_UI : MonoBehaviour
{
    static public DragSlot_UI instance;

    public Slot_UI dragSlot;

    [SerializeField]
    private Image imgItem;

    private void Start()
    {
        instance = this;
    }
    public void DragSetImage(Image _itemImg)
    {
        imgItem.sprite = _itemImg.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imgItem.color;
        color.a = _alpha;
        imgItem.color = color;
    }
}

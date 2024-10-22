using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{

    public enum ItemType
    {
        Equipment,
        Used,
        ETC
    }
    public string itemName;
    public ItemType itemtype;
    public Sprite itemimg;
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

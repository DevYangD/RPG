using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Damage_Text : MonoBehaviour
{
    TextMeshPro text;


    private void Start()
    {

        text = GetComponent<TextMeshPro>();



        Invoke("DamageDestroy", 1f);
    }

    // Update is called once per frame

    public void DamageDestroy()
    {
        Destroy(gameObject);
    }
}

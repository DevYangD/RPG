using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Damage_Ctrl : MonoBehaviour
{

    private static Damage_Ctrl instance = null;

    public static Damage_Ctrl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Damage_Ctrl>();

                if (instance == null)
                {
                    Debug.Log("null");
                }
            }
            return instance;
        }
    }

    public Canvas canvas;
    public GameObject dmgText;

    public void CreateDamageText(Vector3 pos, int damage)
    {
        GameObject damageText = Instantiate(dmgText, pos, Quaternion.identity, canvas.transform);
        int ab = Random.Range(0, 12);
        damageText.GetComponent<TextMeshProUGUI>().text = (damage +ab).ToString();
    }
}

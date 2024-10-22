using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sword : MonoBehaviour
{
    public GameObject sword;

    [SerializeField]
    private string GetSword;

    void Start()
    {
        sword = GameObject.Find("SM_Katana_UnSheathed");
        if (sword != null)
        {
            // Sword �� �ڽĵ��� ��� MeshRenderer ��������
            MeshRenderer[] meshRenderers = sword.GetComponentsInChildren<MeshRenderer>();

            // ��� MeshRenderer ����
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSword()
    {
        // Sword �� �ڽĵ��� ��� MeshRenderer ��������
        MeshRenderer[] meshRenderers = sword.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        SoundMgr.instance.PlaySE(GetSword);
    }    
}

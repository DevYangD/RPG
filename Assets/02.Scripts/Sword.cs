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
            // Sword 및 자식들의 모든 MeshRenderer 가져오기
            MeshRenderer[] meshRenderers = sword.GetComponentsInChildren<MeshRenderer>();

            // 모든 MeshRenderer 끄기
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
        // Sword 및 자식들의 모든 MeshRenderer 가져오기
        MeshRenderer[] meshRenderers = sword.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        SoundMgr.instance.PlaySE(GetSword);
    }    
}

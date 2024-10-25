using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageMgr : MonoBehaviour
{

    [SerializeField]
    GameObject m_goPrefab = null;

    List<Transform> m_objectList = new List<Transform>();
    List<GameObject> m_dmgTextList = new List<GameObject>();

    Camera m_cam = null;


    private void Start()
    {
        m_cam = Camera.main;
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Boss");
        for (int i = 0; i < t_objects.Length; i++)
        {
            m_objectList.Add(t_objects[i].transform);
            GameObject t_dmgText = Instantiate(m_goPrefab, t_objects[i].transform.position, Quaternion.identity, transform);
            m_dmgTextList.Add(t_dmgText);
        }
    }
    public void CreateDamageText(Vector3 pos, int damage)
    {
        GameObject damageText = Instantiate(m_goPrefab, pos, Quaternion.identity);
        int ab = Random.Range(0, 12);
        damageText.GetComponent<Text>().text = (damage + ab).ToString();
    }
}

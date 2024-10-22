using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiexed_UI : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = camera.transform.position - transform.position;
        direction.y = 0; // Y축 회전을 방지 (지면 평면으로 고정)
        transform.rotation = Quaternion.LookRotation(direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NaviManager : MonoBehaviour
{
    private LineRenderer line;
    public List<Transform> targetPos;
    public Transform curPos;
    [SerializeField]
    private string BGM;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        // LineRenderer 기본 설정 추가
        line.startWidth = 0.2f;  // 선의 시작 두께 설정
        line.endWidth = 0.2f;    // 선의 끝 두께 설정
        line.material = new Material(Shader.Find("Sprites/Default"));  // 기본 쉐이더 설정
        
    }
    public void Start()
    {
        for (int i = 1; i <= 7; i++)
        {
            targetPos.Add(GameObject.Find($"TargetPos{i}").transform);
        }
        SetupLine(targetPos);

        
    }
    private void Update()
    {
        if (targetPos.Count > 0)
        {
            curPos = targetPos[0];
            float distance = Vector3.Distance(transform.position, curPos.position);
            Vector3 dir = curPos.position - transform.position;
            dir.Normalize();
            if (distance < 1f)
            {
                targetPos.RemoveAt(0);  // 0번 없앰
                SetupLine(targetPos);

            }
        }
    }

    public void SetupLine(List<Transform> targetPos)
    {
        line.positionCount = targetPos.Count;
        for( int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, targetPos[i].position);
        }

    }

}


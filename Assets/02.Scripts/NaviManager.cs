using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviManager : MonoBehaviour
{
    private LineRenderer line;
    public List<Transform> targetPos = new List<Transform>();
    public Transform curPos;
    [SerializeField]
    private string BGM;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();

        if (line == null)
        {
            Debug.LogError("LineRenderer component not found!");
            return;
        }

        // LineRenderer 기본 설정
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void Start()
    {
        // 타겟 포지션 찾기
        for (int i = 1; i <= 7; i++)
        {
            GameObject target = GameObject.Find($"TargetPos{i}");
            if (target != null)
            {
                targetPos.Add(target.transform);
            }
            else
            {
                Debug.LogWarning($"TargetPos{i} not found in scene!");
            }
        }

        // 라인 설정
        if (targetPos.Count > 0)
        {
            SetupLine(targetPos);
        }
        else
        {
            Debug.LogWarning("No target positions available to setup line!");
        }
    }

    private void Update()
    {
        // 타겟이 존재하고 LineRenderer가 설정된 경우에만 실행
        if (targetPos.Count > 0 && line != null)
        {
            // 현재 타겟을 가져옴
            curPos = targetPos[0];

            // 현재 타겟이 파괴되었는지 null 체크
            if (curPos == null)
            {
                targetPos.RemoveAt(0); // null인 경우 리스트에서 제거
                SetupLine(targetPos);  // 리스트 갱신
                return; // null 체크 후 다음 프레임까지 대기
            }

            // 타겟과의 거리 계산
            float distance = Vector3.Distance(transform.position, curPos.position);
            Vector3 dir = curPos.position - transform.position;
            dir.Normalize();

            if (distance < 1f)
            {
                targetPos.RemoveAt(0);  // 타겟에 도착한 경우 리스트에서 제거
                SetupLine(targetPos);   // 리스트 갱신
            }
        }
    }

    public void SetupLine(List<Transform> targetPos)
    {
        if (line == null)
        {
            Debug.LogError("LineRenderer is not initialized!");
            return;
        }

        // 라인 포지션 설정
        line.positionCount = targetPos.Count;

        for (int i = 0; i < line.positionCount; i++)
        {
            if (targetPos[i] != null)
            {
                line.SetPosition(i, targetPos[i].position);
            }
        }
    }
}

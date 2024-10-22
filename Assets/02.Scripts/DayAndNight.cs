using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecound;

    private bool isNight = false;

    [SerializeField] private float fogDensityCal;
    [SerializeField] private float nightFogDensity;
    private float dayFogDensity;
    private float curFogDensity;
    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.deltaTime);

        // ���� ���� ���� ���� ����
        if (transform.eulerAngles.x >= 180 && transform.eulerAngles.x <= 360)
            isNight = true;  // 180�� �̻��� �� �� (���� �Ʒ�)
        else
            isNight = false; // 0������ 180�� ������ �� �� (���� ��)


        if (isNight)
        {
            if (curFogDensity <= 0.1f)
            {
                curFogDensity += 0.1f * fogDensityCal * Time.deltaTime;
                RenderSettings.fogDensity = curFogDensity;
            }
        }
        else
        {
            if (curFogDensity > dayFogDensity)
            {
                curFogDensity -= 0.1f * fogDensityCal * Time.deltaTime;
                RenderSettings.fogDensity = curFogDensity;
            }

        }
    }
}

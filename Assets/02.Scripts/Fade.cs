using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{

    public Image panel;
    float time = 0;
    float f_time = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Fadeway());
    }


    IEnumerator Fadeway()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color a = panel.color;

        //Fade In
        //while(a.a <  1f)
        //{
        //    time += Time.deltaTime / f_time;
        //    a.a = Mathf.Lerp(0,1,time);
        //    panel.color = a;
        //    yield return null;

        //}
        //time = 0f;

        //yield return new WaitForSeconds(1f);

        // Fade out
        while(a.a>0f)
        {
            time += Time.deltaTime / f_time;
            a.a = Mathf.Lerp(1,0,time);
            panel.color=a;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

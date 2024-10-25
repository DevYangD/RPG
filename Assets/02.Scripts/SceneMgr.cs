using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnClickStart()
    {


        LoadingScene.LoadScene("02_RPG_World");

    }

    public void OnClickReturn()
    {

        LoadingScene.LoadScene("01_Start");
    }

    public void OnClickExit()
    {

        Application.Quit();
    }


}

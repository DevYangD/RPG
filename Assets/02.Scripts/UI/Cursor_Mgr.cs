using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Mgr : MonoBehaviour
{
    Texture2D original;
    Texture2D hand;
    void Start()
    {
        original = Resources.Load<Texture2D>("Cursor_Basic");
        hand = Resources.Load<Texture2D>("Cursor_Hand");
    }

    public void OnClickOver()
    {
        Cursor.SetCursor(hand, new Vector2(hand.width / 4, 0), CursorMode.Auto);
    }

    public void OnClickExit()
    {
        Cursor.SetCursor(original, new Vector2(original.width / 4, 0), CursorMode.Auto);
    }


}

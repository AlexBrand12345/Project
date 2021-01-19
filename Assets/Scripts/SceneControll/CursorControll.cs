using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorControll : MonoBehaviour
{
    bool menuOpened = false;
    CursorMode cursorMode = CursorMode.Auto;
    public Texture2D texture;
    Texture2D cursor;
    public static CursorControll cursorControll = new CursorControll();
    public void HideCursor()
    {
        Cursor.visible = false;
    }
    public void ChangeCursor()
    {   
        if(!Cursor.visible)
        {
            Cursor.visible = true;
        }
        menuOpened = !menuOpened;
        if (menuOpened) cursor = null;
        else cursor = texture;
        Cursor.SetCursor(cursor, Vector2.zero, cursorMode);
    }
    void Awake()
    {     
        Cursor.SetCursor(texture, Vector2.zero, cursorMode); 
    }
}

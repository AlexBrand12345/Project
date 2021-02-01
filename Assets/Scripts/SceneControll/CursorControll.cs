using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorControll : MonoBehaviour
{
    bool menuOpened;
    CursorMode cursorMode = CursorMode.ForceSoftware;
    public Texture2D texture;
    [SerializeField]
    Texture2D cursor;
    public static CursorControll cursorControll = new CursorControll();
    public void HideCursor()
    {
        Cursor.visible = false;
    }
    public void ChangeCursor()
    {
        //Texture2D cursor;
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
        menuOpened = false;
        cursor = texture;
        Cursor.SetCursor(texture, Vector2.zero, cursorMode); 
    }
}

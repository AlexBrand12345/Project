using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorControll : MonoBehaviour
{
    bool menuOpened = false;
    CursorMode cursorMode = CursorMode.ForceSoftware;
    public Texture2D texture;
    //public Texture2D baseTexture;
    [SerializeField] GameObject cursorPoint;
    [SerializeField]
    Texture2D cursor;
    public static CursorControll cursorControll = new CursorControll();
    public void HideCursor()
    {
        Cursor.visible = false;
    }
    public void ChangeCursor()
    {
        if (!Cursor.visible) Cursor.visible = true;
        menuOpened = !menuOpened;
        Debug.Log(menuOpened);
        if (menuOpened)
        {
            //Debug.Log(baseTexture);
            Cursor.SetCursor(null, Vector3.zero, cursorMode);
        }
        else
        {
            Debug.Log(texture);
            Cursor.SetCursor(texture, Vector3.zero, cursorMode);
        }

    }
    public void Update()
    {
        //if (cursorPoint != null) cursorPoint.transform.position = Input.mousePosition;
    }
    public void Awake()
    {
        menuOpened = false;
        cursor = texture;
        Cursor.SetCursor(texture, Vector2.zero, cursorMode);
        Debug.Log(texture);
        cursorControll.texture = texture;
    }
}

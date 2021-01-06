using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuBut : MonoBehaviour
{
    public GameObject newsBut;
    GameObject scroll;
    Vector3 startPos;
    bool butClicked;
    public void  OpenWindow(GameObject obj)
    {        
        scroll = obj.transform.GetChild(1).gameObject;
        if (scroll.activeSelf)
        {
            obj.transform.position = startPos;
            scroll.SetActive(false);
            butClicked = false;
        }
        else if(!butClicked)
        {
            startPos = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = newsBut.transform.position;
            //obj.transform.Translate(new Vector3(0, 0, -1));
            scroll.SetActive(true);
            butClicked = true;
        }
    }
    
}

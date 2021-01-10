using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuBut : MonoBehaviour
{
    public List<GameObject> buttons;
    public GameObject togo;
    GameObject scroll;
    GameObject clone;
    Vector3 startPos;
    bool butClicked;
    public void OpenWindow(GameObject obj)
    {
         if (!butClicked)
         {
            clone = obj.transform.GetChild(2).gameObject;
            scroll = obj.transform.GetChild(1).gameObject;
            startPos = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            obj.transform.position = togo.transform.position;
            Debug.Log(clone);
            for (int i = 0; i < buttons.Count; i++)
            {
                if (obj != buttons[i]) buttons[i].SetActive(false);
            }
            //obj.transform.Translate(new Vector3(0, 0, -1));
            scroll.SetActive(true);
            clone.SetActive(true);
            butClicked = true;
         }
         else if (scroll.activeSelf)
         {
            for (int i = 0; i<buttons.Count; i++)
            {
                buttons[i].SetActive(true);
            }
            obj.transform.position = startPos;
            scroll.SetActive(false);
            clone.SetActive(false);
            butClicked = false;
         }
        
    }
    public void ShowCredits()
    {

    }
    
}

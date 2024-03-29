﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkin : MonoBehaviour
{
    public float timeToChange;
    [SerializeField]
    RectTransform contentRect;
    public GameObject[] skins;
    //float target;
    [SerializeField]
    int i;

    Vector2 contentVector;
    // Start is called before the first frame update
    void Awake()
    {
        i = MainSave.save.curSkin;
        contentRect = GetComponent<RectTransform>();
        contentRect.anchoredPosition = new Vector2(-skins[i].transform.localPosition.x, 0);
        
    }
    private void Update()
    {        
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, -skins[i].transform.localPosition.x, timeToChange * Time.deltaTime);
            contentRect.anchoredPosition = contentVector;      
    }

    public void NextSkin()
    {
        if (skins[i + 1] != null)
        {
            i++;
            MainSave.save.curSkin = i;
        }
    }
    public void PrevSkin()
    {
        if (skins[i - 1] != null) 
        { 
            i--;
            MainSave.save.curSkin = i;
        }
    }
}

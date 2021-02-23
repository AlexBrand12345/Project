﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadSceneAnim : MonoBehaviour
{
    public GameObject[] prefabs; //prefabs with animations

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
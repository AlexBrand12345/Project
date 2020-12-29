using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LockPrefab : MonoBehaviour
{
    GameObject EscController;
    Escbuttons script;
    string name;

    // Start is called before the first frame update
    void Start()
    {
        EscController = GameObject.FindWithTag("EscController");
        script = EscController.GetComponent<Escbuttons>();
        name = gameObject.name;
    }

    void ItemClicked(object sender, EventArgs e)
    {
        MethodInfo m = script.GetType().GetMethod(name);
        m.Invoke(script, null);
    }    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    Escbuttons script;
    public bool need2Load;
    public string scene;
    string loader;
    void Start()
    {
        script = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        if (need2Load) LoadScene(scene);
    }
    void LoadScene(string sceneName)
    {

    }
    
}

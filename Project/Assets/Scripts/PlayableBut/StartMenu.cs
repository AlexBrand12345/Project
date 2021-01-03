using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    Escbuttons script;  
    void Start()
    {
        script = GetComponent<Escbuttons>();
        script.LoadScene("Main_Menu");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public string scene;
    Escbuttons script;  
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        script = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        Debug.Log(scene);
        LoadScene(scene);
    }
    public void LoadScene(string SceneName)
    {
        if (SceneName != null) script.LoadScene(SceneName, true);
        else script.LoadScene("Main_Menu", true);
    }
      
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public bool startGame;
    public bool needToLoad;
    public string scene;
    Escbuttons script;  
    void Awake()
    {      
    }
    void Start()
    {
        script = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        Debug.Log(scene);
        if (startGame)
        {
            script.LoadScene("Main_Menu StartScene");
            startGame = false;
        }
        if (needToLoad)
        {
            LoadScene(scene);
        }   
    }
    public void LoadScene(string SceneName)
    {
        Debug.Log(SceneName);
        if (SceneName != null) script.LoadScene(SceneName, true);
        else script.LoadScene("Main_Menu", true);
        needToLoad = false;
        Destroy(gameObject);
    }
      
}

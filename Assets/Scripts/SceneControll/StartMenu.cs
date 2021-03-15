using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    bool need2DestroySelf;
    public bool startGame;
    public bool needToLoad;
    public string scene;
    Escbuttons script;  
    public void Load()
    {
        need2DestroySelf = true;
        //Debug.Log("iteration");
        script = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        //Debug.Log(scene + "startmenu1");
        LoadScene(scene);
    }
    void Start()
    {
        if (!need2DestroySelf) DontDestroyOnLoad(this.gameObject);
        if (startGame)
        {
            script = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
            //Debug.Log(scene + "startmenu1");
            script.LoadScene("Main_Menu StartScene");
            startGame = false;
        }
        //if (needToLoad)
        //{
        //    LoadScene(scene);
        //}
    }
    public void LoadScene(string SceneName)
    {
        Debug.Log(SceneName + "startmenu2");
        if (SceneName != null) script.LoadScene(SceneName, true);
        else script.LoadScene("Main_Menu", true);
        needToLoad = false;
        Destroy(this.gameObject);
    }
      
}

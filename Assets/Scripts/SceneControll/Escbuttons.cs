using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Escbuttons : MonoBehaviour
{
    public Player player;
    MusicControll music;
    public GameObject loadingScene;
    public bool IsLoading = false;
    Slider loadingSlider;
    GameObject settings;
    public Hands hands;
    public GameObject panel;
    public GameObject setts;
    public GameObject PauseMenu;
    public GameObject GOMenu;
//public bool onPause = false;
    bool isSets = false;
    AsyncOperation loading;
    
    public void LoadScene(string scene)
    {
        SaveLoad.Save();
        
        IsLoading = true;
        CursorControll.cursorControll.HideCursor();
        loadingSlider = loadingScene.GetComponentInChildren<Slider>();
        loadingScene.SetActive(true);
        loading = SceneManager.LoadSceneAsync(scene);
        if(loading.progress >=0.9f) IsLoading = false;       
    }

    public void Update()
    {
        if(IsLoading)
        loadingSlider.value = 1 - loading.progress;
    }
    //public void Main_Menu()
    //{
    //    loadingScene.SetActive(true);
    //    AsyncOperation loading = SceneManager.LoadSceneAsync("Main_Menu");
    //    loadingSlider.value = 1 - loading.progress;
    //}
    private void Awake()
    {
        SaveLoad.Load();
        music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        if (!isSets)
        {
            settings = Instantiate(setts, panel.transform);
            isSets = true;
        } else
        {
            Destroy(settings);
            isSets = false;
        }
        Debug.Log(isSets);
    } 
    public void Start()
    {
        //music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
        //loadingSlider = loadingScene.GetComponentInChildren<Slider>();
    }
    //private void Update()
    //{
    //    OnEscape()
    //}
    public void OnEscape()
    {
        if (player.paused)
        {
            Destroy(settings);
            Resume();
        }
        else Stop();
           
    }
    public void Resume()
    {
        //if (!GOMenu.activeSelf) //если игра не проиграна, а на паузе
        if (!GOMenu.activeSelf)
        {
            player.paused = false;
            CursorControll.cursorControll.ChangeCursor();
            hands.canRotate = true;
            panel.SetActive(false);
            Time.timeScale = 1f;
            //if(pauseMenu != null) Destroy(pauseMenu);
            PauseMenu.SetActive(false);
            music.Pause(player.paused);
            //Settings.SetActive(false);
            //paused = false;
        }
    }
    void Stop()
    {
        player.paused = true;
        CursorControll.cursorControll.ChangeCursor();
        hands.canRotate = false;
        panel.SetActive(true);
        Time.timeScale = 0.001f;
        music.Pause(player.paused);
        //pauseMenu = Instantiate(PauseMenu, new Vector3(0, 0, 0), panel.transform.rotation, panel.transform);
        PauseMenu.SetActive(true);
        //paused = true;
    }
}

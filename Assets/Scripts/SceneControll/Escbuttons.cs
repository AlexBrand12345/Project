using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Escbuttons : MonoBehaviour
{
    StartMenu startmenu;
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
    
    public void LoadScene(string scene, bool anything)
    {
        SaveLoad.Load();
        //SaveLoad.Save();     
        IsLoading = true;
        //CursorControll.cursorControll.HideCursor();
        Debug.Log(loadingScene);
        loadingSlider = loadingScene.GetComponentInChildren<Slider>();
        Debug.Log(loadingSlider);
        //loadingScene.SetActive(true);
        loading = SceneManager.LoadSceneAsync(scene);
        if(loading.progress >=0.9f) IsLoading = false;
    }
    public void LoadScene(string allInOne) //передача сцены и её подгрузчика
    {
        SaveLoad.Save();
        //SaveLoad.Load();  
        string scene = allInOne.Split(' ')[0];
        string loader = allInOne.Split(' ')[1];
        Debug.Log(scene + "esc1");
        Debug.Log(loader + "esc2");
        //SaveLoad.Save();
        //IsLoading = true;
        //CursorControll.cursorControll.HideCursor();
        startmenu.scene = scene;
        Debug.Log(startmenu.scene + "esc3");
        startmenu.needToLoad = true;
        //loadingSlider = loadingScene.GetComponentInChildren<Slider>();
        //loadingScene.SetActive(true);
        //DontDestroyOnLoad(startmenu.gameObject);
        loading = SceneManager.LoadSceneAsync(loader); //загрузка подгрузчика
        //if (loading.progress >= 0.9f) IsLoading = false;
    }

    public void Update()
    {
        if (IsLoading) 
        {
            //Debug.Log(IsLoading);
            loadingSlider.value = 1 - loading.progress;
        }
        
    }
    private void Awake()
    {
        SaveLoad.Load();
        startmenu = GameObject.FindWithTag("StartMenu").GetComponent<StartMenu>();
        music = GetMusicControll();
        if (startmenu.needToLoad) startmenu.Load();
    }
    MusicControll GetMusicControll()
    {
        Debug.Log("GetMusicStarted");
        if (GameObject.FindWithTag("MusicControll") == null)
        {
            Debug.Log("Didnt find anything");
            return null;
        }
        else
        {
            MusicControll music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
            return music;
        }
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
    //public void Start()
    //{
        //music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
        //loadingSlider = loadingScene.GetComponentInChildren<Slider>();
    //}
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

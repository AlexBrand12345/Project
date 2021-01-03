using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Escbuttons : MonoBehaviour
{
    public GameObject loadingScene;
    Slider loadingSlider;
    GameObject settings;
    public GameObject panel;
    public GameObject setts;
    public GameObject PauseMenu;
    public GameObject GOMenu;
    bool paused = false;
    
    public void LoadScene(string scene)
    {
        loadingSlider = loadingScene.GetComponentInChildren<Slider>();
        loadingScene.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(scene);
        loadingSlider.value = 1 - loading.progress;
    }
    //public void Main_Menu()
    //{
    //    loadingScene.SetActive(true);
    //    AsyncOperation loading = SceneManager.LoadSceneAsync("Main_Menu");
    //    loadingSlider.value = 1 - loading.progress;
    //}

    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        //settings = Instantiate(setts, new Vector3(0, 0, 0), panel.transform.rotation, panel.transform);
        settings = Instantiate(setts, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0));
        //if (settings.activeSelf) settings.SetActive(false);
        //else settings.SetActive(true);
        //settings.SetActive(!settings.activeSelf);
    } 
    public void Start()
    {
        //loadingSlider = loadingScene.GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Destroy(settings);
                settings.SetActive(false);
                Resume();
            }
            else Stop();
        }
    }
    public void Resume()
    {
        if (!GOMenu.activeSelf) //если игра не проиграна, а на паузе
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            //if(pauseMenu != null) Destroy(pauseMenu);
            PauseMenu.SetActive(false);
            //Settings.SetActive(false);
            paused = false;
        }
    }
    void Stop()
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
        //pauseMenu = Instantiate(PauseMenu, new Vector3(0, 0, 0), panel.transform.rotation, panel.transform);
        PauseMenu.SetActive(true);
        paused = true;
    }
}

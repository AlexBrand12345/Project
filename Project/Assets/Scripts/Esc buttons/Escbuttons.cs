using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escbuttons : MonoBehaviour
{
    public GameObject settings;
    public GameObject panel;
    public GameObject PauseMenu;
    public GameObject GOMenu;
    bool paused = false;
    public void Restart()
    {
        SceneManager.LoadScene("Arena");
    }
    public void Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Settings()
    {
        //settings = Instantiate(setts, new Vector3(0, 0, 0), panel.transform.rotation, panel);
        if (settings.activeSelf) settings.SetActive(false);
        else settings.SetActive(true);
        //settings.SetActive(!settings.activeSelf);
    } 
    public void Start()
    {
        settings.SetActive(false);
        panel.SetActive(false);
        GOMenu.SetActive(false);
        PauseMenu.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                //Destroy(settings);
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
            paused = !paused;
        }
    }
    void Stop()
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
        //pauseMenu = Instantiate(PauseMenu, new Vector3(0, 0, 0), panel.transform.rotation, panel.transform);
        PauseMenu.SetActive(true);
        paused = !paused;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public Escbuttons esc;
    GameObject pauseMenu;
    bool paused = false;
    public GameObject panel;

    public void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Destroy(esc.settings);
                Resume();
            }
            else Stop();
        }
    }
    void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(pauseMenu);
        paused = !paused;
    }
    void Stop()
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
        //pauseMenu = Instantiate(PauseMenu, new Vector3(0, 0, 0), panel.transform.rotation, panel);
        paused = !paused;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllStats : MonoBehaviour
{
    //MusicControll music;
    public bool alreadyDead = false;
    bool waveIsOver;
    public Escbuttons esc;
    public float updLvlTime;
    public float gameOverTime;
    public float startGameOverTime;
    public GameObject gameOver;
    public GameObject updLvl;
    public GameObject panel;
    public Slider sliderH;
    public Slider sliderE;
    public Gradient gradient;
    public Image image;
    public Text textH;
    public Text textE;
    //string child;
    Player player;

    public Coroutine End;
    public Coroutine GameOverCor;
    // Start is called before the first frame update

    public void Awake()
    {
        Time.timeScale = 1f;
        alreadyDead = false;    
        //music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.paused = false;
        sliderH.maxValue = player.health;
        sliderE.maxValue = player.maxExp;
        sliderH.value = player.maxHealth;
        sliderE.value = 0;
        textH.text = ($"{player.health}/{player.maxHealth}");
        textE.text = ($"{player.exp}/{player.maxExp}");
        image.color = gradient.Evaluate(1f);
    }
    public void Update()
    {
        waveIsOver = Game.game.waveIsOver;
        textH.text = ($"{player.health}/{player.maxHealth}");
        textE.text = ($"{player.exp}/{player.maxExp}");
    }
    public void FixedUpdate()
    {
        sliderH.maxValue = player.maxHealth;
        sliderH.value = player.health;
        //if (player.health == 0 && !alreadyDead)
        //{
        //    CursorControll.cursorControll.HideCursor();
        //    End = StartCoroutine(StartGameOver());
        //    //music.GameOver();
        //    alreadyDead = true;
        //}
        image.color = gradient.Evaluate(sliderH.normalizedValue);

        sliderE.maxValue = player.maxExp;
        sliderE.value = player.exp;
        if (player.exp == player.maxExp && waveIsOver)
        {
            StartCoroutine(UpdLvl());
        }
    }
    public void GuiDie()
    {
        if (!alreadyDead)
        {
            CursorControll.cursorControll.HideCursor();
            End = StartCoroutine(StartGameOver());
            //StartGameOver();
            //music.GameOver();
            alreadyDead = true;
        }
    }
    
    public float time(string Name)
    {
        if (Name == "updLvlTime") return updLvlTime;
        else return gameOverTime;    
    }
    public IEnumerator StartGameOver()
    {
       player.paused = true;
       yield return new WaitForSeconds(startGameOverTime);
       Debug.Log("I catched you");
       StartCoroutine(GameOver());
    }
    public IEnumerator GameOver()
    {
        Debug.Log("started2");
        CursorControll.cursorControll.ChangeCursor();       
        gameOver.GetComponentInChildren<TimerSlider>().Begin();
        Debug.Log("started3");
        yield return new WaitForSeconds(gameOverTime);
        Debug.Log("finished");
        esc.LoadScene("Main_Menu");
    }
    IEnumerator UpdLvl()
    {
        player.paused = true;
        updLvl.GetComponentInChildren<TimerSlider>().Begin();
        player.SetMaxExp();
        yield return new WaitForSeconds(updLvlTime);
        updLvl.SetActive(false);
        player.paused = false;

    }
  

}

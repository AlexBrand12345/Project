﻿using System.Collections;
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
    public GameObject redText;
    public GameObject[] weapons;
    GameObject weapon;
    public GameObject reload;
    GameObject reloadObj;
    public GameObject canvas;
    public GameObject gameOver;
    public GameObject updLvlpref;
    GameObject updLvl;
    public GameObject panel;
    public Slider sliderH;
    public Slider sliderE;
    public Gradient gradient;
    public Image image;
    public Text textH;
    public Text textE;
    Text redText2;
    //string child;
    Player player;

    public Coroutine End;
    public Coroutine GameOverCor;
    // Start is called before the first frame update

    public void Awake()
    {
        Time.timeScale = 1f;
        redText2 = redText.GetComponent<Text>();
        weapon = weapons[0];
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
        if ((player.exp == player.maxExp) && waveIsOver)
        {
            player.SetMaxExp();
            StartCoroutine(UpdLvl());
        }
    }
    public void SwitchWeapon(int curIndex, int index)
    {
        weapon = weapons[curIndex];
        weapon.GetComponent<Image>().color = Color.black;
        weapon.GetComponentInChildren<Text>().color = new Color(91 / 255.0f, 218 / 255.0f, 245 / 255.0f);
        weapon = weapons[index];
        weapon.GetComponent<Image>().color = Color.green;
        weapon.GetComponentInChildren<Text>().color = new Color(255 / 255.0f, 218 / 211.0f, 69 / 255.0f);
    }
    public void Reload(float time2reload)
    {
        reloadObj = Instantiate(reload, new Vector3(weapon.transform.position.x, weapon.transform.position.y + weapon.GetComponent<RectTransform>().rect.height/20f, 0f), Quaternion.Euler(0,0,0), canvas.transform);
        reloadObj.GetComponent<Reloading>().GetTime(time2reload);
    }
    public void StopReloading()
    {
        Destroy(reloadObj);
        //Debug.Log("Destroyed");
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
        StartCoroutine(ShowRedText("Игра окончена", startGameOverTime - 0.1f));
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
        esc.LoadScene("Main_Menu LoadingScene");
    }
    IEnumerator UpdLvl()
    {
        player.paused = true;
        player.body.Sleep();
        //updLvl.SetActive(true);
        updLvl = Instantiate(updLvlpref, canvas.transform);
        updLvl.GetComponentInChildren<TimerSlider>().Begin();       
        yield return new WaitForSeconds(updLvlTime);
        //updLvl.SetActive(false);
        Destroy(updLvl);
        player.body.WakeUp();
        player.paused = false;
    }
    public IEnumerator ShowRedText(string text, float time2show)
    {
        redText2.text = text;
        redText.SetActive(true);
        yield return new WaitForSeconds(time2show);
        redText.SetActive(false);
    }

}

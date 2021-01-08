using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllStats : MonoBehaviour
{
    bool waveIsOver;
    public Escbuttons esc;
    public int updLvlTime;
    public int gameOverTime;
    public int startGameOverTime;
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
    // Start is called before the first frame update

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        if (player.health == 0) StartCoroutine(StartGameOver());
        image.color = gradient.Evaluate(sliderH.normalizedValue);

        sliderE.maxValue = player.maxExp;
        sliderE.value = player.exp;
        if (player.exp == player.maxExp && waveIsOver)
        {
            StartCoroutine(UpdLvl());
        }
    }
    
    public int time(string Name)
    {
        if (Name == "updLvlTime") return updLvlTime;
        else return gameOverTime;
        
    }
    IEnumerator StartGameOver()
    {
        yield return new WaitForSeconds(startGameOverTime);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        player.canShoot = false;
        gameOver.GetComponentInChildren<TimerSlider>().Begin();
        yield return new WaitForSeconds(gameOverTime);
        player.canShoot = true;
        esc.LoadScene("Main_Menu");
    }
    IEnumerator UpdLvl()
    {
        player.canShoot = false;
        updLvl.GetComponentInChildren<TimerSlider>().Begin();
        player.SetMaxExp();
        yield return new WaitForSeconds(updLvlTime);
        updLvl.SetActive(false);
        player.canShoot = true;

    }
  

}

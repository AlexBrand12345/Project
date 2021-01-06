using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllStats : MonoBehaviour
{
    public Escbuttons esc;
    public int updLvlTime;
    public int gameOverTime;
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

    void Start()
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
    void Update()
    {
        textH.text = ($"{player.health}/{player.maxHealth}");
        textE.text = ($"{player.exp}/{player.maxExp}");
    }
    private void FixedUpdate()
    {
        sliderH.maxValue = player.maxHealth;
        sliderH.value = player.health;
        if (player.health == 0) StartCoroutine(GameOver());
        image.color = gradient.Evaluate(sliderH.normalizedValue);

        sliderE.maxValue = player.maxExp;
        sliderE.value = player.exp;
        if (player.exp == player.maxExp)
        {
            StartCoroutine(UpdLvl());
        }
    }
    
    public int time(string Name)
    {
        if (Name == "updLvlTime") return updLvlTime;
        else return gameOverTime;
        
    }
    IEnumerator GameOver()
    {
        
        gameOver.GetComponentInChildren<TimerSlider>().Begin();
        yield return new WaitForSeconds(gameOverTime);
        esc.LoadScene("Main_Menu");
    }
    IEnumerator UpdLvl()
    {
        updLvl.GetComponentInChildren<TimerSlider>().Begin();
        player.SetMaxE();
        yield return new WaitForSeconds(updLvlTime);
        updLvl.SetActive(false);
        
    }
  

}

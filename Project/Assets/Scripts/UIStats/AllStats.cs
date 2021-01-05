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
    int damage;
    int exp;
    int curH;
    public int maxH;
    public int curE;
    public int maxE;
    Player player;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        curE = 0;
        sliderH.maxValue = player.health;
        sliderE.maxValue = player.maxExp;
        sliderH.value = player.maxHealth;
        sliderE.value = 0;
        textH.text = ($"{player.health}/{player.maxHealth}");
        textE.text = ($"{curE}/{player.maxExp}");
        image.color = gradient.Evaluate(1f);
    }
    void Update()
    {
        textH.text = ($"{player.health}/{player.maxHealth}");
        textE.text = ($"{curE}/{player.maxExp}");
    }
    private void FixedUpdate()
    {
        MainSave.game.timeValue += Time.deltaTime;

        sliderH.maxValue = player.maxHealth;
        sliderH.value = player.health;
        if (player.health == 0) StartCoroutine(GameOver());
        image.color = gradient.Evaluate(sliderH.normalizedValue);

        sliderE.maxValue = player.maxExp;
        sliderE.value = player.exp;
        if(player.exp == player.maxExp)
        {
            player.exp = 0;
            StartCoroutine(UpdLvl());
        }
    }
    public void LaunchUpg(string upgName, GameObject upgrades)
    {               
        switch (upgName.Split('(')[0])
        {
            case "PlusEXP":
                PlusEXP();
                break;
            case "PlusDMG":
                PlusDMG();
                break;
            case "PlusHP":
                PlusHP();
                break;
            default:
                Debug.Log("ничего не произошло");
                break;
        }
        Destroy(upgrades);
    }

    public void PlusEXP()
    {
        player.exp++;
    }
    public void PlusDMG()
    {
        player.baseDMG = player.baseDMG * 3 / 2;
        //damage = damage * 3 / 2;
    }
    public void PlusHP()
    {
        player.maxHealth = player.maxHealth * 3 / 2;
        player.health = player.maxHealth;
    }
    public void SetMaxE()
    {
        player.maxExp = player.maxExp * 3 / 2;
        player.exp = 0;
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
        SetMaxE();
        yield return new WaitForSeconds(updLvlTime);
        updLvl.SetActive(false);
        
    }
    // Update is called once per frame

}

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
    // Start is called before the first frame update

    void Start()
    {
        curH = maxH;
        curE = 0;
        sliderH.maxValue = maxH;
        sliderE.maxValue = maxE;
        sliderH.value = maxH;
        sliderE.value = 0;
        textH.text = ($"{curH}/{maxH}");
        textE.text = ($"{curE}/{maxE}");
        image.color = gradient.Evaluate(1f);
        updLvl.SetActive(false);
    }
    void Update()
    {
        textH.text = ($"{curH}/{maxH}");
        textE.text = ($"{curE}/{maxE}");
        //Debug
        GainEXP();
    }
    private void FixedUpdate()
    {
        MainSave.game.timeValue += Time.deltaTime;
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
        exp++;
    }
    public void PlusDMG()
    {
        damage = damage * 3 / 2;
    }
    public void PlusHP()
    {
        maxH = maxH * 3 / 2;
        curH = maxH;
        textH.text = ($"{curH}/{maxH}");
        sliderH.maxValue = maxH;
        sliderH.value = curH;
    }
    public void Heal(int heal)
    {
        curH += heal;
        if (curH >= maxH) curH = maxH;
        sliderH.value = curH;
        image.color = gradient.Evaluate(sliderH.normalizedValue);
    }
    public void Damage(int damage)
    {
        curH -= damage;
        if (curH <= 0)
        {
            MainSave.game.deaths++;
            curH = 0;
            sliderH.value = 0;
            StartCoroutine(GameOver());
        }
        else
        {
            sliderH.value = curH;
            image.color = gradient.Evaluate(sliderH.normalizedValue);
        }
    }
    //public void SetH(int damage, int heal)
    //{
    //    curH += heal * maxH / 5;
    //    curH -= damage;
    //    if (curH <= 0)
    //    {
    //        curH = 0;
    //        sliderH.value = 0;
    //        //StartCoroutine(GameOver());
    //    }
    //    else if (curH >= maxH)
    //    {
    //        curH = maxH;
    //        sliderH.value = maxH;
    //    }
    //    else
    //    {
    //        sliderH.value = curH;
    //    }
    //    image.color = gradient.Evaluate(sliderH.normalizedValue);
    //}    
    public void GainEXP()
    {
        curE += exp;
        if (curE >= maxE)
        {
            curE = maxE;
            //if (!Wave)
            //{
                  StartCoroutine(UpdLvl());              
            //}
        }
        sliderE.value = curE;
    }

    public void SetMaxE()
    {
        maxE = maxE * 3 / 2;
        curE = 0;
        sliderE.value = curE;
    }
    public int time(string Name)
    {
        if (Name == "updLvlTime") return updLvlTime;
        else return gameOverTime;
        
    }
    

    //IEnumerator GameOver()
    //{
    //    GO_clone = Instantiate(GO, new Vector3(0, 0, 0), panel.transform.rotation panel);
    //    yield return new WaitForSeconds(10);
    //    //загрузка сцены с меню
    //}
    //IEnumerator UpdLvl()
    //{
    //    UpdWindow_Clone = Instantiate(UpdWindow, new Vector3(0, 0, 0), panel.transform.rotation panel);
    //    yield return new WaitForSeconds(3);
    //    Destroy(UWindow_Clone);
    //    SetMaxE();
    //}
    IEnumerator GameOver()
    {
        //updLvl.SetActive(true);
        gameOver.GetComponentInChildren<TimerSlider>().Begin();
        yield return new WaitForSeconds(gameOverTime);
        esc.LoadScene("Main_Menu");
    }
    IEnumerator UpdLvl()
    {
        //updLvl.SetActive(true);
        updLvl.GetComponentInChildren<TimerSlider>().Begin();
        SetMaxE();
        yield return new WaitForSeconds(updLvlTime);
        updLvl.SetActive(false);
        
    }
    // Update is called once per frame

}

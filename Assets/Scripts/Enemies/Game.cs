using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public bool waveIsOver;
    public float timeBetWaves;

    public List <GameObject> spawnPoints;
    public List<GameObject> enemies;
    GameObject enemy;
    Vector3 spawnPos;

    public int protivnikov;
    public int waves;
   // public List<Sprite> sprites;
    public static Game game = new Game();
    

    public int pistolDMG;
    public int tommyDMG;
    public int autogunDMG;
    public int rifleDMG;

    public int pistolHP;
    public int tommyHP;
    public int autogunHP;
    public int rifleHP;

    public int pistolbspeed;
    public int tommybspeed;
    public int autogunbspeed;
    public int riflebspeed;

    public int pistolAmmo;
    public int tommyAmmo;
    public int autogunAmmo;
    public int rifleAmmo;

    public int extraHP = 0;
    public int extraDMG = 0;
    public int extrabspeed = 0;

    public Game()
    {
        extraHP = new int();
        extraDMG = new int();
        extrabspeed = new int();

        pistolDMG = new int();
        tommyDMG = new int();
        autogunDMG = new int();
        rifleDMG = new int();

        pistolHP = new int();
        tommyHP = new int();
        autogunHP = new int();
        rifleHP = new int();

        pistolbspeed = new int();
        tommybspeed = new int();
        autogunbspeed = new int();
        riflebspeed = new int();

        pistolAmmo = new int();
        tommyAmmo = new int();
        autogunAmmo = new int();
        rifleAmmo = new int();

}
    
    IEnumerator Wave(int bots)
    {
        waveIsOver = true;
        yield return new WaitForSeconds(timeBetWaves);
        var upgrades = new List<Action> { AddDMG, AddHP, Addbspeed };
        if (waves % 10 == 0)
        {
            upgrades[UnityEngine.Random.Range(0, upgrades.Count - 1)]();
        }
        waveIsOver = false;
        for (int i=0; i<bots; i++)
        {
            spawnPos = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)].transform.position;
            enemy = enemies[UnityEngine.Random.Range(0, enemies.Count - 1)];
            Instantiate(enemy, new Vector3(UnityEngine.Random.Range(spawnPos.x - 10f, spawnPos.x +10f), spawnPos.y, spawnPos.z), Quaternion.Euler(0, 0, 0));
            protivnikov++;
        }       
    }
public void FixedUpdate()
    {
        MainSave.save.timeValue += Time.deltaTime;
        if (protivnikov == 0) StartCoroutine(Wave(UnityEngine.Random.Range(0, waves / 10 + 10)));
    }
    public void AddDMG()
    {
        extraDMG += UnityEngine.Random.Range(extraDMG / 10, extraDMG / 1) * waves / 10;
        pistolDMG += extraDMG;
        tommyDMG += extraDMG;
        autogunDMG += extraDMG;
        rifleDMG += extraDMG;
    }
    public void AddHP()
    {
        extraHP += UnityEngine.Random.Range(extraHP / 10, extraHP / 1) * waves / 10;
        pistolHP += extraHP;
        tommyHP += extraHP;
        autogunHP += extraHP;
        rifleHP += extraHP;
    }
    public void Addbspeed()
    {
        extrabspeed += UnityEngine.Random.Range(extrabspeed / 10, extrabspeed / 1) * waves / 10;
        pistolbspeed += extrabspeed;
        tommybspeed += extrabspeed;
        autogunbspeed += extrabspeed;
        riflebspeed += extrabspeed;
    }
    public void AddPistol()
    {

    }

    //public void EndGame() EndGame уже есть
    //{
    //    Debug.Log("EndGame");
    //    Time.timeScale = 0;
    //    //AlexBrand-123
    //        //Destroy(game);
    //        //UI.EndGame();
    //}
}



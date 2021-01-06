using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int waves;

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

    public int pistolAmmo = 7;
    public int tommyAmmo = 15;
    public int autogunAmmo = 30;
    public int rifleAmmo = 5;

    public int extraHP;
    public int extraDMG;
    public int extrabspeed;
    //public float fasterReload;
    public Game()
    {
        extraHP = new int();
        extraDMG = new int();
        extrabspeed = new int();
        //fasterReload = new float();
    }
    void FixedUpdate()
    {
        MainSave.save.timeValue += Time.deltaTime;
    }
    public void AddDMG()
    {
        extraDMG += Random.Range(extraDMG / 10, extraDMG / 1) * waves / 10;
    }
    public void AddHP()
    {
        extraHP += Random.Range(extraHP / 10, extraHP / 1) * waves / 10;
    }
    public void Addbspeed()
    {
        extrabspeed += Random.Range(extrabspeed / 10, extrabspeed / 1) * waves / 10;
    }
    public void AddPistolDMG()
    {

    }
}

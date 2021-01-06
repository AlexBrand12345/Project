using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
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
public void FixedUpdate()
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

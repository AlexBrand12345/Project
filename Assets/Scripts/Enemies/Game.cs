using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    MusicControll music;
    bool startWave;
    public bool waveIsOver;
    public float timeBetWaves;
    public AllStats stats;
    public List <GameObject> spawnPoints;
    public List<GameObject> enemies;
    GameObject enemy;
    Vector3 spawnPos;

    public float time2die;
    public int protivnikov;
    public int waves;
   // public List<Sprite> sprites;
    public static Game game = new Game();    
    public int pistolDMG;
    public int autoDMG;
    public int machinegunDMG;
    public int rifleDMG;

    public int pistolHP;
    public int autoHP;
    public int machinegunHP;
    public int rifleHP;

    public int pistolbspeed;
    public int autobspeed;
    public int machinegunbspeed;
    public int riflebspeed;

    public int pistolAmmo;
    public int autoAmmo;
    public int machinegunAmmo;
    public int rifleAmmo;

    public int extraHP = 0;
    public int extraDMG = 0;
    public int extrabspeed = 0;

    public Game()
    {
        //extraHP = new int();
        //extraDMG = new int();
        //extrabspeed = new int();

        //pistolDMG = new int();
        //autoDMG = new int();
        //machinegunDMG = new int();
        //rifleDMG = new int();

        //pistolHP = new int();
        //autoHP = new int();
        //machinegunHP = new int();
        //rifleHP = new int();

        //pistolbspeed = new int();
        //autobspeed = new int();
        //machinegunbspeed = new int();
        //riflebspeed = new int();

        //pistolAmmo = new int();
        //autoAmmo = new int();
        //machinegunAmmo = new int();
        //rifleAmmo = new int();
}
    public void Start()
    {
        game.waves = 0;
        game.time2die = time2die;
        game.stats = stats;
        game.timeBetWaves = timeBetWaves;
        game.startWave = startWave;
        game.waveIsOver = waveIsOver;
        game.spawnPoints = spawnPoints;
        game.enemies = enemies;

        game.pistolHP = pistolHP;
        game.pistolAmmo = pistolAmmo;
        game.pistolbspeed = pistolbspeed;
        game.pistolDMG = pistolDMG;

        game.autoHP = autoHP;
        game.autoAmmo = autoAmmo;
        game.autobspeed = autobspeed;
        game.autoDMG = autoDMG;

        game.machinegunHP = machinegunHP;
        game.machinegunAmmo = machinegunAmmo;
        game.machinegunbspeed = machinegunbspeed;
        game.machinegunDMG = machinegunDMG;

        game.music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
        StartCoroutine(game.Wave(UnityEngine.Random.Range(waves/5 + 2, (waves / 5) + 4)));
    }
    IEnumerator Wave(int bots)
    {
        game.waves++;
        //Debug.Log(waves);
        game.startWave = false;
        game.waveIsOver = true;
        game.protivnikov = bots;
        game.music.StartCoroutine(music.SwitchWave(game.timeBetWaves));
        yield return new WaitForSeconds(game.timeBetWaves);
        stats.StartCoroutine(stats.ShowRedText($"Волна {game.waves}", game.timeBetWaves / 3f));
        var upgrades = new List<Action> { AddDMG, AddHP, Addbspeed };
        if (waves % 5 == 0)
        {
            upgrades[UnityEngine.Random.Range(0, upgrades.Count)]();
        }
        waveIsOver = false;
        for (int i = 0; i < bots; i++)
        {
            game.spawnPos = game.spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position;
            var enemiesCount =  waves / 10 + 1;
            if(enemiesCount >= enemies.Count)
            {
                enemiesCount = enemies.Count;
            }
            game.enemy = game.enemies[UnityEngine.Random.Range(0, enemiesCount)];
            Instantiate(game.enemy, new Vector3(UnityEngine.Random.Range(game.spawnPos.x - 10f, game.spawnPos.x + 10f), game.spawnPos.y, game.spawnPos.z), Quaternion.Euler(0, 0, 0)); 
        }      
        game.startWave = true;
    }
    public void FixedUpdate()
    {
        MainSave.save.timeValue += Time.deltaTime;
        if (game.protivnikov == 0 && !game.waveIsOver && game.startWave)
        {
            //startWave = false;
            MainSave.save.waves++;
            Debug.Log(game.waves / 5);
            StartCoroutine(game.Wave(UnityEngine.Random.Range(game.waves / 5 + 2, (game.waves / 5) + 4)));
        }
    }
    public void AddDMG()
    {
        extraDMG += UnityEngine.Random.Range(extraDMG / 10, extraDMG / 1) * waves / 10;
        game.pistolDMG += extraDMG;
        game.autoDMG += extraDMG;
        game.machinegunDMG += extraDMG;
        game.rifleDMG += extraDMG;
    }
    public void AddHP()
    {
        extraHP += UnityEngine.Random.Range(extraHP / 10, extraHP / 1) * waves / 10;
        game.pistolHP += extraHP;
        game.autoHP += extraHP;
        game.machinegunHP += extraHP;
        game.rifleHP += extraHP;
    }
    public void Addbspeed()
    {
        extrabspeed += UnityEngine.Random.Range(extrabspeed / 10, extrabspeed / 1) * waves / 10;
        game.pistolbspeed += extrabspeed;
        game.autobspeed += extrabspeed;
        game.machinegunbspeed += extrabspeed;
        game.riflebspeed += extrabspeed;
    }
    public void AddPistol()
    {

    }
}



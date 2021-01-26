using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Класс для сохранения игры
/// </summary>

[System.Serializable]
public class MainSave
{
    public static MainSave save = new MainSave();
    public int i;
    public int kills;
    public int deaths;
    public int heals;
    public int exp;
    public float timeValue;
    public int rounds;
    public int waves;
    public int shots;
    public int curSkin;
    public float musicVolume;
    public float effectsVolume;
    public MainSave()
    {
        i = new int();
        kills = new int();
        deaths = new int();
        heals = new int();
        exp = new int();
        timeValue = new float();
        rounds = new int();
        waves = new int();
        shots = new int();
        curSkin = new int();
        musicVolume = new float();
        effectsVolume = new float();
    }
}

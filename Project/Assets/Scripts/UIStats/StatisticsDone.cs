using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticsDone : MonoBehaviour
{
    int i;
    int kills;
    int deaths;
    int heals;
    int exp;
    public float timeValue;
    int rounds;
    int waves;
    int shots;
    //List<int> stats = { kills, deaths, heals, exp, rounds, waves, shots };
    List<int> values;
    public GameObject[] stats;
    GameObject time;
    // Start is called before the first frame update
    public void Awake()
    {
        kills = MainSave.save.kills;
        deaths = MainSave.save.deaths;
        heals = MainSave.save.heals;
        exp = MainSave.save.exp;
        rounds = MainSave.save.rounds;
        timeValue = MainSave.save.timeValue;
        waves = MainSave.save.waves;
        shots = MainSave.save.shots;
    }
    void Start()
    {      
            values = new List<int> { kills, deaths, heals, exp, rounds, waves, shots };
            time = stats[i].transform.parent.GetChild(7).gameObject;
            i = 0;
            foreach (GameObject obj in stats)
            {
                obj.transform.GetChild(1).GetComponent<Text>().text = values[i].ToString();
            }
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeValue);
            time.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);       

    }
}

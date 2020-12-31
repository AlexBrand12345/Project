using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgButScript : MonoBehaviour
{
    public AllStats stats;
    public List<GameObject> Upgrades;
    GameObject upg;
    //public string upgName;

    // Start is called before the first frame update
    void Start()
    {
        upg = Instantiate(Upgrades[UnityEngine.Random.Range(0, Upgrades.Count - 1)], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4), gameObject.transform.rotation, gameObject.transform);
        Debug.Log(gameObject);
        //upgName = upg.name;
    }
    public void GetUpgName()
    {
        stats.LaunchUpg(upg.name, gameObject.transform.parent.gameObject);
    }
}

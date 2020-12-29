using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    Text text;
    Image slider;
    ForBars script;
    public string called;
    public float time;
    public float timeRemain;


    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("UIController");
        script = obj.GetComponent<ForBars>();
        called = gameObject.transform.parent.name + "Time";
        text = gameObject.GetComponent<Text>();
        slider = gameObject.GetComponentInChildren<Image>();
        
    }
    public void Begin()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
        time = script.time(called);
        timeRemain = time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("я запустился");
        timeRemain -= Time.fixedDeltaTime;
        text.text = Mathf.RoundToInt(timeRemain).ToString();
        slider.fillAmount = timeRemain / time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    EffectsControll audioEffects;
    Text text;
    Image slider;
    AllStats script;
    public string called;
    public float time;
    public float timeRemain;


    // Start is called before the first frame update
    void Awake()
    {
        //GameObject obj = GameObject.Find("UIController");
        //script = obj.GetComponent<AllStats>();
        //called = ($"{gameObject.transform.parent.name}" + "Time");
        //Debug.Log(called);
        //text = gameObject.GetComponent<Text>();
        //slider = gameObject.GetComponentInChildren<Image>();
        
    }
    public void Begin()
    {
        audioEffects = GameObject.FindWithTag("EffectsControll").GetComponent<EffectsControll>();
        GameObject obj = GameObject.Find("UIController");
        script = obj.GetComponent<AllStats>();
        called = ($"{gameObject.transform.parent.name}" + "Time");
        Debug.Log(called);
        text = gameObject.GetComponent<Text>();
        slider = gameObject.GetComponentInChildren<Image>();
        gameObject.transform.parent.parent.gameObject.SetActive(true);
        //gameObject.transform.parent.gameObject.SetActive(true);
        time = script.time(called);
        timeRemain = time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeRemain -= Time.fixedDeltaTime;
        text.text = Mathf.RoundToInt(timeRemain).ToString();
        if (timeRemain == 3 || timeRemain == 2 || timeRemain == 1) audioEffects.PlayOneShot(1, clip);
        slider.fillAmount = timeRemain / time;
    }
}

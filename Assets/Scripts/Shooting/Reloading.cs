using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloading : MonoBehaviour
{
    public Slider slider;
    float time2reload;
    float timeLeft;
    
    public void GetTime(float time)
    {
        Debug.Log("GotTime");
        time2reload = time;
        timeLeft = time2reload;
    }
    private void FixedUpdate()
    {
        if (slider.value >= 1) Destroy(gameObject);
        slider.value = (time2reload - timeLeft) / time2reload;
        timeLeft -= Time.deltaTime;
    }
}


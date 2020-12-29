using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Text text;
    int healtht;
    int maxhealtht;
    void Start()
    {
        maxhealtht = 10;
        healtht = maxhealtht;
        slider.maxValue = 10;
        text.text = ($"{healtht}/{maxhealtht}");
    }

   
    void Update()
    {
        
    }
}

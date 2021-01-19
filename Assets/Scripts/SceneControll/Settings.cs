using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    Escbuttons esc;
    private void Start()
    {
        esc = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
    }
    public void Destroy()
    {
        esc.Settings();
    }
}

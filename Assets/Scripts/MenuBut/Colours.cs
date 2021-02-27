using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colours : MonoBehaviour
{
    public List<GameObject> skins;
    Color color;
    public void Start()
    {
        if (MainSave.save.skinColorr == 0 && MainSave.save.skinColorg == 0 && MainSave.save.skinColorb == 0 && MainSave.save.skinColora == 0) SetDefault();
        foreach (GameObject skin in skins)
        {
            skin.GetComponent<Image>().color = new Color(MainSave.save.skinColorr, MainSave.save.skinColorg, MainSave.save.skinColorb, MainSave.save.skinColora);
        }
    }
    public void ChangeColour(GameObject obj)
    {
        color = obj.GetComponent<Image>().color;
        MainSave.save.skinColorr = color.r;
        MainSave.save.skinColorg = color.g;
        MainSave.save.skinColorb = color.b;
        MainSave.save.skinColora = color.a;
        foreach (GameObject skin in skins)
        {
            skin.GetComponent<Image>().color = color;
        }
    }
    void SetDefault()
    {
        MainSave.save.skinColorr = 1;
        MainSave.save.skinColorg = 1;
        MainSave.save.skinColorb = 1;
        MainSave.save.skinColora = 1;
    }
}

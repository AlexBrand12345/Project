using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colours : MonoBehaviour
{
    public List<GameObject> skins;
    Color color;
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
}

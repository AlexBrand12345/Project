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
        skins[MainSave.save.curSkin].GetComponent<Image>().color = color;
    }
}

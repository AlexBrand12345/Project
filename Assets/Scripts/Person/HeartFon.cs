using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartFon : MonoBehaviour
{
    SpriteRenderer sprite;
    public float redTime;
    bool isShoted=false;
    Color color;
    public Image image;
    void Awake()
    {
        image = GetComponent<Image>();
        sprite = GetComponent<SpriteRenderer>();
    }
    public void MakeRed(string tag)
    {      
        switch (tag)
        {
            case "Player":
                image.color = new Color(image.color.r, image.color.g, image.color.b, 60/255.0f);
                break;
            case "Enemy":
                if (!isShoted) StartCoroutine(MakeEnemyRed());
                break;
        }
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 60f);
    }
    void Update()
    {
        //if(gameObject.tag!="Enemy")
        if (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.01f);
        }
    }
    IEnumerator MakeEnemyRed()
    {
        isShoted = true;
        color = sprite.color;
        sprite.color = new Color(255f, 0f, 0f, 60f);
        yield return new WaitForSeconds(redTime);
        sprite.color = color;
        isShoted = false;
    }
}

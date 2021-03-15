using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartFon : MonoBehaviour
{
    SpriteRenderer sprite;
    SpriteRenderer hand;
    [SerializeField] float changeAlpha;
    float minusAlpha;
    public float redTime;
    bool isShoted=false;
    Color color;
    string tagg;
    public Image image;
    void Awake()
    {
        image = GetComponent<Image>();
        if (GetComponent<SpriteRenderer>() != null)
        {
            sprite = GetComponent<SpriteRenderer>();
            hand = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        minusAlpha = changeAlpha;
    }
    public void MakeRed(string tag)
    {
        tagg = tag;
        switch (tag)
        {
            case "Player":
                image.color = new Color(Color.red.r, 0, 0, 60/255.0f);
                break;
            case "Enemy":
                if (!isShoted) StartCoroutine(MakeEnemyRed());
                break;
        }
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 60f);
    }
    public void MakeGreen()
    {
        image.color = new Color(0, Color.green.g, 0, 60 / 255.0f);
    }
    void Update()
    {
        if (tagg == "Player")
            if (image.color.a > 0f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - minusAlpha);
                minusAlpha += 0.05f;
            }
            else minusAlpha = changeAlpha;
    }
    IEnumerator MakeEnemyRed()
    {
        isShoted = true;
        color = sprite.color;
        sprite.color = new Color(255f, 0f, 0f, 60f);
        hand.color = new Color(255f, 0f, 0f, 60f);
        yield return new WaitForSeconds(redTime);
        sprite.color = color;
        hand.color = color;
        isShoted = false;
    }
}

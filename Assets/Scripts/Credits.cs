using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    Escbuttons esc;
    RectTransform rect;
    //Camera camera;
    public RectTransform font;
    float ymax, ymin;
    public float speed;
    Vector2 max, min;
    void Start()
    {
        esc = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        rect = GetComponent<RectTransform>();
        //camera = Camera.main;
        //ymax = camera.WorldToScreenPoint(transform.position).y;
        max = new Vector2(0, font.rect.height / 10);
            //camera.ViewportToWorldPoint(new Vector2(0, 1));
       
        //min = camera.ScreenToWorldPoint(new Vector2(0, 0-gameObject.GetComponent<RectTransform>().rect.height));
        min = new Vector2(0, -font.rect.height / 10);
        Debug.Log(max.y);
        Debug.Log(min.y);
        //GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0));
    }

    void FixedUpdate()
    {
        if (Input.anyKey) esc.LoadScene("Main_Menu LoadingScene");
        //Debug.Log(transform.localPosition.y);
        if (transform.localPosition.y > max.y + rect.rect.height  || transform.localPosition.y < -rect.rect.height + min.y ) speed = -speed;     
        gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

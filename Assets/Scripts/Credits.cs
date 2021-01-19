using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    Escbuttons esc;
    RectTransform rect;
    Camera camera;
    float ymax, ymin;
    public float speed;
    Vector2 max, min;
    void Start()
    {
        esc = GetComponent<Escbuttons>();
        rect = GetComponent<RectTransform>();
        camera = Camera.main;
        //ymax = camera.WorldToScreenPoint(transform.position).y;
        max = camera.ViewportToWorldPoint(new Vector2(0, 1));
        Debug.Log(max);
        //min = camera.ScreenToWorldPoint(new Vector2(0, 0-gameObject.GetComponent<RectTransform>().rect.height));
        min = camera.ViewportToWorldPoint(new Vector2(0, 0));
    }

    void FixedUpdate()
    {
        if (Input.anyKey) esc.LoadScene("Main_Menu");
        Debug.Log(transform.position.y);
        if (transform.position.y > max.y  || transform.position.y < min.y ) speed = -speed;     
        gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon228 : MonoBehaviour
{
    public float offset;
    
    private float ShotTime;
    public float StartTime;

    public GameObject bullet;
    public GameObject effect;
    public Transform bulletPosition;
    GameObject child;
    SpriteRenderer sprite;
    Movement move;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        sprite = transform.parent.parent.GetComponent<SpriteRenderer>();
        move = transform.parent.parent.GetComponent<Movement>();
    }
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - gunPosition.x;
        mousePos.y = mousePos.y - gunPosition.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            //move.isForward = false;
            sprite.flipX = true;
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            if(Input.GetAxis("Horizontal") < 0) move.isForward = true;
            else move.isForward = false;
        }
        else
        {
            //move.isForward = true;
            sprite.flipX = true;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
            if (Input.GetAxis("Horizontal") > 0) move.isForward = true;
            else move.isForward = false;
        }

        if (ShotTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(effect, bulletPosition.position, bulletPosition.rotation);
                Instantiate(bullet, bulletPosition.position, transform.rotation);
                ShotTime = StartTime;
            }
        }
        else
        {
            ShotTime -= Time.deltaTime;
        }
    }
}

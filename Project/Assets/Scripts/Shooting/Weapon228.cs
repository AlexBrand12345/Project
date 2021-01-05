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
    GameObject hands;
    GameObject child;
    SpriteRenderer sprite;
    Movement move;
    Guns gun;

    private void Awake()
    {
        gun = Guns.pistol;
        hands = transform.parent.gameObject;
        child = transform.GetChild(0).gameObject;
        sprite = transform.parent.parent.GetComponent<SpriteRenderer>();
        move = transform.parent.parent.GetComponent<Movement>();
    }    
    private void Start()
    {
        //спавн оружия
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
            sprite.flipX = true;
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            //hands.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            if (Input.GetAxis("Horizontal") < 0) move.isForward = true;
            else move.isForward = false;
        }
        else
        {
            sprite.flipX = true;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
            //hands.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            if (Input.GetAxis("Horizontal") > 0) move.isForward = true;
            else move.isForward = false;
        }


       
        if (Input.GetMouseButtonDown(0))
            {
            if (ShotTime <= 0)
            {
                 Shoot();
                 //Instantiate(effect, bulletPosition.position, bulletPosition.rotation);
            }
            else
            {
                 ShotTime -= Time.fixedDeltaTime;
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, transform.rotation);
        ShotTime = gun.shotTime;
    }
    void SwitchWeapon()
    {

    }
}

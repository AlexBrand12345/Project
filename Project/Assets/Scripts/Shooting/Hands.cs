using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    int index;
    public float offset;
    
    private float ShotTime;
    public float StartTime;

    public GameObject bullet;
    public GameObject effect;
    public Transform bulletPosition;
    public List<Sprite> sprites;
    GameObject batka;
    GameObject child;
    SpriteRenderer sprite;
    Player player;


    private void Awake()
    {
        batka = transform.parent.gameObject;
        sprite = transform.parent.GetComponent<SpriteRenderer>();
        player = transform.parent.GetComponent<Player>();
    }    
    private void Start()
    {
        //спавн оружия
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F)) index = 0;
        //else if (Input.GetKeyDown(KeyCode.Alpha1)) index = 1;
        //else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 2;
        //else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 3;
        ////gun = guns[index];
        //sprite.sprite = sprites[index]; 

        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - gunPosition.x;
        mousePos.y = mousePos.y - gunPosition.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            //sprite.flipX = true;
            //transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            batka.transform.rotation = Quaternion.Euler(0,180,0);
            if (Input.GetAxis("Horizontal") < 0) player.isForward = true;
            else player.isForward = false;
        }
        else
        {
            //sprite.flipX = true;
            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
            batka.transform.rotation = Quaternion.Euler(0, 180, 0);          
            if (Input.GetAxis("Horizontal") > 0) player.isForward = true;
            else player.isForward = false;
        }
        
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, transform.rotation);
    }
    //void SwitchWeapon()
    //{
        //if (Input.GetKeyDown(KeyCode.F)) index = 0;
        //else if (Input.GetKeyDown(KeyCode.Alpha1)) index = 1;
        //else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 2;
        //else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 3;
        //gun = guns[index];
        //sprite.sprite = sprites[index];
    //}
}

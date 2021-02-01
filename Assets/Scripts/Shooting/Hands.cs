using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    int index;
    public float offset;
    public bool canRotate;
    private float ShotTime;
    public float StartTime;

    //public GameObject bullet;
    //public GameObject effect;
    //public Transform bulletPosition;
    //public List<Sprite> sprites;
    public GameObject reload;
    public List<GameObject> guns;
    GameObject batka;
    GameObject gun;
    //SpriteRenderer sprite;
    Player player;
    public Weapon weapon;

    private void Awake()
    {
        canRotate = true;
        batka = transform.parent.gameObject;
        //sprite = transform.parent.GetComponent<SpriteRenderer>();
        player = transform.parent.GetComponent<Player>();
    }    
    private void Start()
    {
        //спавн оружия
        gun = guns[0];
        weapon = gun.GetComponent<Weapon>();
        gun.SetActive(true);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - gunPosition.x;
        mousePos.y = mousePos.y - gunPosition.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (canRotate) 
        { 
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
              //sprite.flipX = true;
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
                batka.transform.rotation = Quaternion.Euler(0, -180, 0);
                if (Input.GetAxis("Horizontal") < 0) player.isForward = true;
                else player.isForward = false;
            }
            else
            {
                //sprite.flipX = true;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
                batka.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (Input.GetAxis("Horizontal") > 0) player.isForward = true;
                else player.isForward = false;
            }
        //transform.rotation = Quaternion.Euler(new Vector3(0f, batka.transform.rotation.y, gunAngle));
        }

    }

    public void Shoot()
    {
        StartCoroutine(weapon.Shoot());
    }
    //public void SwitchWeapon(int index)
    //{
    //if (gun != guns[index])
    //{
    //    gun.SetActive(false);
    //    gun = guns[index];
    //    weapon = gun.GetComponent<Weapon>();
    //    gun.SetActive(true);
    //}
    //}
}

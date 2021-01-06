using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    RectTransform rect;
    SpriteRenderer sprite;
    public int damage; //урон пули
    public int bspeed; //скорость пули
    public float shotTime;
    public int ammo;
    //public static Guns pistol = new Guns { damage = 1, bspeed = 10, shotTime = 1f, ammo = 7};
    //public static Guns tommy = new Guns { damage = 2, bspeed = 25, shotTime = 0.5f, ammo = 15};
    //public static Guns autogun = new Guns { damage = 3, bspeed = 35, shotTime = 1.5f, ammo = 30};
    //public static Guns rifle = new Guns{ damage = 10, bspeed = 50, shotTime = 4f, ammo = 5};

    public void Start()
    {
        rect = GetComponent<RectTransform>();
        sprite = GetComponent<SpriteRenderer>();
        switch(gameObject.name)
        {
            case "pistol":
                damage = Game.game.pistolDMG;
                bspeed = Game.game.pistolbspeed;
                shotTime = 1f;
                ammo = Game.game.pistolAmmo;
                break;
            case "tommy":
                damage = Game.game.tommyDMG;
                bspeed = Game.game.tommybspeed;
                shotTime = 0.5f;
                ammo = Game.game.tommyAmmo;
                break;
            case "autogun":
                damage = Game.game.autogunDMG;
                bspeed = Game.game.autogunbspeed;
                shotTime = 1.5f;
                ammo = Game.game.autogunAmmo;
                break;
            case "rifle":
                damage = Game.game.rifleDMG;
                bspeed = Game.game.riflebspeed;
                shotTime = 4f;
                ammo = Game.game.rifleAmmo;                
                break;
        }
    }
    public IEnumerator Shoot()
    {
        Instantiate(bullet, new Vector3(rect.rect.xMax, rect.rect.y / 2, 0), Quaternion.Euler(0, 0, 0), gameObject.transform);
        yield return new WaitForSeconds(shotTime);
    }
    
}

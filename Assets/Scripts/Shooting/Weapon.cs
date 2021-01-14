using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Player player;
    public GameObject bullet;
    RectTransform rect;
    // SpriteRenderer sprite; 
    //public int baseDMG;
    //public int basebspeed;
    //public int baseAmmo;
    public float damage; //урон пули
    public int bspeed; //скорость пули
    public float time2reload;
    public float shotTime;
    public int ammo;
    public int ammoLeft;
    int shoted;
    bool reloading;
    bool firing;

    //public static Guns pistol = new Guns { damage = 1, bspeed = 10, shotTime = 1f, ammo = 7};
    //public static Guns tommy = new Guns { damage = 2, bspeed = 25, shotTime = 0.5f, ammo = 15};
    //public static Guns autogun = new Guns { damage = 3, bspeed = 35, shotTime = 1.5f, ammo = 30};
    //public static Guns rifle = new Guns{ damage = 10, bspeed = 50, shotTime = 4f, ammo = 5};

    public void Start()
    {
        player = transform.parent.parent.GetComponent<Player>();
        rect = GetComponent<RectTransform>();
        // sprite = GetComponent<SpriteRenderer>();
            switch (gameObject.name)
            {           
                case "pistol":
                    damage = Game.game.pistolDMG;
                    bspeed = Game.game.pistolbspeed;
                    //shotTime = 1f;
                    ammo = Game.game.pistolAmmo;
                    //sprite.sprite = Game.game.sprites[0];
                    break;
                case "tommy":
                    damage = Game.game.tommyDMG;
                    bspeed = Game.game.tommybspeed;
                    //shotTime = 0.5f;
                    ammo = Game.game.tommyAmmo;
                    //sprite.sprite = Game.game.sprites[1];
                    break;
                case "autogun":
                    damage = Game.game.autogunDMG;
                    bspeed = Game.game.autogunbspeed;
                    //shotTime = 1.5f;
                    ammo = Game.game.autogunAmmo;
                    //sprite.sprite = Game.game.sprites[2];
                    break;
                case "rifle":
                    damage = Game.game.rifleDMG;
                    bspeed = Game.game.riflebspeed;
                    //shotTime = 4f;
                    ammo = Game.game.rifleAmmo;
                    //sprite.sprite = Game.game.sprites[3];
                    break;
                default: //блять я второй, крч мы должны обновлять статистику оружия, ладно, отдельного оружия сможем, но когда у всех увеличивается дамаг или скорость, то нужны методы, нужно их вызывать, скорее всего из player
                damage *= player.DMGmod;
                bspeed += player.bspeed;
                ammo += player.ammo;
                break;
            }
        ammoLeft = ammo;
    }
    public void UpdateDMG(float DMGmod)
    {
        damage *= DMGmod;
    }
    public void Updatebspeed(int speed)
    {   
        bspeed += speed;
    }
    public void UpdateAmmo(int ammunition)
    {
        ammo += ammunition;
    }
    public IEnumerator Shoot()
    {
        Debug.Log(reloading);
        while (!reloading) 
        { 
        
            if (ammoLeft == 0) StartCoroutine(Reload(time2reload));       
            else        
            {
                Debug.Log(firing);
                while (!firing) 
                { 
                    Instantiate(bullet, new Vector3(rect.rect.xMax, rect.rect.y / 2, 0), transform.rotation, gameObject.transform);          
                    shoted++;            
                    ammoLeft--;
                    firing = true;
                    yield return new WaitForSeconds(shotTime);
                    firing = false;
                }
            }
        }
    }
    private IEnumerator Reload(float time2reload)
    {
        reloading = true;
        yield return new WaitForSeconds(time2reload);
        ammoLeft = ammo;
        reloading = false;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : BaseWeapon
{
    [SerializeField]
    GameObject reloadObj;
    [SerializeField]
    GameObject reload;
    [SerializeField]
    GameObject canvas;
    //Player player;
    //public GameObject bullet;
    //RectTransform rect;
    // SpriteRenderer sprite; 
    //public int baseDMG;
    //public int basebspeed;
    //public int baseAmmo;
    //public float damage; //урон пули
    //public int bspeed; //скорость пули
    //public float time2reload;
    //public float shotTime;
    //public int ammo;
    //public int ammoLeft;
    //int shoted;
    //bool reloading = false;
    //bool firing;

    //public static Guns pistol = new Guns { damage = 1, bspeed = 10, shotTime = 1f, ammo = 7};
    //public static Guns tommy = new Guns { damage = 2, bspeed = 25, shotTime = 0.5f, ammo = 15};
    //public static Guns autogun = new Guns { damage = 3, bspeed = 35, shotTime = 1.5f, ammo = 30};
    //public static Guns rifle = new Guns{ damage = 10, bspeed = 50, shotTime = 4f, ammo = 5};

    public new void Start()
    {
        base.Start();
        reloadObj = null;
        reload = transform.parent.GetComponent<Hands>().reload;       
        canvas = player.canvas;
        //sprite = GetComponent<SpriteRenderer>();       
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
    public override IEnumerator Reload(float time2reload)
    {
        reloadObj = Instantiate(reload, new Vector3(Camera.main.transform.position.x, player.gameObject.transform.position.y - player.gameObject.GetComponent<RectTransform>().rect.height, 0), Quaternion.identity, canvas.transform);
        reloadObj.GetComponent<Reloading>().GetTime(time2reload);
        yield return new WaitForSeconds(time2reload);
        ammoLeft = ammo;
        reloading = false;
    }
    //public void Shoot()
    //{
    //    //Debug.Log(reloading);
    //    if (!reloading)
    //    {
    //        if (ammoLeft == 0) GoToReload();
    //        else
    //        {
    //            //Debug.Log(firing);
    //            if (!firing)
    //            {
    //                StartCoroutine(ShootCor());
    //            }
    //        }
    //    }
    //}
    //public IEnumerator ShootCor()
    //{

    //    firing = true;
    //    Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, transform);  
    //    shoted++;            
    //    ammoLeft--;                   
    //    yield return new WaitForSeconds(shotTime);
    //    firing = false;
    //}
    //public void GoToReload()
    //{
    //    if (!reloading)
    //    {
    //        StartCoroutine(Reload(time2reload));
    //    }
    //    reloading = true;
    //}


}

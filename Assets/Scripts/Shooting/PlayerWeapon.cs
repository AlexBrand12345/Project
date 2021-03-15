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
    AllStats stats;
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
    //public static Guns auto = new Guns { damage = 2, bspeed = 25, shotTime = 0.5f, ammo = 15};
    //public static Guns machinegun = new Guns { damage = 3, bspeed = 35, shotTime = 1.5f, ammo = 30};
    //public static Guns rifle = new Guns{ damage = 10, bspeed = 50, shotTime = 4f, ammo = 5};

    public new void Start()
    {
        base.Start();
        //reloadObj = null;
        //reload = transform.parent.GetComponent<Hands>().reload;       
        canvas = player.canvas;
        stats = GameObject.FindWithTag("UIController").GetComponent<AllStats>();
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
        reloading = true;
        //Debug.Log(player.reloading);
        stats.Reload(time2reload);
        yield return new WaitForSeconds(time2reload);
        //Destroy(reloadObj);
        reloadObj = null;
        ammoLeft = ammo;
        reloading = false;
    }
    public override void StartReloading(float time2reload)
    {
        reloadingCor = StartCoroutine(Reload(time2reload));
    }
    public override void StopReloading()
    {       
        if(reloadingCor != null)
        StopCoroutine(reloadingCor);
        stats.StopReloading();
        Debug.Log("stopped");
        reloading = false;
    }

}

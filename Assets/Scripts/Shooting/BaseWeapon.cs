using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public Player player;
    public GameObject bullet;
    RectTransform rect;
    // SpriteRenderer sprite; 
    //public int baseDMG;
    //public int basebspeed;
    //public int baseAmmo;
    [SerializeField]
    public float damage; //урон пули
    [SerializeField]
    public int bspeed; //скорость пули
    [SerializeField]
    public float time2reload;
    [SerializeField]
    public float shotTime;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    Animator animator;
    public int ammo;
    public int ammoLeft;
    int shoted;
    public bool reloading = false;
    bool firing;
    [SerializeField]
    protected Coroutine reloadingCor;
    public void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rect = GetComponent<RectTransform>();
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
            //case "Ppistol":
            //    break;
            default: //блять я второй, крч мы должны обновлять статистику оружия, ладно, отдельного оружия сможем, но когда у всех увеличивается дамаг или скорость, то нужны методы, нужно их вызывать, скорее всего из player
                damage *= player.DMGmod;
                bspeed += player.bspeed;
                ammo += player.ammo;
                break;                
        }
        ammoLeft = ammo;
    }
    public void Shoot()
    {
        //Debug.Log(reloading);
        if (!reloading)
        {
            if (ammoLeft == 0) GoToReload();
            else
            {
                //Debug.Log(firing);
                if (!firing)
                {
                    source.PlayOneShot(source.clip);
                    StartCoroutine(ShootCor());
                }
            }
        }
    }
    public IEnumerator ShootCor()
    {
        firing = true;
        animator.SetBool("isFiring", true);
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, transform);
        shoted++;
        if (transform.parent.parent.tag == "Player") MainSave.save.shots++;
        ammoLeft--;
        yield return new WaitForSeconds(shotTime);
        firing = false;
        animator.SetBool("isFiring", false);
    }
    public virtual IEnumerator Reload(float time2reload)
    {
        yield return new WaitForSeconds(time2reload);
        ammoLeft = ammo;
        reloading = false;
    }
    public void GoToReload()
    {
        if (!reloading)
        {
            StartReloading(time2reload);
        }
        reloading = true;
    }
    public virtual void StartReloading(float time2reload)
    {
        reloadingCor = StartCoroutine(Reload(time2reload));
    }

    public virtual void StopReloading()
    {

    }
}

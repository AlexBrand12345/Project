using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Player : Person
{
    public GameObject font;
    public List<Sprite> fonts;
    public HeartFon heartFon;
    public GameObject canvas;
    bool alreadyDead = false;
    public bool paused;
    Escbuttons esc;
    AllStats stats;
    int index;
    int CurIndex;
    public bool canShoot = true;


    //public int pistolAmmo;
    //public int pistolbspeed;
    //public int pistolDMG;
    //public int autogunAmmo;
    //public int autogunbspeed;
    //public int autogunDMG;
    //public int tommyAmmo;
    //public int tommybspeed;
    //public int tommyDMG;
    //public int rifleAmmo;
    //public int riflebspeed;
    //public int rifleDMG;


    public float DMGmod = 1f;
    public int ammo;
    public int bspeed;
    public int baseDMG;
    public int maxExp;
    public int exp;
    public int gainExp = 1;
    public int health2heal;

    SpriteRenderer sprite;
    public List<Sprite> sprites;
    public List<Sprite> hands;
    EffectsControll effects;
    public AudioClip updClip;
    public AudioClip healClip;
    //int maxHealth;
    //int health;
    //int baseDMG;
    //int exp;
    //int maxExp;

    public float moveInput;
    [SerializeField]
    public Hands hand;

    private Collider2D bg;

    private new void Awake()
    {
        index = 0;
        CurIndex = 0;
        base.Awake();
        effects = GameObject.FindWithTag("EffectsControll").GetComponent<EffectsControll>();
        esc = GameObject.FindGameObjectWithTag("EscController").GetComponent<Escbuttons>();
        stats = GameObject.Find("UIController").GetComponent<AllStats>();
        bg = GameObject.FindWithTag("Background").GetComponent<Collider2D>();
        hand = GetComponentInChildren<Hands>();
        //Debug.Log(MainSave.save.curSkin);
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[MainSave.save.curSkin];
        sprite.color = MainSave.save.skinColor;
        hand.gameObject.GetComponent<SpriteRenderer>().sprite = hands[MainSave.save.curSkin];
        hand.gameObject.GetComponent<SpriteRenderer>().color = MainSave.save.skinColor;
        font.GetComponent<Image>().sprite = fonts[Random.Range(0, fonts.Count)]; //для переключения неба, если оно есть (а оно есть, но выключено)
    }
    private new void Update()
    {
        if (!stats.alreadyDead) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        //if (!stats.alreadyDead && canShoot)
        if (!paused)
        {
            base.Update();
            moveInput = Input.GetAxis("Horizontal");
            Move(moveInput);

            if(Input.GetKeyDown(KeyCode.R))
            {
                hand.weapon.GoToReload();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
               
            if (Input.GetKeyDown(KeyCode.F))
            {              
                index = 0;
                hand.SwitchWeapon(CurIndex, index);
                CurIndex = index;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                index = 1;
                hand.SwitchWeapon(CurIndex, index);
                CurIndex = index;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                index = 2;
                hand.SwitchWeapon(CurIndex, index);
                CurIndex = index;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                index = 3;
                hand.SwitchWeapon(CurIndex, index);
                CurIndex = index;
            }
            //if(!esc.IsLoading) CheckOnBG();
        }
       
    }
    private void FixedUpdate()
    {
        if (gameObject) CheckOutOfView();
        //if (!paused && gameObject)
        if (!paused)
        {            
            if (Input.GetMouseButton(0)) hand.Shoot();         
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "medkit" && health != maxHealth)
        {
            Heal(health2heal);
            Destroy(collision.gameObject);
        }               
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!esc.IsLoading && collision == bg) 
        CheckOnBG();
    }
    public void CheckOutOfView()
    {
        if (body.transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y)
        {
            Debug.Log("ExitMap");
            Destroy(gameObject);
            StopCoroutine(stats.End);
            StopAllCoroutines();
            //Debug.Log(stats.StartGameOver().Current);
            stats.StartCoroutine(stats.GameOver());
            Debug.Log("started");
        }
    }
    public override void Die()
    {
        health = 0;
        MainSave.save.deaths++;
        stats.GuiDie();
        Debug.Log("Die");
        body.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        body.gravityScale = 0;
        body.velocity = new Vector2(0, 10);
        Debug.Log(body.velocity);
        //Game.game.EndGame();
        //if (transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y)
        //{
        //    Debug.Log("ExitMap");
        //    Destroy(gameObject);
        //    StopCoroutine(stats.End);
        //    Debug.Log(stats.StartGameOver().Current);
        //    StartCoroutine(stats.GameOver());
        //}
    }
    void CheckOnBG()
    {
        //if (!gameObject.GetComponent<Collider2D>().IsTouching(bg) && gameObject != null)
        if (gameObject != null)
        {
            Die();
        }
    }
    public void Pause()
    {
        esc.OnEscape();
        //paused = !paused;
    }
    public void Heal(int heal)
    {
        effects.PlayOneShot(0, healClip);
        health += heal;
        MainSave.save.heals++;
        if (health >= maxHealth) health = maxHealth;
    }
    //TakeDamage уже есть у бати у Person
    public void GainExp()
    {
        exp += gainExp;
        MainSave.save.exp += gainExp;
        if (exp > maxExp) exp = maxExp;
    }
    public void LaunchUpg(string upgName, GameObject upgrades)
    {
        switch (upgName.Split('(')[0])
        {
            case "PlusEXP":
                PlusEXP();
                break;
            case "PlusDMG":
                PlusDMG();
                break;
            case "PlusHP":
                PlusHP();
                break;
            default:
                Debug.Log("ничего не произошло");
                break;
        }
        effects.PlayOneShot(1, updClip);
        body.WakeUp();
        Destroy(upgrades);
        paused = false;
    }

    public void PlusEXP()
    {
        gainExp++;
    }
    public void PlusDMG()
    {
        DMGmod *= Random.Range(1.1f, 1.5f);
        foreach(GameObject weapon in hand.guns)
        {
            weapon.GetComponent<PlayerWeapon>().UpdateDMG(DMGmod);
        }
    }
    public void PlusHP()
    {
        maxHealth = maxHealth * 3 / 2;
        health = maxHealth;
    }
    public void SetMaxExp()
    {
        maxExp = maxExp * 3 / 2;
        exp = 0;
    }

}

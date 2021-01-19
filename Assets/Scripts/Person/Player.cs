using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : Person
{
    bool alreadyDead = false;
    bool paused = false;
    Escbuttons esc;
    AllStats stats;
    int index;
    public bool canShoot = true;
    public float DMGmod = 1f;
    public int ammo;
    public int bspeed;

    public int baseDMG;

    public int maxExp;
    public int exp;
    public int gainExp = 1;

    SpriteRenderer sprite;
    public List<Sprite> sprites;
    public List<Sprite> hands;
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
        base.Awake();
        esc = GameObject.FindGameObjectWithTag("EscController").GetComponent<Escbuttons>();
        stats = GameObject.Find("UIController").GetComponent<AllStats>();
        bg = GameObject.FindWithTag("Background").GetComponent<Collider2D>();
        hand = GetComponentInChildren<Hands>();
        sprite = GetComponent<SpriteRenderer>();
        //sprite.sprite = sprites[MainSave.save.curSkin];
        //hand.gameObject.GetComponent<SpriteRenderer>().sprite = hands[MainSave.save.curSkin];
    }
    private new void Update()
    {
        if (canShoot)
        {
            base.Update();
            moveInput = Input.GetAxis("Horizontal");
            Move(moveInput);

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause();               
            }
            if (Input.GetMouseButtonDown(0) && !paused) hand.Shoot();

            if (Input.GetKeyDown(KeyCode.F))
            {
                index = 0;
                //hand.SwitchWeapon(index);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                index = 1;
                //hand.SwitchWeapon(index);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                index = 2;
                //hand.SwitchWeapon(index);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                index = 3;
                //hand.SwitchWeapon(index);
            }

            }
        if (!gameObject.GetComponent<Collider2D>().IsTouching(bg) && gameObject != null)
        {
            Die();
        }
    }
    public override void Die()
    {
        health = 0;
        Debug.Log("Die");
        body.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        body.gravityScale = 0;
        body.velocity = new Vector2(0, 10);
        Debug.Log(body.velocity);
        //Game.game.EndGame();
        if (body.transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y)
        {
            Debug.Log("ExitMap");
            Destroy(gameObject);
            StopCoroutine(stats.End);
            Debug.Log(stats.StartGameOver().Current);
            StartCoroutine(stats.GameOver());
        }
    }
    public void Pause()
    {
        esc.OnEscape(paused);
        paused = !paused;
    }
    public void Heal(int heal)
    {
        health += heal;
        if (health >= maxHealth) health = maxHealth;
    }
    //TakeDamage уже есть у бати у Person
    public void GainExp()
    {
        exp += gainExp;
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
        Destroy(upgrades);
    }

    public void PlusEXP()
    {
        exp++;
    }
    public void PlusDMG()
    {
        DMGmod *= Random.Range(1.1f, 1.5f);
        foreach(GameObject weapon in hand.guns)
        {
            weapon.GetComponent<Weapon>().UpdateDMG(DMGmod);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : Person
{

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
            if (Input.GetMouseButtonDown(0)) hand.Shoot();

            if (Input.GetKeyDown(KeyCode.F)) index = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha1)) index = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 3;
            hand.SwitchWeapon(index);
        }
        if (!gameObject.GetComponent<Collider2D>().IsTouching(bg))
        {
            Die();
        }
    }
    public override void Die()
    {
        Debug.Log("Die");
        //body.gravityScale -= 3 * body.gravityScale;
        Game.game.EndGame();
        Destroy(gameObject);
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health >= maxHealth) health = maxHealth;
    }
    //TakeDamage уже есть у бати
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

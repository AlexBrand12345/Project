using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : Person
{
    public int maxHealth;
    public int baseDMG;
    public int exp;
    public int maxExp;
    public int gainExp = 1;
    //int maxHealth;
    //int health;
    //int baseDMG;
    //int exp;
    //int maxExp;

    public float moveInput;
    private Hands hands;

    void Awake()
    {
        hands = GetComponentInChildren<Hands>();
    }
    new void Update()
    {
        base.Update();
        moveInput = Input.GetAxis("Horizontal");
        Move(moveInput);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0)) hands.Shoot();
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
        baseDMG = baseDMG * 3 / 2;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
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

    private void Start()
    {
        
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
    public virtual void SetMaxE()
    {
        maxExp = maxExp * 3 / 2;
        exp = 0;
    }

}

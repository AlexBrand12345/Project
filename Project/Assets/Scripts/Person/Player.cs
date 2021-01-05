using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public int maxHealth;
    public int maxExp;
    public int gainExp = 1;
    public int exp;

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
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : Movement
{
    public int health;
    public int baseDMG;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die(); 
        }
    }
    public void Die()
    {
        if (tag != "Player") Destroy(gameObject);
    }
}

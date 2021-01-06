
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : Movement
{
    [Header("Статы")]
    public int health;

    protected new void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die(); 
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

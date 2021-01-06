using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : Movement
{
    [Header("Stats")]
    public int health;
    public int maxHealth;

    protected new void Awake()
    {
        base.Awake();
    }
    private void Start()
    {

    }
    protected new void Update()
    {
        base.Update();
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
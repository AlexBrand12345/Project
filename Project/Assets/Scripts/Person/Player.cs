using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }
    public void Die()
    {
        if (tag != "Player") Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : Movement
{
    float health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    void Die()
    {
        if (health <= 0) Destroy(gameObject);
    }
}

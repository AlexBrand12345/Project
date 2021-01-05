using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : Movement
{
    float health;

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        //Die();
    }

    void Die()
    {
        if (health <= 0) Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy0 : BaseEnemy
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    private new void Awake()
    {
        base.Awake();
        maxHealth = Game.game.pistolHP;
        health = maxHealth;
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        //Hand Controller
       
    }
}

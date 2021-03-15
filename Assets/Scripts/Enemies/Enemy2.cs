using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : BaseEnemy
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }
    private new void Awake()
    {
        base.Awake();
        maxHealth = Game.game.machinegunHP;
        health = maxHealth;
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
}

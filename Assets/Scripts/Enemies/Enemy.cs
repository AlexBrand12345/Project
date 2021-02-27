using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy : BaseEnemy
{
    [SerializeField] GameObject hand;
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
        if (SearchPlayer())
        {
            float x = -player.transform.position.x + transform.position.x;
            float y = -player.transform.position.y + transform.position.y;
            float gunAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            Debug.Log(gunAngle);
            if (body.velocity.x <= -0.01f) hand.transform.rotation = Quaternion.Euler(0, -180, -gunAngle);
            if (body.velocity.x >= 0.01f) hand.transform.rotation = Quaternion.Euler(0, 0, -gunAngle);
        }
        else
        {
            if (body.velocity.x <= -0.01f) hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation,Quaternion.Euler(0, -180, 0), 0.1f);
            if (body.velocity.x >= 0.01f) hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
        }
    }
}

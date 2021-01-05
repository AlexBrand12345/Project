using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    int damage;
    int speed;
    public float lifeTime = 2;

    Weapon parent;

    public void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Weapon>();
        damage = parent.damage;
        speed = parent.bspeed;
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        lifeTime = lifeTime - Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Enemy GameObject enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
        //{
        //    enemy.TakeDamage(damage);
        //}
        //Destroy(gameObject);
    }
}

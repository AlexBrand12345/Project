using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    int damage;
    int speed;
    public float lifeTime = 4f;

    Weapon parent;

    public void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Weapon>();
        damage = (int)parent.damage;
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
        if (transform.parent.parent.parent.gameObject.tag == "Enemy" && hitInfo.gameObject.tag == "Enemy") return;
        else
        {
            GameObject obj = hitInfo.gameObject;
            if (obj.tag == "Enemy")
            {
                obj.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (obj.tag == "Player")
            {
                obj.GetComponent<Player>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        //Enemy GameObject enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
        //{
        //    enemy.TakeDamage(damage);
        //}
        //Destroy(gameObject);
    }
}

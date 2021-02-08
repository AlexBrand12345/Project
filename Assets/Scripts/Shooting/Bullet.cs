using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject batyanya;
    int damage;
    int speed;
    public float lifeTime = 4f;

    BaseWeapon parent;

    public void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<BaseWeapon>();
        batyanya = transform.parent.parent.parent.gameObject;
        damage = (int)parent.damage;
        speed = parent.bspeed;
        transform.parent = null;
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
    public void DoDamage(GameObject target)
    {
        if (batyanya.tag == target.tag) return;
        else target.GetComponent<Person>().TakeDamage(damage);
        Destroy(gameObject);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("EnterCol");
    //    //if (transform.parent.parent.parent.gameObject.tag == "Enemy" && collision.gameObject.tag == "Enemy") return;
    //    if (batyanya.tag == "Enemy" && collision.gameObject.tag == "Enemy") return;
    //    else
    //    {
    //        GameObject obj = collision.gameObject;
    //        if (obj.tag == "Enemy")
    //        {
    //            Debug.Log("Do damage");
    //            obj.GetComponent<Enemy>().TakeDamage(damage);
    //        }
    //        else if (obj.tag == "Player")
    //        {
    //            obj.GetComponent<Player>().TakeDamage(damage);
    //        }
    //        Destroy(gameObject);
    //    }
        //Enemy GameObject enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
        //{
        //    enemy.TakeDamage(damage);
        //}
        //Destroy(gameObject);
    ////}
}

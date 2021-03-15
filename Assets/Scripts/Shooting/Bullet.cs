using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject batyanya;
    string batyanyatag;
    int damage;
    int speed;
    public float lifeTime = 4f;

    BaseWeapon parent;

    public void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<BaseWeapon>();
        batyanya = transform.parent.parent.parent.gameObject;
        batyanyatag = batyanya.tag;
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
    //public void DoDamage(GameObject target)
    //{
    //    if (batyanya.tag == target.tag) return;
    //    else if (target.tag == "ground") Destroy(gameObject);
    //    else target.GetComponent<Person>().TakeDamage(damage);
    //    Destroy(gameObject);
    //}
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (batyanyatag == collider.gameObject.tag) return;
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Person>().TakeDamage(damage);
            Destroy(gameObject);
        } else if(collider.gameObject.tag == "Ground") Destroy(gameObject);

    }
}

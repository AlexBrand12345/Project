using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Person : Movement
{
    HeartFon heartFon;
    [Header("Stats")]
    public int health;
    public int maxHealth;

    protected new void Awake()
    {
        base.Awake();
        if (gameObject.tag == "Player")
        {
            heartFon = GameObject.Find("HeartFon").GetComponent<HeartFon>();
        }
        else heartFon = GetComponent<HeartFon>();
    }
    private void Start()
    {

    }
    protected new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.GetComponent<Bullet>().DoDamage(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("damage");
        health -= damage;
        heartFon.MakeRed(gameObject.tag);
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("Die");
            health = 0;
            Die();
        }
    }
    public virtual void Die()
    { 
        Destroy(gameObject);
    }
}
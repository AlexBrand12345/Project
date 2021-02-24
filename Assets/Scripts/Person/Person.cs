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
    [SerializeField]
    public AudioClip[] clips; //0-takeDMG 1-Die
    protected AudioSource source;

    protected new void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();      
    }
    protected void LookForComponents(bool need)
    {
        Debug.Log("searching");
        if(!source)
        source = GetComponent<AudioSource>();
        if(!heartFon)
        if (gameObject.tag == "Player")
        {
            heartFon = GameObject.Find("HeartFon").GetComponent<HeartFon>();
        }
        else heartFon = GetComponent<HeartFon>();
        //if (source && heartFon) need = false;
        //if(!need) LookForComponents(need);
    }
    public void Start()
    {
        LookForComponents(true);
        //if (gameObject.tag == "Player")
        //{
        //    heartFon = GameObject.Find("HeartFon").GetComponent<HeartFon>();
        //}
        //else heartFon = GetComponent<HeartFon>();
    }
    protected new void Update()
    {
        base.Update();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        collision.gameObject.GetComponent<Bullet>().DoDamage(gameObject);
    //    }
    //}
    public void TakeDamage(int damage)
    {

        health -= damage;
        source.clip = clips[0];
        source.Play();
        //heartFon = GetComponent<HeartFon>();

        heartFon.MakeRed(gameObject.tag);

        if (health <= 0)
        {
            source.clip = clips[1];
       
            source.PlayOneShot(clips[1]);
            
            health = 0;
            Die();
        }
       
    }
    public virtual void Die()
    {         
        source.Play();
        Destroy(gameObject);
    }
}
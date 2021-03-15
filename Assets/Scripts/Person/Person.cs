using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Person : Movement
{
    HeartFon heartFon;
    public GameObject explosionPref;
    GameObject explosion;
    protected SpriteRenderer sprite;
    [Header("Stats")]
    public int health;
    public int maxHealth;
    [SerializeField]
    public AudioClip[] clips; //0-takeDMG 1-Die
    protected AudioSource source;
    bool alreadyDead;

    protected new void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }
    protected void LookForComponents(bool need)
    {
        //Debug.Log("searching");
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
    protected void Update()
    {
        base.FixedUpdate();
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
            //source.clip = clips[1];
            //source.Play();           
            health = 0;
            Die();
        }
       
    }
    public virtual void Die()
    {
        //StartCoroutine(BlowUp(2));
    }
    protected void BlowUp(float time2die)
    {
        if (!alreadyDead) 
        {
            alreadyDead = true;
            source.Play();
            explosion = Instantiate(explosionPref, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0));
            explosion.GetComponent<Explosion>().Blow(time2die);
            Destroy(gameObject);
        }
        
    }
}
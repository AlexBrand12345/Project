using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Person : Movement
{
    protected HeartFon heartFon;
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
        if(!source)
        source = GetComponent<AudioSource>();
        if(!heartFon)
        if (gameObject.tag == "Player")
        {
            heartFon = GameObject.Find("HeartFon").GetComponent<HeartFon>();
        }
        else heartFon = GetComponent<HeartFon>();        
    }
    public void Start()
    {
        LookForComponents(true);
        
    }
    protected void Update()
    {
        base.FixedUpdate();
    }
    public void TakeDamage(int damage)
    {

        health -= damage;
        source.clip = clips[0];
        source.Play();

        heartFon.MakeRed(gameObject.tag);      

        if (health <= 0)
        {         
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
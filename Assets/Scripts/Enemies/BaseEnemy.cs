using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : Person
{
    protected Player player;
    [Header("BaseEnemy")]
    public float minSpeed = 1;
    public float maxSpeed = 3;
    public float timeRange = 5;
    public float rotationDistance = 3; //дистанция до стены перед разворотом
    public float jumpDistance; //дистанция до прыжка
    public int moveInput = 1;

    private bool isFalling;

    //Movement move;
    Weapon weapon;

    Coroutine shootingCoroutine;

    protected new void Awake()
    {
        base.Awake();
        StartCoroutine(ChangeSpeedAndDirectionPerTime(timeRange));
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected new void Update()
    {
        base.Update();
       //Debug.Log(rect.rect.yMin);

        //Поворот
        if (moveInput < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        if (moveInput > 0) transform.rotation = Quaternion.Euler(0, 0, 0);

        //Движение
        Move(moveInput);

        //Разворот от препядствия
        if (CheckLet()) ChangeSpeedAndDirection(moveInput);


        //Debug.Log(CheckAbyss());

        //Прыжок над пропастью
        if (!isGrounded & !isFalling) Jump();
        if (!isGrounded & CanLand()) { Fall(); isFalling = true; }
        if (isGrounded | !CanLand()) isFalling = false;

        //Стрельба
        Debug.Log(SearchPlayer());
        if (shootingCoroutine == null)
        {
            if (SearchPlayer()) Debug.Log("Shooting");//shootingCoroutine = StartCoroutine(weapon.Shoot()); 
        }
        else if (!SearchPlayer())
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    IEnumerator ChangeSpeedAndDirectionPerTime(float timeRange)
    {
        float waitingTimeForChangeSpeed = Random.Range(0, timeRange);
        if (isGrounded) ChangeSpeedAndDirection();
        yield return new WaitForSeconds(waitingTimeForChangeSpeed);
        StartCoroutine(ChangeSpeedAndDirectionPerTime(timeRange));
    }

    void ChangeSpeedAndDirection()
    {
        moveInput = Random.Range(-1, 2);
        speed = Random.Range(minSpeed, maxSpeed);
    }
    void ChangeSpeedAndDirection(int direction)
    {
        moveInput = direction * -1;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    bool SearchPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);
        if (hit.rigidbody != null)
        {   
            if (hit.rigidbody.gameObject == player.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    bool SearchPlayerInstantly()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position);
        if (hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject == player.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckLet()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rotationDistance);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != player)
            {
                Debug.Log(hit.collider.name);
                return true;
            }
        }
        return false;
    }

    bool CheckAbyss()
    {
        if (body.velocity.x > 0)
        {
            Vector2 origin = rect.position;
            origin.x += rect.rect.width / 2 + jumpDistance;
            origin.y -= rect.rect.height / 2;
            RaycastHit2D hit = Physics2D.Raycast(origin, origin - Vector2.up);
            if (hit.collider != null)
            {
                if (hit.distance > 1)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        else if (body.velocity.x < 0)
        {
            Vector2 origin = rect.position;
            origin.x -= rect.rect.width / 2 - jumpDistance;
            origin.y -= rect.rect.height / 2;
            RaycastHit2D hit = Physics2D.Raycast(origin, origin - Vector2.up);
            if (hit.collider != null)
            {
                if (hit.distance > 1)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    bool CanLand()
    {
        Vector2 origin = rect.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public override void Die()
    {
        Destroy(gameObject);
        player.GainExp();
    }
}

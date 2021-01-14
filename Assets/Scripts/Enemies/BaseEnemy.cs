using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Базовая модель поведения врага
/// </summary>
public abstract class BaseEnemy : Person
{
    protected Player player;
    [Header("BaseEnemy")]
    public float timeRange = 5;
    public float rotationDistance = 3; //дистанция до стены перед разворотом
    public float jumpDistance; //дистанция до прыжка
    public int moveInput = 1;

    private bool isFalling;

    [Header("AI")]
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Path path;
    Seeker seeker; 

    //Movement move;
    Weapon weapon;

    Coroutine shootingCoroutine;

    protected new void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        seeker = GetComponent<Seeker>();
    }

    protected void Start()
    {
        InvokeRepeating("ChangeSpeedAndDirectionPerTime", 0, timeRange);
        InvokeRepeating("UpdatePath", 0, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) seeker.StartPath(body.position, player.transform.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    protected new void Update()
    {
        base.Update();
        //Debug.Log(rect.rect.yMin);

        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force;
        Debug.Log(direction.y);

        //Нужно для того, чтобы скорость вниз не увеличивалась
        //Path прокладывается по центру клеток, а position объекта может быть с краю клетки
        if (!isGrounded & direction.y <= -0.25f /*Максимальное расстояние от края клетки до центра*/) Fall();
        else
        {
            force = direction * speed * Time.deltaTime;
            if (direction.y > 0) force.y += -Physics2D.gravity.y * 4;
            body.AddForce(force);
        }

        //Поворот
        if (body.velocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (body.velocity.x <= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        //if (!isGrounded & !isFalling) Jump();
        //if (!isGrounded & CanLand()) { Fall(); isFalling = true; }
        //if (isGrounded | !CanLand() | body.velocity.y > 0) isFalling = false;
        //if (!isGrounded & !isFalling)
        //{
        //    force = Vector2.up * speed * Time.deltaTime;
        //    body.AddForce(force);
        //}
        //if (!isGrounded & CanLand()) { 
        //    Fall();
        //    isFalling = true;
        //}


        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Движение
    //    Move(moveInput);

    //    //Разворот от препядствия
    //    if (CheckLet()) ChangeSpeedAndDirection(moveInput);


    //    //Debug.Log(CheckAbyss());

    //    //Стрельба
    //    Debug.Log(SearchPlayer());
    //    if (shootingCoroutine == null)
    //    {
    //        if (SearchPlayer()) Debug.Log("Shooting");//shootingCoroutine = StartCoroutine(weapon.Shoot()); 
    //    }
    //    else if (!SearchPlayer())
    //    {
    //        StopCoroutine(shootingCoroutine);
    //        shootingCoroutine = null;
    //    }
    }

    //void ChangeSpeedAndDirectionPerTime()
    //{
    //    float waitingTimeForChangeSpeed = Random.Range(0, timeRange);
    //    if (isGrounded) ChangeSpeedAndDirection();
    //}

    //void ChangeSpeedAndDirection()
    //{
    //    moveInput = Random.Range(-1, 2);
    //    speed = Random.Range(minSpeed, maxSpeed);
    //}
    //void ChangeSpeedAndDirection(int direction)
    //{
    //    moveInput = direction * -1;
    //    speed = Random.Range(minSpeed, maxSpeed);
    //}

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

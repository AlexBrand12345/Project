using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Базовая модель поведения врага
/// </summary>
public abstract class BaseEnemy : Person
{
    [SerializeField] GameObject hand;
    bool isVisible;
    protected Player player;
    [Header("BaseEnemy")]
    public float timeRange = 5;
    public float rotationDistance = 3; //дистанция до стены перед разворотом
    public float jumpDistance; //дистанция до прыжка
    public int moveInput = 1;
    public float minDistance = 5;

    private bool isFalling;
    
    
    [Header("AI")]
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool need2move;
    Path path;
    Seeker seeker; 

    //Movement move;
    EnemyWeapon weapon;

    Coroutine shootingCoroutine;

    protected new void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        seeker = GetComponent<Seeker>();
    }

    protected new void Start()
    {
        base.Start();
        weapon = transform.GetChild(0).GetChild(0).GetComponent<EnemyWeapon>();
        need2move = true;
        if (need2move) 
        {
            InvokeRepeating("ChangeSpeedAndDirectionPerTime", 0, timeRange);
            InvokeRepeating("UpdatePath", 0, .5f);
        }
        
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
    public void OnBecameVisible()
    {
        isVisible = true;
        //Debug.Log(isVisible);
    }
    public void OnBecameInvisible()
    {
        isVisible = false;
        //Debug.Log(isVisible);
    }
    protected new void Update()
    {
        base.Update();
        //Debug.Log(rect.rect.yMin);
        if (player != null) {
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

            if (SearchPlayer())
            {
                need2move = false;
                float x = -player.transform.position.x + transform.position.x;
                float y = -player.transform.position.y + transform.position.y;
                float gunAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                //Debug.Log(gunAngle);
                if (body.velocity.x <= -0.01f) hand.transform.rotation = Quaternion.Euler(0, -180, -gunAngle);
                if (body.velocity.x >= 0.01f) hand.transform.rotation = Quaternion.Euler(0, 0, gunAngle-180f);
            }
            else
            {
                need2move = true;
                if (body.velocity.x <= -0.01f) hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation, Quaternion.Euler(0, -180, 0), 0.1f);
                if (body.velocity.x >= 0.01f) hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
            Vector2 force;

            //Нужно для того, чтобы скорость вниз не увеличивалась
            //Path прокладывается по центру клеток, а position объекта может быть с краю клетки
            if (!isGrounded & direction.y <= -0.25f /*Максимальное расстояние от края клетки до центра*/)
            {
                Fall();
                jetAnimator.SetBool("isFlying", false);
            }
            else
            {
                force = direction * speed * Time.deltaTime;
                if (direction.y > 0) force.y += -Physics2D.gravity.y * 4;
                body.AddForce(force);
                jetAnimator.SetBool("isFlying", true);
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

            float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);
            float distanceToPlayer = Vector2.Distance(body.position, player.body.position);
            if (distance < nextWaypointDistance && distanceToPlayer > minDistance && SearchPlayer()) currentWaypoint++;
            if (distance < nextWaypointDistance && !SearchPlayer()) currentWaypoint++;

            //Стрельба
            if (shootingCoroutine == null)
            {
                if (SearchPlayer() && isVisible)
                {
                    //Debug.Log("Shooting");
                    weapon.Shoot();
                }
            }
            else if (!SearchPlayer())
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    protected bool SearchPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.body.position - body.position).normalized);
        Debug.DrawRay(transform.position, player.body.position - body.position, Color.white);
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
         Blow(Game.game.time2die);
    }
    protected void Blow(float time2die)
    {
        Game.game.protivnikov--;
        MainSave.save.kills++;
        player.GainExp();
        BlowUp(time2die);
    }
}

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

    //protected GameObject player;

    private bool isFalling;

    //Movement move;
    Weapon weapon;

    Coroutine shootingCoroutine;
    
    protected new void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(ChangeSpeedAndDirectionPerTime(timeRange));
    }

    protected new void Update()
    {
        base.Update();
       //Debug.Log(rect.rect.yMin);

        if (moveInput == -moveInput) transform.rotation = Quaternion.Euler(0, 180, 0);
        //Движение
        Move(moveInput);

        //Разворот от препядствия
        if (CheckLet()) ChangeSpeedAndDirection(moveInput);

        //Debug.Log(CheckAbyss());
        //Прыжок над пропастью
        if (CheckAbyss())
        {
            if (!isFalling) Jump();
            if (CanLand())
            {
                isFalling = true;
                Fall();
            }
        }
        if (isGrounded) isFalling = false;

        //Стрельба
        if (shootingCoroutine == null)
        {
            if (SearchPlayer()) shootingCoroutine = StartCoroutine(weapon.Shoot());
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
        moveInput = Random.Range(-1, 1);
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
            if (hit.rigidbody.gameObject == player)
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
            Debug.Log(origin.y);
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
        Vector2 v = body.velocity;
        float g = Physics2D.gravity.magnitude;
        float velocityX  = v.x;
        float velocityY0 = 10;
        //float angleDeg = Vector2.Angle(new Vector2(v.x, 0), v);
        //float angleRad = angleDeg * Mathf.Deg2Rad;
        //float distance = Mathf.Pow(velocity, 2) * Mathf.Sin(2 * angleRad) / g; //Максимальное расстояние, на которое возможен прыжок ПО ПРЯМОЙ
        float distance = velocityX * velocityY0 / g;

        Vector3[] arr = new Vector3[4];
        rect.GetWorldCorners(arr);

        if (velocityX < 0)
        {
            Vector2 origin = new Vector2(arr[0].x - distance, arr[0].y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down);
            if (hit.collider != null)
            {
                return true;
            }
        }
        if (velocityX > 0)
        {
            Vector2 origin = new Vector2(arr[3].x + distance, arr[0].y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down);
            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}

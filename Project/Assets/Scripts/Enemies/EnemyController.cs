using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float speedRange;
    public float timeRange;
    public float rotationDistance; //дистанция до стены перед разворотом
    public float jumpDistance; //дистанция до прыжка
    public GameObject player;

    int moveInput;
    Movement move;
    Weapon228 weapon;
    RectTransform rect;

    Coroutine shootingCoroutine;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        move = GetComponent<Movement>();
        StartCoroutine(ChangeSpeedAndDirectionPerTime(timeRange));
    }

    void Update()
    {
        //Движение
        move.Move(moveInput);

        //Разворот от препядствия
        if (CheckLet()) ChangeSpeedAndDirection(moveInput);

        //Прыжок над пропастью (или разворот)
        if (CheckAbyss())
        {
            if (CanJump())
            {
                move.Jump();
            }
            else
            {
                ChangeSpeedAndDirection(moveInput);
            }
        }

        //Стрельба
        if (shootingCoroutine == null)
        {
            //if (SearchPlayer()) shootingCoroutine = StartCoroutine(weapon.Shoot());
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
        ChangeSpeedAndDirection();
        yield return new WaitForSeconds(waitingTimeForChangeSpeed);
        StartCoroutine(ChangeSpeedAndDirectionPerTime(timeRange));
    }

    void ChangeSpeedAndDirection()
    {
        moveInput = Random.Range(-1, 1);
        move.speed = Random.Range(0, speedRange);
    }
    void ChangeSpeedAndDirection(int direction)
    {
        moveInput = direction * -1;
        move.speed = Random.Range(0, speedRange);
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

        if (hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject != player)
            {
                Debug.Log(hit.rigidbody.name);
                return true;
            }
        }
        return false;
    }

    bool CheckAbyss()
    {
        Vector2 origin = new Vector2(rect.rect.xMax + jumpDistance, rect.rect.yMin);
        RaycastHit2D hit = Physics2D.Raycast(origin, origin - Vector2.up);
        if (hit.rigidbody != null)
        {
            if (hit.distance > 0)
            {
                return true;
            }
        }
        origin = new Vector2(rect.rect.xMin - jumpDistance, rect.rect.yMin);
        hit = Physics2D.Raycast(origin, origin - Vector2.up);
        if (hit.rigidbody != null)
        {
            if (hit.distance > 0)
            {
                return true;
            }
        }
        return false;
    }

    bool CanJump()
    {
        Vector2 v = new Vector2(move.body.velocity.x, 10);
        float g = Physics2D.gravity.magnitude;
        float velocity = v.magnitude;
        float angleDeg = Vector2.Angle(new Vector2(v.x, 0), v);
        float angleRad = angleDeg * Mathf.Deg2Rad;
        float distance = Mathf.Pow(velocity, 2) * Mathf.Sin(2 * angleRad) / g; //Максимальное расстояние, на которое возможен прыжок ПО ПРЯМОЙ

        Vector2 origin = new Vector2(rect.rect.xMax + jumpDistance, rect.rect.yMin);
        RaycastHit2D hit = Physics2D.CircleCast(origin, rect.rect.height/2, transform.forward);
        if (hit.rigidbody != null && hit.rigidbody != player)
        {
            if (hit.distance <= distance)
            {
                return true;
            }
        }
        return false;
    }
}

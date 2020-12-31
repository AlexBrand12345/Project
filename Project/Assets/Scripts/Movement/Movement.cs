using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float moveInput;


    public float speed;
    public bool isGrounded;
    public float checkRadius;
    public LayerMask ground;
    //public Transform checkPosition;

    Rigidbody2D body;
    
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(gameObject.transform.position, checkRadius, ground);
        moveInput = Input.GetAxis("Horizontal");
        if (!isGrounded) 
        {
            //такая-то анимация
            body.velocity = new Vector2(body.velocity.x, body.velocity.y);
        }
        //if (isGrounded)
        else
        {
            body.velocity = new Vector2(moveInput * speed, 0);
            //if (moveInput != 0)
            //{
            //    body.velocity = new Vector2(moveInput * speed, 0);
            //    // анимации
            //}
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Debug.Log("батрачит");
        {
            body.AddForce(transform.up*10, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            body.AddForce(transform.up * 2, ForceMode2D.Force);
            //анимация ранца
        }
    }
}

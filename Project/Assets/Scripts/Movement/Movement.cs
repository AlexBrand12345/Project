using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    public bool isForward;
    public float checkRadius;
    public LayerMask ground;

    public Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    protected void Move(float moveInput)
    {
        if (isGrounded)
        {
            Go(moveInput);
        }
        else Fall();
    }

    protected void Go(float moveInput)
    {
        if(isForward) body.velocity = new Vector2(moveInput * speed, 0);
        else body.velocity = new Vector2(moveInput * speed/2, 0);
    }
    protected void Fall()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y);
    }
    protected void Jump()
    {
        if (isGrounded) body.velocity = new Vector2(body.velocity.x, 10);
        else body.velocity = new Vector2(body.velocity.x, 2);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public bool isGrounded;
    public bool isForward;
    public float checkRadius;
    public LayerMask ground;

    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {        
        body = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, ground);
    }
    public void Move(float moveInput)
    {
        if (isGrounded)
        {
            Go(moveInput);
        }
        else Fall();
    }

    public void Go(float moveInput)
    {
        if(isForward) body.velocity = new Vector2(moveInput * speed, 0);
        else body.velocity = new Vector2(moveInput * speed/2, 0);
    }
    public void Fall()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y);
    }
    public void Jump()
    {
        if (isGrounded) body.velocity = new Vector2(body.velocity.x, 10);
        else body.velocity = new Vector2(body.velocity.x, 2);
    }
    
}

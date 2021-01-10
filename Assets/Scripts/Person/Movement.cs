using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    int layerMask;
    public float speed;
    public bool isGrounded;
    public bool isForward;
    public float checkRadius = 0.4f;
    public LayerMask ground;

    public Rigidbody2D body;
    protected RectTransform rect;

    protected void Awake()
    {
        layerMask = LayerMask.NameToLayer("Ground");
        layerMask = (1 << layerMask);
        rect = GetComponent<RectTransform>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        isGrounded = Physics2D.OverlapCircle(rect.position - new Vector3(0, rect.rect.height/2), checkRadius, ground);
        //RaycastHit hit;
        //isGrounded = Physics.Raycast(rect.position, Vector2.down, out hit, checkRadius, layerMask);
        //Debug.DrawRay(rect.position, Vector2.down * 100, Color.yellow);
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

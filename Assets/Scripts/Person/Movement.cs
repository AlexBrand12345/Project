using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Header("Movement")]
    bool isMoving;
    public float speed;
    public bool isGrounded;
    public bool isForward;
    public LayerMask ground;

    public Rigidbody2D body;
    protected RectTransform rect;
    private float checkRadius;
    Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("curSkin", MainSave.save.curSkin);
        ground = LayerMask.GetMask("Ground");
        //layerMask = (1 << layerMask);
        rect = GetComponent<RectTransform>();
        body = gameObject.GetComponent<Rigidbody2D>();
        checkRadius = rect.rect.height/2 + 0.11f ;
    }

    protected virtual void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(rect.position - new Vector3(0, rect.rect.height/2), checkRadius, ground);
        //RaycastHit hit;
        isGrounded = Physics2D.Raycast(rect.position, Vector2.down, checkRadius, ground);
        animator.SetBool("isGrounded", isGrounded);
#if DEBUG_MODE
        Debug.DrawRay(rect.position, Vector2.down * checkRadius, Color.yellow, 2);
#endif
    }

    protected void Move(float moveInput)
    {
        //if (moveInput != 0) isMoving = true;
        if (body.velocity.x == 0 || moveInput == 0) isMoving = false;
        else isMoving = true;
        animator.SetBool("isMoving", isMoving);
        Go(moveInput);
        //if (isGrounded)
        //{
        //    Go(moveInput);
        //}
        //else Fall();
    }

    protected void Go(float moveInput)
    {
        if(isForward) body.velocity = new Vector2(moveInput * speed, body.velocity.y);
        else body.velocity = new Vector2(moveInput * speed/2, body.velocity.y);
    }
    protected void Fall()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y);
    }
    protected void Jump()
    {
        if (isGrounded) body.velocity = new Vector2(body.velocity.x, 10);
        else body.velocity = new Vector2(body.velocity.x, 5);
    }
    
}

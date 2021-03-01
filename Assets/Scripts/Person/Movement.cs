using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Movement : MonoBehaviour
{
    [Header("Movement")]
    bool isMoving;
    public float speed;
    public bool isGrounded;
    public bool isForward;
    public LayerMask ground;
    [SerializeField] protected bool isFlying;
    [SerializeField] bool infiniteFuel;
    [SerializeField] float fuel;
    [SerializeField] float actualFuel;
    [SerializeField] float time2refuel;
    [SerializeField] bool mayFLy;

    public Rigidbody2D body;
    protected RectTransform rect;
    private float checkRadius;
    Animator playerAnimator;
    [SerializeField] protected Animator jetAnimator;
    Slider jetSlider;

    protected void Awake()
    {
        jetSlider = transform.GetComponentInChildren<Slider>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetInteger("curSkin", MainSave.save.curSkin);
        ground = LayerMask.GetMask("Ground");
        //layerMask = (1 << layerMask);
        rect = GetComponent<RectTransform>();
        body = gameObject.GetComponent<Rigidbody2D>();
        checkRadius = (rect.rect.height / 2)*rect.localScale.y  + rect.rect.height / 20;
        actualFuel = fuel;
    }

    protected virtual void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(rect.position - new Vector3(0, rect.rect.height/2), checkRadius, ground);
        //RaycastHit hit;
        isGrounded = Physics2D.Raycast(rect.position, Vector2.down, checkRadius, ground);
        playerAnimator.SetBool("isGrounded", isGrounded);
#if DEBUG_MODE
        //Debug.DrawRay(rect.position, Vector2.down * checkRadius, Color.yellow, 2);
#endif
        JetFly(isFlying);
    }
    void JetFly(bool isFlying)
    {
        jetAnimator.SetBool("isFlying", isFlying);
        if (!infiniteFuel)
        {
            if (isFlying)
            {
                if (actualFuel > 0f) 
                {
                    actualFuel -= 0.05f;
                    if (actualFuel <= (fuel / 20f)) StartCoroutine(Refuel());
                }                   
                else
                {
                    Debug.Log("pidor");
                    actualFuel = 0f;
                }
            }
            else
            {
                if (actualFuel >= fuel)
                    actualFuel = fuel;
                else actualFuel += 0.02f;
            }
            //if(actualFuel == 0f)
            jetSlider.value = actualFuel / fuel;
        }
        else jetSlider.value = 1f;
    }
    IEnumerator Refuel()
    {
        mayFLy = false;
        Debug.Log("refuel");
        yield return new WaitForSeconds(time2refuel);
        mayFLy = true;
    }

    protected void Move(float moveInput)
    {
        //if (moveInput != 0) isMoving = true;
        if (body.velocity.x == 0 || moveInput == 0) isMoving = false;
        else isMoving = true;
        playerAnimator.SetBool("isMoving", isMoving);
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
        else 
        {
            if(!isFlying && mayFLy) isFlying = true;
            if (isFlying && mayFLy)
            {           
                body.velocity = new Vector2(body.velocity.x, 16);
            }
            else if (!mayFLy) isFlying = false;           
        }
        
    }
    
}

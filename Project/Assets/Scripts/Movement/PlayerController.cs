using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveInput;
    Movement move;
    
    void Start()
    {    
        move = GetComponent<Movement>();
    }

    
    void Update()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
        move.Move(moveInput);

        if (Input.GetKey(KeyCode.Space))        
        {
            move.Jump();
        }
    }
}

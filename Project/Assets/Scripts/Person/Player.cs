using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    float moveInput;
    
    new void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        Move(moveInput);

        if (Input.GetKey(KeyCode.Space))        
        {
            Jump();
        }
    }
}

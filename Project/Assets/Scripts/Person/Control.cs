using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    float moveInput;
    Movement move;
    
    void Start()
    {    
        move = GetComponent<Movement>();
    }

    
    void Update()
    {
        
        moveInput = Input.GetAxis("Horizontal");
        move.Move(moveInput);

        if (Input.GetKey(KeyCode.Space))        
        {
            move.Jump();
        }
    }
}

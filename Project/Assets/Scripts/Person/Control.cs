using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    float moveInput;
    Movement move;
    Hands hands;
    
    void Start()
    {    
        move = GetComponent<Movement>();
        hands = GetComponentInChildren<Hands>();
    }   
    void Update()
    {
        
        moveInput = Input.GetAxis("Horizontal");
        move.Move(moveInput);

        if (Input.GetKey(KeyCode.Space))        
        {
            move.Jump();
        }
        if (Input.GetMouseButtonDown(0)) hands.Shoot();
    }
}

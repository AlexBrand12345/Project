using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medkit : MonoBehaviour
{
    float time2change = 0.5f;
    //bool F1sTime = true;
    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0.5f;
        //StartCoroutine(ChangePhysics(time2change, true));
    }

    private void Update()
    {
        if (body.velocity.y <= -5f) body.gravityScale = -0.5f;
        else if (body.velocity.y >= 5f) body.gravityScale = 0.5f;
    }
    //IEnumerator ChangePhysics(float timeTochange, bool F1sTime)
    //{
    //    yield return new WaitForSeconds(timeTochange);
    //    body.gravityScale *= -1;
    //    timeTochange = 1f;
    //   //Debug.Log(timeTochange);
    //    StartCoroutine(ChangePhysics(timeTochange, false));
    //}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAnimation : MonoBehaviour {
    public GameObject shark;

    void Update(){
        float moveSpeed = 10;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
      
    }

    /*
    void Shark() { 
         
    */
}

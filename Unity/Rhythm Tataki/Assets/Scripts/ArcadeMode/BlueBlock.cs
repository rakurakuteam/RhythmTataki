using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlueBlock : MonoBehaviour
{
    public static BlueBlock instance;
    public static bool touch = false;
    private void Start()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            touch = true;
        }
    }

    
}
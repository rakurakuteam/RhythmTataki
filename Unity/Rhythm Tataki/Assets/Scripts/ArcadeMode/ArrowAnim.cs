﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnim : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.Play("Left");
        }
     
    }
}

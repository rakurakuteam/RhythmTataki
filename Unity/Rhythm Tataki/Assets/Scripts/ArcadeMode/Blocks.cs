using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public Rigidbody2D rb;
    //public int count;
    public static Blocks instance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Box")
        {

            rb.bodyType = RigidbodyType2D.Kinematic;

            Debug.Log("kinematic success");
        }


    }
}
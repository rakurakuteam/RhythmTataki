using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Blocks")
        {
            collision.collider.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (collision.collider.tag == "Box")
        {
           
            collision.collider.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    
    }
}

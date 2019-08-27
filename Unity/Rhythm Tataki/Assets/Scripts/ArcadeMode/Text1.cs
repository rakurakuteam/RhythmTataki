using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Text1 : MonoBehaviour {
    //Rigidbody2D rigid;
   

    // Use this for initialization
    void Start () {
        float moveSpeed = 20;
        Rigidbody2D ready = gameObject.GetComponent<Rigidbody2D>();
        ready.velocity = new Vector2(moveSpeed, 0);

        Invoke("InvokeTest", 0.56f);
        Invoke("InvokeTest2", 1.5f);

    }

    void InvokeTest()
    {
        float moveSpeed = 0;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    void InvokeTest2()
    {
        float moveSpeed = 20;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject,1);
    }

    // Update is called once per frame
    void Update () {


	}
}

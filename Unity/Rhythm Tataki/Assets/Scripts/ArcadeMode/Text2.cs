using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text2 : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Invoke("InvokeTest", 2.2f);
        Invoke("InvokeTest2", 2.78f);
        Invoke("InvokeTest3", 3.2f);
        //Invoke("InvokeTest4", 3.7f);
        

    }

    void InvokeTest()
    {
         float moveSpeed = 20;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    void InvokeTest2()
    {
        float moveSpeed = 0;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);
    }
  
    void InvokeTest3()
    {
        float moveSpeed = 20;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(moveSpeed, 0);
    }
    /*
    void InvokeTest4()
    {
        backImg.SetActive(false);
    }
    */
    void OnBecameInvisible()
    {
        Destroy(gameObject, 1);
    }
    // Update is called once per frame
    void Update () {
		
	    }
}

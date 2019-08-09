using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : MonoBehaviour
{

    public float bgMoveSpeed = 4.0f;
    //public GameObject Obj;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-bgMoveSpeed * Time.deltaTime, -bgMoveSpeed * Time.deltaTime, 0);
        //transform.Translate(-0.1f*, -0.1f, 0);
        if (transform.localPosition.x < -23.0f)
        {
            transform.localPosition = new Vector3(-1.3f, 1.3f, 0);
        }
    }
    /*
    if (HeartTest.instance.checkHeart == true)
    {
        Debug.Log("끝");
        //Instantiate(gameObject);
        HM();
        HeartTest.instance.checkHeart = false;
    }
}

void HM()
{
    Obj = Instantiate(gameObject);
    gameObject.transform.position = new Vector3(0, 13, 0);
}

*/
}
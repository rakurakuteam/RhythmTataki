using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Blocks : MonoBehaviour
{

    public bool inTrigger = false;
    public bool inTrigger2 = false;
    public bool inTrigger4 = false;
    int height;
    public int height2;
    public static Blocks instance;
    GameObject box;


    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, 90);
        //Debug.Log(transform.position);

    }
    void Start()
    {
        DOTween.Init();
        box = GameObject.Find("box");
  
    }
  

    private void Awake()
    {
        instance = this;
        
    }

    private void FixedUpdate()
    {
        if (inTrigger == true)
        {
             if (inTrigger2 == true)
             {
                //height2 += 10;
                inTrigger2 = false;
                Debug.Log("시발라ㅣㅇ무리ㅏㅁ우리ㅏㅁ우라");
                height += 5;
                Camera.main.transform.DOMove(new Vector2(0, height),1);
                height2 += 10;
                Swipe.instance.perfect.transform.DOMove( new Vector2(0, height2),0);
                //Debug.Log(height2);
            }
            
        }
        inTrigger = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pink") { 
             inTrigger = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Block") { 
               inTrigger2 = true;
        }
    }

  

}

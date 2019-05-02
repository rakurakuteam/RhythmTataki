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
    public static int height2;
    public static Blocks instance;
    GameObject box;


    // Update is called once per frame

    
    void Start()
    {
        DOTween.Init();
        box = GameObject.Find("box");
  
    }
  

    private void Awake()
    {
        instance = this;
        
    }

    private void Update()
    {
        if (inTrigger == true)
        {
             if (inTrigger2 == true)                      
             {
                inTrigger2 = false;
                Debug.Log("pink on");
                height += 5;
                Camera.main.transform.DOMove(new Vector2(0, height), 1);
                Debug.Log("camera move");
                /*
                height += 50;
                Camera.main.transform.DOMove(new Vector2(0, height),1);
                //box.transform.localPosition = new Vector2(0, height);
                Debug.Log(height);
                Debug.Log(transform.position.y);
               */
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Swipe : MonoBehaviour
{

    bool inTrigger = false;
    bool inTrigger2 = false;
    bool inTrigger3 = false;
    bool inTrigger4 = false;
    public GameObject PBlock;
    public GameObject GBlock;
    public GameObject MBlock;
    public GameObject canvas;
    int height;
    //int height2;
    //public GameObject perfect;
    public static Swipe instance;

    Transform Canvas;

    //public GameObject spectrum;


    private void Start()
    {
        DOTween.Init();
        instance = this;
        Canvas = canvas.transform;
    }


    private void FixedUpdate()
    {
        //PBlock.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            inTrigger = false;
            GameObject miss = Instantiate(MBlock);
            miss.transform.position = new Vector2(-0.5f, 5);
        }
        else if (inTrigger2 && Input.GetKeyDown(KeyCode.Space))
        {
            inTrigger2 = false;
            GameObject good = Instantiate(GBlock);
            good.transform.position = new Vector2(-1, 5);
        }
        else if(inTrigger3 && Input.GetKeyDown(KeyCode.Space))
        {
            inTrigger3 = false;
            GameObject perfect = Instantiate(PBlock);
            perfect.transform.SetParent(Canvas,false);
            perfect.transform.localPosition = new Vector3(0, 541,0);
            Debug.Log(perfect.transform.position.y);
            Debug.Log(perfect.transform.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MissBox")
        {
            inTrigger = true;
            inTrigger2 = false;
            inTrigger3 = false;
        }else if (collision.gameObject.tag == "GoodBox")
        {
            inTrigger2 = true;
            inTrigger = false;
            inTrigger3 = false;
        }else if(collision.gameObject.tag == "PerfectBox")
        {
            inTrigger3 = true;
            inTrigger = false;
            inTrigger2 = false;
        }
    }
}

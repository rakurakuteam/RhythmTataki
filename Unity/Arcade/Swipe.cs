using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Swipe : MonoBehaviour
{

    bool inTrigger = false;
    bool inTrigger2 = false;
    bool inTrigger3 = false;
    public bool inTrigger5 = false;
    public GameObject PBlock;
    public GameObject GBlock;
    public GameObject MBlock;
    public GameObject canvas;
   public float height;
    float height2;
    public float height3;
    public int count;
    float cameraCount = 0;
    public static Swipe instance;
    Transform Canvas;
    public GameObject Canvas2;
    public GameObject stick;
    GameObject Cameras;
    GameObject perfect;
    GameObject miss;
    GameObject good;
    public bool check;
    public bool check2=false;
    private void Start()
    {

        //stick.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        DOTween.Init();
        instance = this;
        Canvas = canvas.transform;
        //Canvas.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, 0);
       
        Cameras = GameObject.FindWithTag("Mini");


    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inTrigger)
            {
                inTrigger = false;
                check = true;
                MakeBlock.instance.M();
                count += 1;


                //Debug.Log(count);
                //Debug.Log(miss.transform.localPosition);
            }
            else if (inTrigger2)
            {
                inTrigger2 = false;
                check = true;
                MakeBlock.instance.G();
                count += 1;

                //Box.instance.count2 -= 1;
                //Debug.Log(count);
                //Debug.Log(good.transform.localPosition);

            }
            else if (inTrigger3)
            {
                inTrigger3 = false;
                check = true;
                MakeBlock.instance.P();

                    //perfect = Instantiate(PBlock);
                    
                    //perfect.transform.localPosition = new Vector3(0, 541, 0);
                    count += 1;
                
                //Debug.Log(count);
                //Debug.Log(perfect.transform.localPosition);
            }
            GGGG();
        }
    }

    void GGGG()
    {

        if (count % 5 == 0 && count != 1 && count != 0 && !inTrigger5)
        {
            StartCoroutine(WaitForIt());
        }


    }

    IEnumerator WaitForIt()
    {
        inTrigger5 = true;
        yield return new WaitForSeconds(1.0f);
        inTrigger5 = false;
        cameraCount += 1;
       

        Debug.Log(cameraCount + "카메라카운트");
        if (cameraCount >= 2 && cameraCount <= 3)
        {
            height += 6.5f;
            height2 += 6f;
            check2 = true;
            height3 += 4;
            Box.instance.count2 -= 1;

        }
        else if(cameraCount<=1)
        {
            height += 5;
            height2 += 2.2f;
            check2 = true;
            height3 += 7;
            Box.instance.count2 -= 1;

        }else if (cameraCount >= 4)
        {
            height += 5;
            height2 += 2.2f;
            check2 = true;
            height3 += 10;
            Box.instance.count2 -= 1;
        }


        Camera.main.transform.DOMove(new Vector3(0, height, 0), 1);
        Cameras.transform.DOMove(new Vector3(0, height2, 0), 1);

        //Camera.main.transform.position = Canvas2.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MissBox")
        {
            inTrigger = true;
            inTrigger2 = false;
            inTrigger3 = false;
        }
        else if (collision.gameObject.tag == "GoodBox")
        {
            inTrigger2 = true;
            inTrigger = false;
            inTrigger3 = false;
        }
        else if (collision.gameObject.tag == "PerfectBox")
        {
            inTrigger3 = true;
            inTrigger = false;
            inTrigger2 = false;
        }
    }
}
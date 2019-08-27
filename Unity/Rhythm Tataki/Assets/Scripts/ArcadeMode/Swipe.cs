using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Swipe : MonoBehaviour
{
    bool inTrigger = false;
    bool inTrigger2 = false;
    bool inTrigger3 = false;
    public bool inTrigger5 = false;
    bool inTrigger4 = false;
    bool inTrigger6 = false;
    public GameObject PBlock;
    public GameObject GBlock;
    public GameObject MBlock;
    public GameObject canvas;
    public GameObject White;
    public float height;
    float height2;
    public float height3;
    public int count;
    float cameraCount = 0;
    public static Swipe instance;
    Transform Canvas;
    public GameObject Canvas2;
    public GameObject stick;
    public Animator sticks;
    GameObject Cameras;
    GameObject perfect;
    public bool check;
    public bool check2 = false;
    public GameObject ReadyImg;
    public GameObject StartImg;
    public bool on;

    private string input; // 드럼 값

    private void Start()
    {

        DOTween.Init();
        instance = this;
        Canvas = canvas.transform;
        Camera.main.transform.position = new Vector3(0, 0, 0);
        Cameras = GameObject.FindWithTag("Mini");
        //Invoke("Startmae",0f);
        Invoke("Ready", 2f);
        Invoke("Go", 2.1f);
        Invoke("Go2", 2.5f);
        Invoke("Move", 2.6f);

    }

    void Ready()
    {
        Destroy(ReadyImg);

    }

    void Go()
    {
        StartImg.SetActive(true);
    }

    void Go2()
    {
        Destroy(StartImg);
        Destroy(White);

    }

    void Move()
    {
        sticks = GetComponent<Animator>();
        sticks.enabled = true;
        on = false;
    }

    private void Awake()
    {
        sticks = GetComponent<Animator>();
        sticks.enabled = false;
        on = true;
        //Physics.gravity = new Vector3(0, 0F, 0);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }


    }
    private void Update()
    {
        if (SerialManager.instance.serial.IsOpen)
        {
            try
            {
                input = SerialManager.instance.serial.ReadLine();
                if (input != null)
                {
                    if (inTrigger)
                    {
                        inTrigger = false;
                        check = true;
                        MakeBlock.instance.M();
                        count += 1;
                    }
                    else if (inTrigger2)
                    {
                        inTrigger2 = false;
                        check = true;
                        MakeBlock.instance.G();
                        count += 1;
                    }
                    else if (inTrigger3)
                    {
                        inTrigger3 = false;
                        check = true;
                        MakeBlock.instance.P();
                        count += 1;
                    }
                    else if (inTrigger4)
                    {
                        inTrigger4 = false;
                        check = true;
                        MakeBlock.instance.M1();
                        count += 1;
                    }
                    else if (inTrigger6)
                    {
                        inTrigger6 = false;
                        check = true;
                        MakeBlock.instance.G1();
                        count += 1;
                    }
                    GGGG();
                } 
            }
            catch (TimeoutException e)
            {
                // Debug.Log("serial Error: " + e.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inTrigger)
            {
                inTrigger = false;
                check = true;
                MakeBlock.instance.M();
                count += 1;
            }
            else if (inTrigger2)
            {
                inTrigger2 = false;
                check = true;
                MakeBlock.instance.G();
                count += 1;
            }
            else if (inTrigger3)
            {
                inTrigger3 = false;
                check = true;
                MakeBlock.instance.P();
                count += 1;
            }
            else if (inTrigger4)
            {
                inTrigger4 = false;
                check = true;
                MakeBlock.instance.M1();
                count += 1;
            }
            else if (inTrigger6)
            {
                inTrigger6 = false;
                check = true;
                MakeBlock.instance.G1();
                count += 1;
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
        else if (cameraCount <= 1)
        {
            height += 5;
            height2 += 2.2f;
            check2 = true;
            height3 += 7;
            Box.instance.count2 -= 1;

        }
        else if (cameraCount >= 4)
        {
            height += 5;
            height2 += 2.2f;
            check2 = true;
            height3 += 10;
            Box.instance.count2 -= 1;
        }
        Camera.main.transform.DOMove(new Vector3(0, height, 0), 1);
        Cameras.transform.DOMove(new Vector3(0, height2, 23), 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MissBox")
        {
            inTrigger = true;
            inTrigger2 = false;
            inTrigger3 = false;
            inTrigger4 = false;
            inTrigger6 = false;
        }
        else if (collision.gameObject.tag == "GoodBox")
        {
            inTrigger2 = true;
            inTrigger = false;
            inTrigger3 = false;
            inTrigger4 = false;
            inTrigger6 = false;
        }
        else if (collision.gameObject.tag == "PerfectBox")
        {
            inTrigger3 = true;
            inTrigger = false;
            inTrigger2 = false;
            inTrigger4 = false;
            inTrigger6 = false;
        }
        else if (collision.gameObject.tag == "MissBox2")
        {
            inTrigger = false;
            inTrigger2 = false;
            inTrigger3 = false;
            inTrigger4 = true;
            inTrigger6 = false;
        }
        else if (collision.gameObject.tag == "GoodBox2")
        {
            inTrigger = false;
            inTrigger2 = false;
            inTrigger3 = false;
            inTrigger4 = false;
            inTrigger6 = true;
        }


    }
}
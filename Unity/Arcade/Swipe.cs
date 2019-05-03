using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Swipe : MonoBehaviour
{

    bool inTrigger = false;
    bool inTrigger2 = false;
    bool inTrigger3 = false;
    //bool inTrigger4 = false;
    public bool inTrigger5 = false;
    public GameObject PBlock;
    public GameObject GBlock;
    public GameObject MBlock;
    public GameObject canvas;
    float height;
    public int count;
    float cameraCount = 0;
    //int height2;
    //public GameObject perfect;
    public static Swipe instance;
    GameObject block;
    Transform Canvas;

    public bool spawnActive;
    //public GameObject spectrum;


    private void Start()
    {
        DOTween.Init();
        instance = this;
        Canvas = canvas.transform;
      

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            if (inTrigger)
            {
                inTrigger = false;
                GameObject miss = Instantiate(MBlock);
                miss.transform.SetParent(Canvas, false);
                miss.transform.localPosition = new Vector3(0, 541, 0);
                count += 1;
                Debug.Log(count+ "블럭카운트");
            }
            else if (inTrigger2)
            {
                inTrigger2 = false;
                GameObject good = Instantiate(GBlock);
                good.transform.SetParent(Canvas,false);
                good.transform.localPosition = new Vector3(0, 541, 0);
                count += 1;
                Debug.Log(count+"블럭카운트");
            }
            else if (inTrigger3)
            {
                inTrigger3 = false;
                GameObject perfect = Instantiate(PBlock);
                perfect.transform.SetParent(Canvas, false);
                perfect.transform.localPosition = new Vector3(0, 541, 0);
                count += 1;
                Debug.Log(count+"블럭카운트");
            }
            GGGG();
        }
    }

    void GGGG() {

        if (count % 5 == 0 && count != 1 && count != 0&&!inTrigger5)
        {
            StartCoroutine(WaitForIt());
            //inTrigger5 = true;
        }
            //Debug.Log("띠용?");
            //Debug.Log(transform.localPosition);
        

    }
    IEnumerator WaitForIt()
    {
        inTrigger5 = true;
            yield return new WaitForSeconds(1.0f);
        inTrigger5 = false;
        cameraCount += 1;

    
        Debug.Log(cameraCount+"카메라카운트");
        if (cameraCount > 1)
        {
            height += 6.5f;
        }
        else
        {
            height += 5;
        }
        Camera.main.transform.DOMove(new Vector2(0, height), 1);
            //Debug.Log("??");
        
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

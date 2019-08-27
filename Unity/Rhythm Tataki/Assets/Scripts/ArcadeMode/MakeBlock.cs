using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBlock : MonoBehaviour
{
    public GameObject PBlock;
    GameObject perfect;
    public GameObject MBlock;
    GameObject miss;
    public GameObject GBlock;
    GameObject good;
    public static MakeBlock instance;
    public GameObject ccc;
    Transform Canvas2;
    int height;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Canvas2 = ccc.transform;
        Canvas2.transform.position = new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M()
    {
        if (Swipe.instance.check == true)
        {
            miss = Instantiate(MBlock);
            miss.transform.SetParent(Canvas2, false);
            miss.transform.localPosition = new Vector3(-50, 600, 0);
            if (Swipe.instance.check2 == true)
            {
                //height += 200;
                //transform.position = new Vector3(0, height, 0);
                miss.transform.position = new Vector3(miss.transform.position.x, transform.position.y + Swipe.instance.height3, 90);
                //Swipe.instance.check2 = false;
            }
        }
    }

    public void M1()
    {
        if (Swipe.instance.check == true)
        {
            miss = Instantiate(MBlock);
            miss.transform.SetParent(Canvas2, false);
            miss.transform.localPosition = new Vector3(50, 600, 0);
            if (Swipe.instance.check2 == true)
            {
                //height += 200;
                //transform.position = new Vector3(0, height, 0);
                miss.transform.position = new Vector3(miss.transform.position.x, transform.position.y + Swipe.instance.height3, 90);
                //Swipe.instance.check2 = false;
            }
        }
    }


    public void G()
    {
        if (Swipe.instance.check == true)
        {
            good = Instantiate(GBlock);
            good.transform.SetParent(Canvas2, false);
            good.transform.localPosition = new Vector3(-30, 600, 0);

            if (Swipe.instance.check2 == true)
            {

                good.transform.position = new Vector3(good.transform.position.x, transform.position.y + Swipe.instance.height3, 90);
                //Swipe.instance.check2 = false;
            }
        }
    }

    public void G1()
    {
        if (Swipe.instance.check == true)
        {
            miss = Instantiate(GBlock);
            miss.transform.SetParent(Canvas2, false);
            miss.transform.localPosition = new Vector3(30, 600, 0);
            if (Swipe.instance.check2 == true)
            {
                //height += 200;
                //transform.position = new Vector3(0, height, 0);
                miss.transform.position = new Vector3(good.transform.position.x, transform.position.y + Swipe.instance.height3, 90);
                //Swipe.instance.check2 = false;
            }
        }
    }

    public void P()
    {
        if (Swipe.instance.check == true)
        {
            perfect = Instantiate(PBlock);
            perfect.transform.SetParent(Canvas2, false);
            perfect.transform.localPosition = new Vector3(0, 600, 0);
            if (Swipe.instance.check2 == true)
            {
                // height += 200;
                //transform.position = new Vector3(0, height, 0);
                perfect.transform.position = new Vector3(0, transform.position.y + Swipe.instance.height3, 90);
                //Swipe.instance.check2 = false;
            }
        }
    }
}

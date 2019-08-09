using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    bool blueOn = false;
    bool whiteOn = false;
    GameObject cube;
    public GameObject Perfect;
    public GameObject Good;
    public GameObject Miss;
    string input;
    public GameObject Leffect;
    public GameObject Reffect;
    public GameObject X;
    public GameObject xx;
    public static Trigger instance;
    public GameObject starEffect;
    public GameObject starEffect2;
    public GameObject starEffect3;

    private float TimeLeft = 4.0f;
    private float nextTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.FindWithTag("Lower");
        Debug.Log("cube");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextTime)
        {
            nextTime = Time.time + TimeLeft;
            Test();
        }


        if (LifeSystem.instance.dead == true)
        {
            Time.timeScale = 0;
           
        }

        else if (SerialManager.instance.serial.IsOpen)
        {
            try
            {
                input = SerialManager.instance.serial.ReadLine();

                if (blueOn == true)
                {
                    if (input.Equals("1"))
                    {
                        GameObject ef = Instantiate(Leffect);
                        //ef.GetComponent<ParticleSystem>().Play();
                        Destroy(ef, 0.5f);
                        //Debug.Log("blue+left");
                        GameObject[] objects = GameObject.FindGameObjectsWithTag("Blue");

                        for (int i = 0; i < objects.Length; i++)
                        {
                            Destroy(objects[0]);
                        }

                            
                        blueOn = false;
                        RandomBlocks.instance.Make();

                        //Debug.Log(blueOn);
                        //blueOn = false;

                        if (cube.transform.localScale.y > 0.5)
                        {
                            GameObject pObj = Instantiate(Perfect);
                            pObj.transform.position = new Vector2(-4, 0);
                            Destroy(pObj, 0.3f);
                            Score.instance.scoreValue += 20;

                        }
                        else if (cube.transform.localScale.y <= 0.5 && cube.transform.localScale.y >= 0.2)
                        {
                            GameObject GObj = Instantiate(Good);
                            GObj.transform.position = new Vector2(-4, 0);
                            Destroy(GObj, 0.3f);
                            Score.instance.scoreValue += 10;

                        }
                        else if (cube.transform.localScale.y < 0.2)
                        {
                            GameObject MObj = Instantiate(Miss);
                            MObj.transform.position = new Vector2(-4, 0);
                            Destroy(MObj, 0.3f);
                        }
                    }

                    else if (input.Equals("2"))
                    {
                        LifeSystem.health -= 1;
                        xx = Instantiate(X);
                        xx.SetActive(true);
                        xx.transform.position = new Vector2(0f, 0.1f);
                        Destroy(xx, 0.5f);
                        blueOn = true;
                        GameObject eff = Instantiate(Reffect);
                        //eff.GetComponent<ParticleSystem>().Play();
                        Destroy(eff, 0.3f);



                    }
                }
                else if (whiteOn == true)
                {
                    if (input.Equals("2"))
                    {
                        GameObject eff = Instantiate(Reffect);
                        //eff.GetComponent<ParticleSystem>().Play();
                        Destroy(eff, 0.3f);
                        //Debug.Log("blue+left");
                        GameObject[] objectss = GameObject.FindGameObjectsWithTag("White");
                        for (int i = 0; i < objectss.Length; i++)
                            Destroy(objectss[0]);
                        whiteOn = false;
                        RandomBlocks.instance.Make();

                        //whiteOn = false;
                        // Debug.Log(whiteOn);
                        //whiteOn = false;

                        if (cube.transform.localScale.y > 0.5)
                        {
                            GameObject pObj = Instantiate(Perfect);
                            pObj.transform.position = new Vector2(4, 0);
                            Destroy(pObj, 0.3f);
                            Score.instance.scoreValue += 20;
                        }
                        else if (cube.transform.localScale.y <= 0.5 && cube.transform.localScale.y >= 0.2)
                        {
                            GameObject GObj = Instantiate(Good);
                            GObj.transform.position = new Vector2(4, 0);
                            Destroy(GObj, 0.3f);
                            Score.instance.scoreValue += 10;
                        }
                        else if (cube.transform.localScale.y < 0.2)
                        {
                            GameObject MObj = Instantiate(Miss);
                            MObj.transform.position = new Vector2(4, 0);
                            Destroy(MObj, 0.3f);
                        }

                    }
                    else if (input.Equals("1"))
                    {
                        LifeSystem.health -= 1;
                        xx = Instantiate(X);
                        xx.SetActive(true);
                        xx.transform.position = new Vector2(0f, 0.1f);
                        Destroy(xx, 0.5f);
                        whiteOn = true;
                        GameObject ef = Instantiate(Leffect);
                        //ef.GetComponent<ParticleSystem>().Play();
                        Destroy(ef, 0.3f);
                    }
                }

            }
            catch (TimeoutException e)
            {

            }
        }


        //if (blueOn == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        //Debug.Log("blue+left");
        //        GameObject[] objects = GameObject.FindGameObjectsWithTag("Blue");

        //        for (int i = 0; i < objects.Length; i++)
        //            Destroy(objects[0]);
        //        blueOn = false;
        //        RandomBlocks.instance.Make();

        //        //Debug.Log(blueOn);
        //        //blueOn = false;

        //        if (cube.transform.localScale.y > 0.5)
        //        {
        //            GameObject pObj = Instantiate(Perfect);
        //            pObj.transform.position = new Vector2(-4, 0);
        //            Destroy(pObj, 0.6f);
        //            Score.instance.scoreValue += 20;

        //        }
        //        else if (cube.transform.localScale.y <= 0.5 && cube.transform.localScale.y >= 0.2)
        //        {
        //            GameObject GObj = Instantiate(Good);
        //            GObj.transform.position = new Vector2(-4, 0);
        //            Destroy(GObj, 0.6f);
        //            Score.instance.scoreValue += 10;

        //        }
        //        else if (cube.transform.localScale.y < 0.2)
        //        {
        //            GameObject MObj = Instantiate(Miss);
        //            MObj.transform.position = new Vector2(-4, 0);
        //            Destroy(MObj, 0.6f);
        //        }
        //    }

        //    else if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        LifeSystem.health -= 1;
        //        blueOn = true;

        //    }
        //}
        //else if (whiteOn == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        //Debug.Log("blue+left");
        //        GameObject[] objectss = GameObject.FindGameObjectsWithTag("White");
        //        for (int i = 0; i < objectss.Length; i++)
        //            Destroy(objectss[0]);
        //        whiteOn = false;
        //        RandomBlocks.instance.Make();

        //        //whiteOn = false;
        //        // Debug.Log(whiteOn);
        //        //whiteOn = false;

        //        if (cube.transform.localScale.y > 0.5)
        //        {
        //            GameObject pObj = Instantiate(Perfect);
        //            pObj.transform.position = new Vector2(4, 0);
        //            Destroy(pObj, 0.6f);
        //            Score.instance.scoreValue += 20;
        //        }else if (cube.transform.localScale.y <= 0.5 && cube.transform.localScale.y >= 0.2)
        //        {
        //            GameObject GObj = Instantiate(Good);
        //            GObj.transform.position = new Vector2(4, 0);
        //            Destroy(GObj, 0.6f);
        //            Score.instance.scoreValue += 10;
        //        }
        //        else if (cube.transform.localScale.y < 0.2)
        //        {
        //            GameObject MObj = Instantiate(Miss);
        //            MObj.transform.position = new Vector2(-4, 0);
        //            Destroy(MObj, 0.6f);
        //        }

        //    }
        //    else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        LifeSystem.health -= 1;
        //        whiteOn = true;


        //    }
        //}
    }
    void Test()
    {
        GameObject ss = Instantiate(starEffect);
        ss.GetComponent<ParticleSystem>().Play();
        Destroy(ss, 10.3f);

        GameObject ss3 = Instantiate(starEffect3);
        ss3.GetComponent<ParticleSystem>().Play();
        Destroy(ss3, 5.1f);
        Test2();
    }

    void Test2()
    {
        GameObject ss2 = Instantiate(starEffect2);
        ss2.GetComponent<ParticleSystem>().Play();
        Destroy(ss2, 4.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blue")
        {
            //Debug.Log("blue");
            blueOn = true;
            whiteOn = false;
        }
        else if (collision.gameObject.tag == "White")
        {
            //Debug.Log("white");
            whiteOn = true;
            blueOn = false;
        }
    }
}
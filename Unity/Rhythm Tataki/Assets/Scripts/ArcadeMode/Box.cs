using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int count2 = 0;
    static bool check;
    public static Box instance;
    public GameObject gameover;
    public bool MusicOff=false;
    private void Start()
    {
        check = false;
    }
    private void Awake()
    {
        instance = this;
        gameover.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Block") //박스에 블럭이 닿였다능 
        {
            count2 += 1;
            if (count2 >= 2)
            {
                Debug.Log("게임끝~!~~!!~!~!~");
                gameover.SetActive(true);
                Time.timeScale = 0;
                MusicOff = true;
            }
        }


    }
}
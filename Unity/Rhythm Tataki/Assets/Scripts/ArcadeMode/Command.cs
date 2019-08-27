using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
    int[] numarray;
    public Text rText;
    string numText = "";
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
        }
        */


        if (Input.GetKeyDown(KeyCode.Space))
        {
            numarray = new int[4];
            for (int i = 0; i < numarray.Length; i++)
            {
                numarray[i] = Random.Range(0, 2);
                numText += numarray[i].ToString() + " ";
            }
            rText.text = numText;

  
                numarray = null;
                numText = "";
            

        }
       
    }

}

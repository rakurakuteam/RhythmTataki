using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour {
    
    private SpriteRenderer theSR;
    public Sprite defaultImg;
    public Sprite pressedImg;

    public GameObject rightArrow;

    public SpriteRenderer right, left;

    GameObject canvasObj = GameObject.Find("Arrow");

    List<SpriteRenderer> arrowList = new List<SpriteRenderer>();
    

    int xPosition = 0;
    public KeyCode keyToPress;
	// Use this for initialization
	void Start () {

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(1, 2);
            if (randIndex == 1)
            {
                arrowList.Add(Instantiate<SpriteRenderer>(right, new Vector2(xPosition, 0), Quaternion.identity));
                arrowList[i].transform.parent = canvasObj.transform;
                xPosition += 20;
            }
            else {
                arrowList.Add(Instantiate<SpriteRenderer>(left, new Vector2(xPosition, 0), Quaternion.identity));
                arrowList[i].transform.parent = canvasObj.transform;
                xPosition += 20;
            }
        }

        theSR = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyToPress))
        {
            arrowList[0].GetComponent<SpriteRenderer>().sprite = pressedImg;
            theSR.sprite = pressedImg;
            //transform.localScale = new Vector2(40  , 40);
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImg;
           
        }
	}
   


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    static int score = 0;
    public Text scoreText;

    public void getScore(int value)
    {
        score += value;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "cloud")
        {
            getScore(15);
            scoreText.text = score.ToString();
        }
    }
  }
   

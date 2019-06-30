using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public int scoreValue = 0;
    public Text score;
    public static Score instance;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();

    }
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        score.text = ""+ scoreValue;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlock : MonoBehaviour
{
    public static WhiteBlock instance;
    public static bool Wtouch = false;
    private void Start()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            Wtouch = true;
        }
    }
}

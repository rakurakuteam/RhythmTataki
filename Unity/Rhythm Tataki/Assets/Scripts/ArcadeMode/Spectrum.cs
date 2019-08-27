using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public GameObject spectrum;

    void Update()
    {
        if (spectrum)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (spectrum.transform.localScale.y > 0.8)
                {
                    Debug.Log("하위");
                }
                else if (spectrum.transform.localScale.y > 0.3 && spectrum.transform.localScale.y < 0.8)
                {
                    Debug.Log("하위2");
                }
                else if (spectrum.transform.localScale.y < 0.3)
                {
                    Debug.Log("하위3");
                }
            }

        }
    }
}

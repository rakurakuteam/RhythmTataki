using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearts : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;
    float Offset;

    void Update()
    {
        Offset += Time.deltaTime * ScrollSpeed;
       // renderer.material.mainTextureOffset = new Vector2(Offset, 0);
    }

}

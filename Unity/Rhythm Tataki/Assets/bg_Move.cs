using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_Move : MonoBehaviour
{

    public float bgMoveSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-bgMoveSpeed * Time.deltaTime, -bgMoveSpeed * Time.deltaTime, 0);

        if (transform.localPosition.x < -500.0f) {
            transform.localPosition = new Vector3(62f, 62f, 0);
        }
    }
}

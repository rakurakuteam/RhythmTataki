using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGameR : MonoBehaviour {
    public Transform prefab;
    public GameObject canvas;
    public ParticleSystem star;
    // Use this for initialization
    public bool check = true;
    void Start()
    {
        check = false;
        StartCoroutine(WaitForIt());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow)&&check)
        {
            Instantiate(prefab, new Vector3(190, 76, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false);
            star.Play();
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(3.7f);
        check = true;
    }
}

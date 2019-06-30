using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGameL : MonoBehaviour
{

    public static FreeGameL instance;
    public Transform prefab;
    public Transform prefab2;
    public GameObject canvas;

    public ParticleSystem star;
    public ParticleSystem star2;

    public bool check = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        check = false;
        StartCoroutine(WaitForIt());
       
    }

    void Update()
    {
        if (check) {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
            
                Instantiate(prefab, new Vector3(-189, 76, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false);
                star.Play();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Instantiate(prefab2, new Vector3(190, 76, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false);
                star2.Play();
            }
        }

    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(3.7f);
        check = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    /*
    GameObject player;
	// Use this for initialization
	void Start () {
        this.player = GameObject.Find("Cat");
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            transform.position.x, playerPos.y, transform.position.z);
	}
    */
    Transform mCamera;
    void Start()
    {
        mCamera = transform.Find("MainCamera");
        mCamera.transform.LookAt(transform.position);
        mCamera.transform.RotateAround(transform.position, Vector3.up, transform.rotation.z);
    }
   

}

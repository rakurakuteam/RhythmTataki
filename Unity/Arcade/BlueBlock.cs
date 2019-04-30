using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBlock : MonoBehaviour
{

    bool inTrigger = false;
    
    public GameObject Good;
    public GameObject Perfect;
    public GameObject Miss;
    public GameObject spc;

    private void Awake()
    {
        spc = GameObject.FindWithTag("Lower");

    }

    private void FixedUpdate()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(-12, 0, 0);
            if (spc.transform.localScale.y > 0.6)
            {
                //Debug.Log("시발");
                GameObject pObj = Instantiate(Perfect);
                pObj.transform.position = new Vector2(-4, 0);
                Score.scoreValue += 20;
                Destroy(pObj, 0.5f);
                ScoreParticle.instance.LeftparticlePlay();
                BlockBreakPtc.instance.BreakBlock2();
            }
            else if (spc.transform.localScale.y > 0.1 && spc.transform.localScale.y < 0.6)
            {
                //Debug.Log("시발2");
                GameObject GObj = Instantiate(Good);
                GObj.transform.position = new Vector2(-4, 0);
                Score.scoreValue += 10;
                Destroy(GObj, 0.5f);
                ScoreParticle.instance.LeftparticlePlay();
                BlockBreakPtc.instance.BreakBlock2();
                
            }
            else if (spc.transform.localScale.y < 0.1)
            {
                //Debug.Log("시발3");
                GameObject MObj = Instantiate(Miss);
                MObj.transform.position = new Vector2(-4, 0);
                Destroy(MObj, 0.5f);
                BlockBreakPtc.instance.BreakBlock2();
            }
        }
        else if (inTrigger && Input.GetKeyDown(KeyCode.RightArrow))
        {
            LifeSystem.health -= 1;
        }


    }


    private void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.collider.tag == "Box")
        {
            inTrigger = false;
            RandomBlocks.instance.Make();
            Destroy(gameObject);
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            inTrigger = true;
        }
    }
}

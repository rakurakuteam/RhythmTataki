using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakPtc : MonoBehaviour
{
    public static BlockBreakPtc instance;
    public GameObject greyPtc;
    public GameObject bluePtc;
    private void Awake()
    {
        instance = this;        
    }

    public void BreakBlock()
    {
        GameObject ptc = Instantiate(greyPtc);
        ptc.GetComponent<ParticleSystem>().Play();
        Destroy(ptc,1);
    }

    public void BreakBlock2()
    {
        GameObject ptc2 = Instantiate(bluePtc);
        ptc2.GetComponent<ParticleSystem>().Play();
        Destroy(ptc2,1);
    }
}

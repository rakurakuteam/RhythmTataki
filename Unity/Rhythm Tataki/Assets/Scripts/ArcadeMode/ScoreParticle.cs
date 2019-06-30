using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreParticle : MonoBehaviour
{
    public GameObject Rshine;
    public GameObject Lshine;
    public static Animator animator;
    public static Animator animator2;
    public static ScoreParticle instance;
    // Start is called before the first frame update
    void Awake()
    {

        instance = this;
    }

    public void RightparticlePlay() {
        Transform Parent = GameObject.Find("Canvas").transform;

        GameObject pcs = Instantiate(Rshine);
        pcs.transform.SetParent(Parent, false);
        animator = pcs.GetComponent<Animator>();
        pcs.GetComponent<ParticleSystem>().Play();
        Destroy(pcs, 0.7f);

        animator.SetTrigger("anim");
        Destroy(animator, 0.7f);
    }

    public void LeftparticlePlay()
    {
        Transform Parent = GameObject.Find("Canvas").transform;

        GameObject pcs2 = Instantiate(Lshine);
        pcs2.transform.SetParent(Parent, false);
        animator2 = pcs2.GetComponent<Animator>();
        pcs2.GetComponent<ParticleSystem>().Play();
        Destroy(pcs2, 0.7f);
 

        animator2.SetTrigger("anim2");
        Destroy(animator2, 0.7f);
    }
}

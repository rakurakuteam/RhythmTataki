using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAudio : MonoBehaviour
{
    public AudioSource audios;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Music", 2.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeSystem.instance.dead == true)
        {
            audios.Stop();
        }
    }

    private void Awake()
    {
        audios.Stop();
    }

    void Music()
    {
        audios = GetComponent<AudioSource>();
        audios.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlocks : MonoBehaviour
{
    [SerializeField]
   private GameObject[] blocks;
    public static RandomBlocks instance;
    public GameObject ReadyImg;
    public GameObject StartImg;
    public GameObject BlackPanel;
    public AudioClip clip;
    public AudioSource audios;
   
    
    private bool check = true;
    void Ready()
    {
        Destroy(ReadyImg);
        
    }

    void Go()
    {
        StartImg.SetActive(true);

    }

    void Go2()
    {
        Destroy(StartImg);
        Destroy(BlackPanel);
    }

    void Awake()
    {
        //BlackPanel.SetActive(true);
        instance = this;
        audios.Stop();
        Make();
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void Start()
    {
        
        Make2();
        Make3();
        Make4();
        
        Invoke("Ready", 2f);
        Invoke("Go", 2.1f);
        Invoke("Go2", 2.5f);
        Invoke("Music", 1.5f);

    }

    private void Update()
    {
        if (LifeSystem.instance.dead == true)
        {
            audios.Stop();
        }
    }
    
    
    public void Make()
    {
            int index = Random.Range(0, blocks.Length);
            Instantiate(blocks[index], transform.position, Quaternion.identity);
    
    }
    public void Make2()
    {
        
            int index = Random.Range(0, blocks.Length);
            Instantiate(blocks[index], transform.position, Quaternion.identity);
        
    }
    public void Make3()
    {
       
            int index = Random.Range(0, blocks.Length);
            Instantiate(blocks[index], transform.position, Quaternion.identity);
        
    }
    public void Make4()
    {
       
            int index = Random.Range(0, blocks.Length);
            Instantiate(blocks[index], transform.position, Quaternion.identity);
        
    }
    
    void Music()
    {
         audios = GetComponent<AudioSource>();
         audios.Play();
    }
    

}



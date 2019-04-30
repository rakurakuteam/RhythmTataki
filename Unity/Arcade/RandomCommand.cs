using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RandomCommand : MonoBehaviour
{
    [SerializeField]
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject Xprefab;
    public GameObject ReadyImg;
    public GameObject StartImg;
    public GameObject canvas;
    public GameObject PausePanel;
    public GameObject NotePanel;
    public GameObject Cloud;
    public GameObject Cats;
    public Transform Cameras;
    public Sprite clickedLSprite;
    public Sprite clickedRSprite;
    public float DestroyTime = 1.0f;
    public static Animator animator;
    private bool check = true;
    private bool isPause = true;
    float height = 0;
    float height2 = 0;
    float length = 0;
    float mawari = 0;
    float Dist;


    List<GameObject> CommandList = new List<GameObject>();
    List<Vector2> CommandPosition = new List<Vector2>();

    GameObject[] temp = new GameObject[4];
    int indexNum = 0;

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
    }

    void Start()
    {
        Invoke("Ready", 2f);
        Invoke("Go", 2.1f);
        Invoke("Go2", 2.5f);

        check = false;
        StartCoroutine(WaitForIt());

        CommandPosition.Add(new Vector2(-464f, -3.67f));
        CommandPosition.Add(new Vector2(-140f, -3.67f));
        CommandPosition.Add(new Vector2(140f, -3.67f));
        CommandPosition.Add(new Vector2(464f, -3.67f));

        CommandInit();

        DOTween.Init();
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2.6f);
        check = true;
    }

    private void Update()
    {
        if (check)
        {
            if (indexNum == 4)
            {
                for (int i = 0; i < 4; i++)
                    Destroy(temp[i]);
                indexNum = 0;
                temp = new GameObject[4];
                CommandInit();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                if (CommandList[0].tag == "Left")
                {
                    CommandList[0].GetComponent<SpriteRenderer>().sprite = clickedLSprite;
                    temp[indexNum] = CommandList[0];
                    CommandList.RemoveAt(0);
                    indexNum++;
                    height = height + 2.0f;
                    length = length + -2f;
                    GameObject clouds = Instantiate(Cloud);
                    clouds.transform.position = new Vector2(length, height);
                    clouds.transform.parent = canvas.transform;

                    animator = Cats.GetComponent<Animator>();
                    animator.SetTrigger("anim");
                   // Destroy(animator, 0.5f);

                    mawari = mawari + -2f;
                    height2 = height + 1.5f;
                    //Cats.transform.position = new Vector2(mawari, height2);
                    Cats.transform.DOJump(new Vector2(mawari, height2),1f,1,1f);

                }
                else
                {
                    if (CommandList[0].tag == "Right")
                    {
                        LifeSystem.health -= 1;
                        GameObject wrongObj = Instantiate(Xprefab);
                        wrongObj.transform.position = CommandList[0].transform.position;
                        Destroy(wrongObj, DestroyTime);

                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (CommandList[0].tag == "Right")
                {
                    CommandList[0].GetComponent<SpriteRenderer>().sprite = clickedRSprite;
                    temp[indexNum] = CommandList[0];
                    CommandList.RemoveAt(0);
                    indexNum++;
                    height = height + 2.0f;
                    length = length + 2f;
                    GameObject clouds = Instantiate(Cloud);
                    clouds.transform.position = new Vector2(length, height);
                    clouds.transform.parent = canvas.transform;
                    //canvas.transform.position = Cameras.transform.position;
                    mawari = mawari + 2f;
                    height2 = height + 1.5f;
                    //Cats.transform.position = new Vector2(mawari, height2);   
                    animator = Cats.GetComponent<Animator>();
                    animator.SetTrigger("anim2");
                    Cats.transform.DOJump(new Vector2(mawari, height2), 1f, 1, 1f);
                
                }
                else
                {
                    if (CommandList[0].tag == "Left")
                    {
                        LifeSystem.health -= 1;
                        GameObject wrongObj = Instantiate(Xprefab);
                        wrongObj.transform.position = CommandList[0].transform.position;
                        Destroy(wrongObj, DestroyTime);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isPause)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                isPause = false;
            }
        }
    }

    void CommandInit()
    {
        for (int i = 0; i < 4; i++)
        {
            int randIndex = Random.Range(0, 2);
            if (randIndex == 1)
            {
                GameObject obj = Instantiate(LeftArrow);
                CommandList.Add(obj);
                obj.transform.parent = NotePanel.transform;
            }
            else
            {
                GameObject obj = Instantiate(RightArrow);
                CommandList.Add(obj);
                obj.transform.parent = NotePanel.transform;

            }

            CommandList[i].transform.localPosition = CommandPosition[i];
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(Cloud);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalGameManager : MonoBehaviour
{
    // 노트 떨어지는 속도
    // public float noteSpeed;

    // 화면에 보이는 스코어UI를 가져온다.
    public GameObject scoreUI;
    private float score;
    private Text scoreText;

    public GameObject comboUI;
    private int combo;
    private Text comboText;
    private Animator comboAnimator;

    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    //public GameObject[] trails;
    //private SpriteRenderer[] trailSpriteRenderes;

    /*
     * bad : 1
     * good : 2
     * perfect : 3
     * miss : 4
     * ENUM 자료형을 사용한다
     */
    public enum judes { NONE = 0, BAD, GOOD, PERFECT, MISS };

    // 음악변수
    //private AudioSource audioSource;
    //public string music = "1";

    // 음악실행 함수
    //void MusicStart()
    //{
    //    // 리소스에서 비트 음악 파일을 불러와 재생
    //    AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + music);
    //    audioSource = GetComponent<AudioSource>();
    //    audioSource.clip = audioClip;
    //    audioSource.Play();
    //}



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("실행");
        //Invoke("MusicStart", 2);
        // 초기화
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        // 판정 결과를 보여주는 스프라이트 이미지를 미리 초기화 합니다.
        judgeSprites = new Sprite[4];
        // 특정한 폴더에 있는 리소스를 Resources.Load를 이용해서 가지온다.
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        // 스프라이트 렌더러를 이용하여 스프라이트 이미지를 표시할 수 있음.
        // 스프라이트 렌더러 초기화
        // 트레일들을 index로 넣어줌
        //trailSpriteRenderes = new SpriteRenderer[trails.Length];
        //for (int i = 0; i < trails.Length; i++)
        //{
        //    trailSpriteRenderes[i] = trails[i].GetComponent<SpriteRenderer>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 입력한 키에 해당하는 라인을 빛나게 처리
        //if (Input.GetKey(KeyCode.LeftArrow)) ShineTrail(0);
        //if (Input.GetKey(KeyCode.RightArrow)) ShineTrail(1);

        //// 한번 빛나게 된 라인은 반복적으로 다시 어둡게 처리
        //for (int i = 0; i < trailSpriteRenderes.Length; i++)
        //{
        //    Color color = trailSpriteRenderes[i].color;
        //    // 매프레임마다 실행되므로 -= 을 사용
        //    color.a -= 0.01f;
        //    trailSpriteRenderes[i].color = color;
        //}
    }

    // 키를 입력받으면 라인을 빛나게 한다
    //public void ShineTrail(int index)
    //{
    //    Color color = trailSpriteRenderes[index].color;
    //    color.a = 0.32f;
    //    trailSpriteRenderes[index].color = color;
    //}

    // 노트 판정 이후에 판정 결과를 화면에 보여줍니다.
    void showJudgement()
    {
        // 점수 이미지를 보여준다
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);

        // 판정 이미지를 보여준다
        // 트리거를 지정해 놨으므로
        judgementSpriteAnimator.SetTrigger("Show");

        // 콤보가 2이상일 때만 콤보 이미지를 보여준다.
        if (combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
        //if (maxCombo < combo)
        //{
        //    // 현재의 최대콤보가 갱신된경우
        //    maxCombo = combo;
        //}
    }

    // 노트 판정을 진행
    public void processJudge(judes judge, int noteType)
    {
        if (judge == judes.NONE) return;
        // MISS 판정을 받은 경우 콤보 종료, 점수를 깍는다
        if (judge == judes.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }
        // BAD 판정을 받은 경우 콤보 종료, 점수 깍는다
        if (judge == judes.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 5) score -= 5;
        }

        // PERFECT 혹은 GOOD 판정 받은 경우
        else
        {
            if (judge == judes.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 20;
            }
            else if (judge == judes.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 15;
            }
            combo += 1;

            // 콤보가 많아지면 점수는 콤보에 비례하여 더욱 증가한다.
            score += (float)combo * 0.1f;
        }

        // 판정이후는 판정결과를 보여준다.
        showJudgement();
    }
}

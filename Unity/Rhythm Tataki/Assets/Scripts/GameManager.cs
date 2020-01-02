using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    // 싱글톤으로 처리
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // 화면에 보이는 스코어UI를 가져온다.
    public GameObject scoreUI;
    public float score;
    private Text scoreText;

    // 트럼펫 UI와 트럼펫 Effect UI
    public GameObject[] leftObjects;
    public GameObject[] rightObjects;
    private Animator[] leftAnimator;
    private Animator[] rightAnimator;
       
    // 화면의 실제로 적용될 COMBO UI를 가지고 온다.
    // 이 UI는 콤보가 증가할 떄 마다 숫자가 증가하여 표시해준다.
    public GameObject comboUI;
    private int combo;
    private Text comboCountText;
    private Animator comboAnimator;

    // 화면의 COMBO Text를 가지고 온다.
    // 이 UI는 COMBO 라는 글의 애니메이션만 계속 반복한다.
    public GameObject comboTextUI;
    private Animator comboTextAnimator;
    
    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;
    public int maxCombo;

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderes;

    int soundId1, soundId2;
    public bool IsPause;
    public GameObject pausePanel;
    public string input;

    Stopwatch sw = new Stopwatch();

    // 시리얼 통신
    //private SerialPort serial;
    //private string portName = "COM4";   // 포트이름
    //private int baudRate = 9600;        // 통신속도
    

    // public RecordManager recordManager;

    /*
     * bad : 1
     * good : 2
     * perfect : 3
     * miss : 4
     * ENUM 자료형을 사용한다
     */
    public enum judes { NONE = 0, BAD, GOOD, PERFECT, MISS };

    // 음악변수
    public AudioSource audioSource;

    // 드럼 소리 변수
    public AudioSource leftDrumSound;
    public AudioSource rigthDrumSound;

    // 자동 판정 모드
    public bool autoPerfect;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // 시리얼 포트 셋팅
        //serial = new SerialPort(portName, baudRate);
        //serial.ReadTimeout = 1;
        //serial.Open();

        // StartCoroutine(SerialRead());
        IsPause = false;
        // 패널숨김
        pausePanel.SetActive(false);

        // MusicStart함수 실행, 2초 후에
        Debug.Log("Game Start");
        Invoke("MusicStart", 2);
        // 초기화
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboCountText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();
        comboTextAnimator = comboTextUI.GetComponent<Animator>();

        // 애니메이터 초기화
        SetAnimator();
        
        // 노래의 제목과 제목 이미지 현재 랭크 보여줌.

        // 판정 결과를 보여주는 스프라이트 이미지를 미리 초기화 한다.
        judgeSprites = new Sprite[4];
        // 특정한 폴더에 있는 리소스를 Resources.Load를 이용해서 가지온다.
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        // 스프라이트 렌더러를 이용하여 스프라이트 이미지를 표시할 수 있음.
        // 스프라이트 렌더러 초기화
        // 트레일들을 index로 넣어줌
        trailSpriteRenderes = new SpriteRenderer[trails.Length];
        
        for(int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderes[i] = trails[i].GetComponent<SpriteRenderer>();
        }

        // 드럼 소리 셋팅
        leftDrumSound = gameObject.AddComponent<AudioSource>();
        rigthDrumSound = gameObject.AddComponent<AudioSource>();
        leftDrumSound.clip = PlayerInformation.leftDrumSound;
        rigthDrumSound.clip = PlayerInformation.rightDrumSound;

        // soundId1 = AudioCenter.loadSound("DrumSnare");
        // soundId2 = AudioCenter.loadSound("DrumKick");
    }


    // Update is called once per frame
    void Update()
    {
        // 사용자가 입력한 키에 해당하는 라인을 빛나게 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShineTrail(0);
            leftDrumSound.Play();
            PlayLeftAnimator();
            // AudioCenter.playSound(soundId1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShineTrail(1);
            rigthDrumSound.Play();
            PlayRightAnimator();
            // AudioCenter.playSound(soundId2);
        }
        

        // 한번 빛나게 된 라인은 반복적으로 다시 어둡게 처리
        for (int i = 0; i < trailSpriteRenderes.Length; i++)
        {
            Color color = trailSpriteRenderes[i].color;
            // 매프레임마다 실행되므로 -= 을 사용
            color.a -= 0.01f;
            trailSpriteRenderes[i].color = color;
        }

        if (SerialManager.instance.serial.IsOpen)
        {
            try
            {
                input = SerialManager.instance.serial.ReadLine();
                // Debug.Log(input);
                if (input.Equals("1"))
                {
                    ShineTrail(0);
                    // leftDrumSound.Play();
                }

                if (input.Equals("2"))
                {
                    ShineTrail(1);
                    // rigthDrumSound.Play();
                }
            }
            catch (TimeoutException e)
            {
                // Debug.Log("serial Error: " + e.ToString());
            }
        }

        //if (SerialManager.instance.serial.IsOpen)
        //{

        //    try
        //    {
        //        // ReadSerialValue();

        //        string input = SerialManager.instance.serial.ReadLine();
        //        sw.Start();

        //        if (input.Equals("1"))
        //        {
        //            ShineTrail(0);
        //            //Debug.Log("1");
        //            leftDrumSound.Play();
        //        }

        //        if (input.Equals("2"))
        //        {
        //            ShineTrail(1);
        //            //Debug.Log("2");
        //            rigthDrumSound.Play();
        //        }

        //        //sw.Stop();
        //        //Debug.Log("sw: " + sw.Elapsed.Milliseconds.ToString() + "ms");
        //        //sw.Reset();

        //    }
        //    catch (TimeoutException e)
        //    {
        //        // Debug.Log("serial Error: " + e.ToString());
        //    }

        //}

    }

    // 음악실행 함수
    void MusicStart()
    {
        Debug.Log("MusicStart");
        // Debug.Log(PlayerInformation.selectedMusic);
        audioSource = GetComponent<AudioSource>();
        // 리소스에서 비트 음악 파일을 불러와 재생
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);

        // Resources 폴더에서 찾지 못하면 persistancePath에서 찾는다.
        if (audioClip == null)
        {
            string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + PlayerInformation.selectedMusic + ".ogg";

            // 노래 찾는 함수 실행
            StartCoroutine(LoadAudioClip(path, audioClip));
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlayDrumSound(int noteType)
    {
        if(noteType == 0)
        {
            leftDrumSound.Play();
        }
        else if(noteType == 1)
        {
            rigthDrumSound.Play();
        }
    }

    // 왼쪽, 오른쪽 드럼 두드릴때의 애니메이션 초기화
    public void SetAnimator()
    {
        leftAnimator = new Animator[leftObjects.Length];
        rightAnimator = new Animator[rightObjects.Length];
        
        for(int i = 0; i < leftAnimator.Length; i++)
        {
            leftAnimator[i] = leftObjects[i].GetComponent<Animator>();
        }

        for (int i = 0; i < rightAnimator.Length; i++)
        {
            rightAnimator[i] = rightObjects[i].GetComponent<Animator>();
        }
    }

    public void PlayLeftAnimator()
    {
        for(int i = 0; i < leftAnimator.Length; i++)
        {
            leftAnimator[i].SetTrigger("Show");
        }
    }

    public void PlayRightAnimator()
    {
        for (int i = 0; i < rightAnimator.Length; i++)
        {
            rightAnimator[i].SetTrigger("Show");
        }
    }

    // 노래 파일을 찾는 코루틴
    IEnumerator LoadAudioClip(string path, AudioClip audioClip)
    {
        Debug.Log("Start LoadAudioClip");

        // 파일 존재 여부 확인
        if (!File.Exists(path))
        {
            Debug.Log("Didn't Exist: " + path);
            yield break;
        }

        // WWW를 통해 오디오 클립을 가져온다.
        WWW audioFile = new WWW("file:///" + path);
        yield return audioFile;

        if (audioFile.error == null)
        {
            // 성공적으로 가져오면 노래를 재생시킨다.
            Debug.Log("Loaded file successfully");
            audioClip = audioFile.GetAudioClip();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("Open File Error : " + audioFile.error);
            yield break; // stop the coroutine here
        }
    }

    // 키를 입력받으면 라인을 빛나게 한다
    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderes[index].color;
        color.a = 0.32f;
        trailSpriteRenderes[index].color = color;
    }
    
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
            comboCountText.text = combo.ToString();
            comboAnimator.SetTrigger("Show");
            comboTextAnimator.SetTrigger("Show");
        }
        if (maxCombo < combo)
        {
            // 현재의 최대콤보가 갱신된경우
            maxCombo = combo;
        }
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

    // 일시정지 버튼 클릭 시 
    public void pauseButtonEvent()
    {
        IsPause = true;
        Time.timeScale = 0;
        audioSource.Pause();
        pausePanel.SetActive(true);
    }

    public void BackButtonEvent()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        audioSource.Play();
        IsPause = false;
        
    }

    // 계속하기 버튼 클릭 시
    public void ContinueButtonEvent()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        audioSource.Play();
        IsPause = false;
    }

    // 다시하기 버튼 클릭 시
    // 난이도 선택으로 가준다.
    public void RetryButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SetLevelScene");
        CancelInvoke();
        StopAllCoroutines();
    }

    // 홈버튼 클릭 시
    // 모드선택화면으로 가준다.
    public void HomeButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SelectModeScene");
        CancelInvoke();
        StopAllCoroutines();
    }


    // 아두이노에서 보내느 값을 읽고 처리한다.
    public void ReadSerialValue()
    {
        sw.Start();
        string input = SerialManager.instance.serial.ReadLine();
        // string input = System.Text.ASCIIEncoding.ASCII.GetString(SerialManager.instance.serial.);

        if(input.Equals("1"))
        {
            ShineTrail(0);
            Debug.Log("1");
        }

        if(input.Equals("2"))
        {
            ShineTrail(1);
            Debug.Log("2");
        }

        sw.Stop();
        Debug.Log("sw: " + sw.Elapsed.Milliseconds.ToString() + "ms");
        //sw.Reset();
    }
}


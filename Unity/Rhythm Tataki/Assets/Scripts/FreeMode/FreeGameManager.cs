﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using NatCorderU.Examples;
using System;

public class FreeGameManager : MonoBehaviour
{
    public static FreeGameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        Application.targetFrameRate = 60; // 프레임 설정.
    }


    // 애니메이션 오브젝트와 애니메이션
    public GameObject[] leftDrumGameObjects;
    public GameObject[] rightDrumGameObjects;
    private Animator[] leftDrumAnimators;
    private Animator[] rightDrumAnimators;

    public float noteSpeed;

    // UI를 가져옴
    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderes;

    public bool IsPause;
    public GameObject pausePanel;

    // 음악변수
    private AudioSource audioSource;
    public AudioClip audioClip;

    // 드럼 소리 변수
    public AudioSource leftDrumSound;
    public AudioSource rigthDrumSound;

    private FileStream file;
    private StreamWriter sw;

    // FreeNoteController를 가져와서 사용한다.
    public FreeNoteController freeNoteController;

    private float time = 0.0f;  // 30초를 체크하기 위한 시간 변수
    private const float MAX_RECORDING_TIME = 30f; // 30초 설정

    private string fileName;
    private string directoryName = "beats";
    private string beatsFolderPath;
    private bool recordingCoroutineIsRuinning = false;

    //int soundId1, soundId2;
    
    void Start()
    {
        // 초기화
        leftDrumAnimators = new Animator[leftDrumGameObjects.Length];
        rightDrumAnimators = new Animator[rightDrumGameObjects.Length];

        for (int i = 0; i < leftDrumGameObjects.Length; i++)
        {
            leftDrumAnimators[i] = leftDrumGameObjects[i].GetComponent<Animator>();
        }

        for(int i = 0; i < rightDrumGameObjects.Length; i++)
        {
            rightDrumAnimators[i] = rightDrumGameObjects[i].GetComponent<Animator>();
        }
        IsPause = false;
        // 패널숨김
        pausePanel.SetActive(false);
        // MusicStart함수 실행, 1초 후에
        Invoke("MusicStart", 1);
        // 30초 후 녹화 종료 코루틴 실행
        // StartCoroutine(StopRecordingPoint());

        // 스프라이트 렌더러를 이용하여 스프라이트 이미지를 표시할 수 있음.
        // 스프라이트 렌더러 초기화
        // 트레일들을 index로 넣어줌
        trailSpriteRenderes = new SpriteRenderer[trails.Length];

        for (int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderes[i] = trails[i].GetComponent<SpriteRenderer>();
        }

        // 드럼 소리 셋팅
        leftDrumSound = gameObject.AddComponent<AudioSource>();
        rigthDrumSound = gameObject.AddComponent<AudioSource>();
        leftDrumSound.clip = PlayerInformation.leftDrumSound;
        rigthDrumSound.clip = PlayerInformation.rightDrumSound;

        //soundId1 = AudioCenter.loadSound("DrumSnare");
        //soundId2 = AudioCenter.loadSound("DrumKick");

        // 비트와 노래 저장하는 경로.
        beatsFolderPath = Application.persistentDataPath + Path.DirectorySeparatorChar + directoryName + Path.DirectorySeparatorChar;

        // 파일명 중복 확인.
        fileName = PlayerInformation.selectedMusic + ".txt";
        fileName = FileUploadName(beatsFolderPath, fileName);

        // 파일 생성.
        sw = new StreamWriter(beatsFolderPath + fileName, true); // true는 덮어쓴다는 말.
        // 먼저 노래 제목을 첫번째 줄에 넣어준다.
        sw.WriteLine(PlayerInformation.musicTitle);
        // 그 다음은 제작자 정보
        sw.WriteLine("creater");
        // 그 다음은 노래 정보 그 이후로는 노트 값
        // 점수 기준 정보는 퍼펙트가 기준이므로 계산을 해줘야함
        sw.WriteLine("120 60 3.5 500 400 300");

        // 노래가 언제 끝나는지 체크하는 코루틴 시작.
        StartCoroutine("AudioEndPoint");
    }

    // 음악실행 함수
    void MusicStart()
    {
        // 리소스에서 비트 음악 파일을 불러와 재생
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        // 만약 Resources 폴더에서 찾지 못하면 사용자가 노래를 추가 한 것이므로 persistancePath에서 찾는다.
        if (audioClip == null)
        {
            Debug.Log("audioClip did not Exist");
            string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + PlayerInformation.selectedMusic + ".ogg";

            // 노래 찾는 함수 실행
            StartCoroutine(LoadAudioClip(path, audioClip));
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        // 녹화 시작.
        // RecordManager.instance.audioSource = audioSource;
        // RecordManager.instance.StartRecording();
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

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    // 모든 터치에 대해서 그 정보를 확인한다.
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        Touch tempTouch = Input.GetTouch(i);
        //        // tempTouch에 터치가 발생했다면 사용자가 터치하자마자
        //        // 특정한 구문을 수행하도록 한다.
        //        if (tempTouch.phase == TouchPhase.Began)
        //        {
        //            Ray ray = Camera.main.ScreenPointToRay(tempTouch.position);
        //            RaycastHit hit;
        //            // 사용자가 터치를 시도한 그위치에 콜라이더가 적용된 오브젝트가 있는지 감지한다.
        //            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //            {
        //                if (hit.collider.name == "Trail 1")
        //                {
        //                    ShineTrail(0);
        //                    string audioTime = "0 " + audioSource.timeSamples.ToString();
        //                    // Debug.Log(audioTime);
        //                    sw.WriteLine(audioTime);
        //                    freeNoteController.MakeNote(1);
        //                }
        //                if (hit.collider.name == "Trail 2")
        //                {
        //                    ShineTrail(1);
        //                    string audioTime = "1 " + audioSource.timeSamples.ToString();
        //                    // Debug.Log(audioTime);
        //                    sw.WriteLine(audioTime);
        //                    freeNoteController.MakeNote(2);
        //                }
        //            }
        //        }
        //    }
        //}
        // 사용자가 입력한 키에 해당하는 라인을 빛나게 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShineTrail(0);
            leftDrumSound.Play();
            //AudioCenter.playSound(soundId1);
            //Debug.Log(audioSource);
            string audioTime = "0 " + (audioSource.time - PlayerInformation.NOTE_MOVEMENT_TIME).ToString();
            sw.WriteLine(audioTime);    // 기록
            // Debug.Log(audioTime);
            freeNoteController.MakeNote(1);

            // 왼쪽 애니메이션 실행
            for (int i = 0; i < leftDrumAnimators.Length; i++)
            {
                leftDrumAnimators[i].SetTrigger("Show");
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShineTrail(1);
            rigthDrumSound.Play();
            //AudioCenter.playSound(soundId2);
            string audioTime = "1 " + (audioSource.time - PlayerInformation.NOTE_MOVEMENT_TIME).ToString();
            sw.WriteLine(audioTime);    // 기록
            // Debug.Log(audioTime);
            freeNoteController.MakeNote(2);

            // 오른쪽 애니메이션 실행
            for(int i = 0; i < rightDrumAnimators.Length; i++)
            {
                rightDrumAnimators[i].SetTrigger("Show");
            }
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
                ReadSerialValue();

            }
            catch (TimeoutException e)
            {
                // Debug.Log("serial Error: " + e.ToString());
            }

        }

    }

    // 키를 입력받으면 라인을 빛나게 한다
    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderes[index].color;
        color.a = 0.32f;
        trailSpriteRenderes[index].color = color;
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
        Time.timeScale = 1;
        audioSource.Play();
        IsPause = false;
        pausePanel.SetActive(false);

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
    // 파일을 삭제하고 노래 선택 화면으로 가준다.
    public void RetryButtonEvent()
    {
        Time.timeScale = 1;
        // 파일 삭제 함수 호출
        DeleteFile(Application.persistentDataPath + Path.DirectorySeparatorChar + "beats", fileName);
        SceneManager.LoadScene("FreeMusicSelectScene");

        CancelInvoke();
        StopAllCoroutines();
    }

    // 홈버튼 클릭 시
    // 파일을 삭제하고 모드선택화면으로 가준다.
    public void HomeButtonEvent()
    {
        Time.timeScale = 1;
        // 파일 삭제 함수 호출
        DeleteFile(Application.persistentDataPath + Path.DirectorySeparatorChar + "beats", fileName);
        SceneManager.LoadScene("SelectModeScene");

        CancelInvoke();
        StopAllCoroutines();
    }

    // 홈버튼, 다시하기버튼 클릭 시 기록하던 파일을 삭제한다.
    public void DeleteFile(string path, string fileName)
    {
        // 먼저 기록하고 있던 txt파일을 닫는다.
        // 이걸 하지 않고 파일을 실행하면 Sharing violation on path 오류가 뜸.
        sw.Close();

        string file = path + Path.DirectorySeparatorChar + fileName;
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    // 노래가 끝나면 결과창으로 전환한다.
    public void ShowResultScene()
    {
        Debug.Log("End Music");
        sw.Close(); // 노트 기록하던 파일 닫는다.

        // 동영상 복사
        SaveRecordingFile();
        // StartCoroutine(FileUpload(Application.persistentDataPath, "/솜사탕.ogg"));
        // 결과씬으로 전환
        SceneManager.LoadScene("FreeResultScene");
    }

    // 동영상 저장.
    public void SaveRecordingFile()
    {
        string fileName = "12.mp4";
        string originalFilePath = "C:/Users/SAMSUNG9/Music/" + fileName;    //  윈도우
        string saveFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "records" + Path.DirectorySeparatorChar + fileName;
        
        if (File.Exists(originalFilePath))
        {
            FileInfo fileInfo = new FileInfo(originalFilePath);

            // 파일 복사 (원래파일경로, 이동하고자하는 경로, true)는 중복파일일 경우 덮어쓰기.
            // false일 경우 해당 파일이 있으면 오류 발생(try catch 문을 이용하여도됨)
            File.Copy(originalFilePath, saveFilePath, true);

            Debug.Log("File copy complete");
        }
        else
        {
            Debug.Log("File does not Exist");
        }
    }

    // 노래가 언제 끝나는지 알려주는 코루틴.
    IEnumerator AudioEndPoint()
    {
        while (true)
        {
            // 2초마다 체크한다.
            yield return new WaitForSeconds(2.0f);
            if (!audioSource.isPlaying)
            {
                // 끝나면 녹화 종료
                // RecordManager.instance.StopRecording();
                // 코루틴이 실행중이라면 종료시킨다.
                if(recordingCoroutineIsRuinning)
                {
                    StopCoroutine("StopRecordingPoint");
                }
                   
                // 결과화면 보여준다.
                ShowResultScene();
                break;
            }
        }
    }

    public void WriteStringToFile(string fileName)
    {
// #if!WEB_BUILD
        string path = PathForDocumentsFile(fileName);
        file = new FileStream(path, FileMode.Create, FileAccess.Write);
        sw = new StreamWriter(file);
    }

    // 경로확인
    public string PathForDocumentsFile(string fileName)
    {
        string path;
        if(Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else
        {
            path = Application.dataPath;
        }


        path = path.Substring(0, path.LastIndexOf('/'));
        return Path.Combine(path, fileName);
    }

    // 파일 업로드
    IEnumerator FileUpload(string path, string fileName)
    {
        WWW localFile = new WWW("file:///" + path + fileName);
        Debug.Log("file:///" + path + fileName);
        
        yield return localFile;
        if(localFile.error == null)
        {
            Debug.Log("Loaded file successfully");
        }
        else
        {
            Debug.Log("Open File Error : " + localFile.error);
            yield break; // stop the coroutine here
        }

        Debug.Log(localFile.size);

        WWWForm postForm = new WWWForm();
        postForm.AddBinaryData("file", localFile.bytes, fileName);

        WWW upload = new WWW("http://dev.rhythmtataki.p-e.kr/unity/fileUpload", postForm);

        yield return upload;

        if(upload.error == null)
        {
            Debug.Log("upload done : " + upload.text);
        }
        else
        {
            Debug.Log("Error during upload : " + upload.error);
        }
    }

    /*
     * 녹화관련
     * 30초가 지나면 자동 종료
     * 30초전에 노래가 끝나면 노래가 끝난 시점에서 종료
     * 일시정지 후 홈 & 다시하기 선택 시 저장되지 않고 녹화 종료(삭제)
     * 사용자가 노래가 끝나고 저장하기 누르면 서버로 전송.
     * 노래가 실행되면 녹화를 시작한다.
     */
    // 시간을 30초 체크하는 코루틴 1초마다 실행하게 함.
    IEnumerator StopRecordingPoint()
    {
        recordingCoroutineIsRuinning = true;
        while (true)
        {
            // 2초마다 체크한다.
            yield return new WaitForSeconds(2.0f);
            time += 2.0f;
            if (time >= MAX_RECORDING_TIME)
            {
                Debug.Log("end recording");
                // 게임화면 녹화 중지 함수 호출
                RecordManager.instance.StopRecording();
                // 시간 다시 초기화
                time = 0.0f;
                // 코루틴 실행 여부 false로 바꿈.
                recordingCoroutineIsRuinning = false;
                break;
            }
        }
    }

    /*
     * 파일명 중복 확인.
     * 같은 노래에 노트파일은 여러개가 있을 수 있으므로 
     * 그럴 때 기존의 노트파일을 덮어씌우는게 아니라 다른 파일명으로 생성해야함
     */
    public string FileUploadName(string dirPath, string fileN)
    {
        string fileName = fileN;

        if (fileN.Length > 0)
        {
            // 확장자 앞의 Dot의 위치를 기준으로 파일명과 확장자를 구분하여 저장.
            int indexOfDot = fileName.LastIndexOf(".");
            string strName = fileName.Substring(0, indexOfDot);
            string strExt = fileName.Substring(indexOfDot);

            bool bExist = true;
            int fileCount = 0;

            while (bExist)
            {
                string pathCombine = Path.Combine(dirPath, fileName);

                // 파일이 존재하는지 확인
                // 존재하면 파일명.확장자(카운트) 로 진행
                // 존재하지 않으면 반복 끝.
                if (File.Exists(pathCombine))
                {
                    fileCount++;
                    fileName = strName + "(" + fileCount + ")" + strExt;
                }
                else
                    bExist = false;
            }
        }
        return fileName;
    }

    // 아두이노에서 보내느 값을 읽고 처리한다.
    public void ReadSerialValue()
    {
        string input = SerialManager.instance.serial.ReadLine();
        // string input = System.Text.ASCIIEncoding.ASCII.GetString(SerialManager.instance.serial.);

        if (input.Equals("1"))
        {
            ShineTrail(0);
            leftDrumSound.Play();
            //AudioCenter.playSound(soundId1);
            //Debug.Log(audioSource);
            string audioTime = "0 " + (audioSource.time - PlayerInformation.NOTE_MOVEMENT_TIME).ToString();
            sw.WriteLine(audioTime);    // 기록
            // Debug.Log(audioTime);
            freeNoteController.MakeNote(1);
        }

        if (input.Equals("2"))
        {
            ShineTrail(1);
            rigthDrumSound.Play();
            //AudioCenter.playSound(soundId2);
            string audioTime = "1 " + (audioSource.time - PlayerInformation.NOTE_MOVEMENT_TIME).ToString();
            sw.WriteLine(audioTime);    // 기록
            // Debug.Log(audioTime);
            freeNoteController.MakeNote(2);
        }
    }
}

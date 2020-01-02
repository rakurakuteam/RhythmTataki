using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;
using System;
using System.Text;


public class LoginManager : MonoBehaviour
{
    // 이메일 및 패스워드 UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // 데이터를 전송할 URL
    private string URL = "http://capstone.rhythmtataki.p-e.kr/unity/getScores/";
    // private string URL = "http://dev.rhythmtataki.p-e.kr/unity/getScores/";
    public string jsonString;

    // 오류에 대한 정보를 보여 줄 UI
    public Text messageUI;

    // 데이터를 전송할 URL
    private string Login_URL = "http://capstone.rhythmtataki.p-e.kr/unity/login";
    // private string Login_URL = "http://dev.rhythmtataki.p-e.kr/unity/login";

    public string[] folderNames = { "beats", "records" };

    private void Awake()
    {
        // 화면이 꺼지지 않도록
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 게임 실행 시 폴더를 생성해준다.
        // 유저가 새로 다운 받는 노래와 노트, 녹화영상을 저장하기 위함
        string dirPath = PlayerInformation.dataPath;
        CheckDirectory(dirPath, folderNames);
    }

    public void CheckDirectory(string dirPath, string[] folderArray)
    {
        for (int i = 0; i < folderArray.Length; i++)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath + folderArray[i]);
            if (!di.Exists)
            {
                di.Create();
                // Debug.Log("create folder");
            }
        }
    }

    public void Login()
    {
        string email = emailInputField.text;    // 이메일 받아옴
        string pw = passwordInputField.text;    // 패스워드 받아옴

        // PlayerInformation의 드럼 소리 이름 셋팅
        SetDrumSoundList();

        // 로그인 처리 코루틴 실행
        StartCoroutine(StartLogin(email, pw));
    }

    public void SignIn()
    {
        SceneManager.LoadScene("JoinScene");
    }

    public void Test()
    {
        Debug.Log("dd");
    }

    // 로그인 확인 라라벨로 송부
    IEnumerator StartLogin(string email, string pw)
    {

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("pw", pw);

        WWW www = new WWW(Login_URL, form);
        // 데이터가 올 때까지 기다림.
        yield return www;
        
        if (www.error == null)
        {
            // 성공한 경우
            Debug.Log("WWW OK : " + www.text);
            if(www.text == "1")
            {
                // 1을 리턴받으면 로그인 성공한 경우
                Debug.Log("로그인 성공");

                // 유저 이메일로 정보 초기화
                PlayerInformation.userEmail = email;

                // 이메일로 사용자 정보 받아오는 코루틴 실행
                StartCoroutine(GetScores(email));
                
            } else
            {
                // 0을 리턴받으면 로그인 실패한 경우
                Debug.Log("로그인 실패");
            }
        }
        else
        {
            // 실패한 경우
            Debug.Log("WWW Error : " + www.error);
        }
    }

    // 모드선택화면 즉 로그인 이후에 프리모드, 노말모드의
    // ListView에 사용할 이미지 및 텍스트들을 셋팅해준다.
    public void SetMusicList(string jsonString)
    {
        PlayerInformation.musicCount = 11;
        PlayerInformation.noteFileNames = new List<string>();
        PlayerInformation.titleSprites = new List<Sprite>();
        PlayerInformation.titleList = new List<string>();
        PlayerInformation.rankSprites = new List<Sprite>();
        PlayerInformation.scores = new List<int>();

        // 노래 리스트가 기록된 Text파일을 불러온다.
        // TextAsset textAsset = Resources.Load<TextAsset>("SongImages/List");
        // StringReader reader = new StringReader(textAsset);
        string listFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "List.txt";
        Debug.Log(listFilePath);
        StreamReader sr = new StreamReader(listFilePath);
        

        // 노트 파일의 제목이 기록된 Text파일을 불러온다.
        // TextAsset noteList = Resources.Load<TextAsset>("Beats/Note List");
        // StringReader noteListReader = new StringReader(noteList.text);

        for (int i = 0; i < PlayerInformation.musicCount; i++)
        {
            string index = (i + 1).ToString();
            
            // 한줄씩 노래 제목을 가지고온다.
            string getString = sr.ReadLine();

            // 노트파일 이름을 가지고 온다.
            // string noteFileName = noteListReader.ReadLine();
            // NoteFileNames 리스트에 저장한다.
            PlayerInformation.noteFileNames.Add(index);

            // 타이틀이미지 리스트와 타이틀텍스트리스트, 랭크이미지를 셋팅한다.
            PlayerInformation.titleSprites.Add(Resources.Load<Sprite>("SongImages/" + index));
            PlayerInformation.titleList.Add(getString);

            // Json 파싱하여 점수를 셋팅한다.
            LoadMusicDataFromJson(jsonString, index);
        }
    }

    public void SetDrumSoundList()
    {
        // 관련 변수 초기화
        PlayerInformation.drumSoundCount = 7;
        PlayerInformation.soundNameList = new List<string>();
        PlayerInformation.soundFileNameList = new List<string>();

        // 우선 선택없음 항목을 생성한다.
        PlayerInformation.soundNameList.Add("なし");
        PlayerInformation.soundFileNameList.Add(null);

        // 나중에는 서버로부터 사운드 리스트와 노래를 받아서 셋팅한다.
        TextAsset textAsset = Resources.Load<TextAsset>("EffectSounds/List");
        StringReader reader = new StringReader(textAsset.text);

        for (int i = 1; i < PlayerInformation.drumSoundCount; i++)
        {
            // 한줄씩 드럼 소리 이름을 가지고온다.
            string getString = reader.ReadLine();

            // 가지고 온 것 중 공백을 기준으로 앞에 것을 드럼소리이름 리스트에 저장한다.
            // 두번째 인덱스는 영어로된 파일이름을 저장한다.
            string drumName = getString.Split(' ')[0];
            string drumSondFileName = getString.Split(' ')[1];
            PlayerInformation.soundNameList.Add(drumName);
            PlayerInformation.soundFileNameList.Add(drumSondFileName);
        }
    }

    // DB에서 점수 정보를 가지고온다.
    IEnumerator GetScores(string email)
    {

        Debug.Log("GetScores실행");
        // WWWForm form = new WWWForm();
        // form.AddField("email", email);


        // Debug.Log(URL + email);
        WWW www = new WWW(URL + email);
        yield return www;

        if (www.error == null)
        {
            // 성공한 경우
            // Debug.Log("WWW OK : " + www.text);
            jsonString = www.text;
            Debug.Log(jsonString);

            // 노래 목록 셋팅하는 함수 실행
            SetMusicList(jsonString);

            // 노래 셋팅이 끝나면 모드 선택화면으로 씬 변경
            SceneManager.LoadScene("SelectModeScene");
        }
        else
        {
            // 실패한 경우
            Debug.Log("WWW Error : " + www.error);
            jsonString = "Error";
        }
    }

    // Json에서 곡에대한 점수를 파싱하는 함수
    public void LoadMusicDataFromJson(string jsonString, string getString)
    {
        JsonData musicData = JsonMapper.ToObject(jsonString);

        int score = Convert.ToInt32(musicData[getString].ToString());
        // Debug.Log(getString);
        // Debug.Log(score);
        PlayerInformation.scores.Add(score);

        SetStarsSprite(getString, score);
    }

    // 점수에 따라 별 이미지 셋팅
    public void SetStarsSprite(string title, int score)
    {
        // title에 맞는 노트파일을 가지고온다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + title);
        StringReader reader = new StringReader(textAsset.text);
        // 첫번째줄(제목) 두번째줄(작곡가) 무시.
        reader.ReadLine();
        reader.ReadLine();

        // 세번째 줄에 점수기준이 있기 때문에 세번째 줄을 가지고온다.
        string beatInformation = reader.ReadLine();
        // 4번째있는 것이 별3개, 5번째가 별 2개, 6번째가 별 1개
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        // 점수에 맞는 별 이미지를 셋팅한다.
        if (score >= scoreS)
        {
            PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_S_Rank"));
        }
        else if (score >= scoreA)
        {
            PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_A_Rank"));
        }
        else if (score >= scoreB)
        {
            PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_B_Rank"));
        }
        else
        {
            PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_default"));
        }
    }

    
}

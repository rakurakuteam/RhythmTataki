using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalNoteController : MonoBehaviour
{
    // 하나의 노트에 대한 정보를 담는 노트 클래스 정의
    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; } // 특정한 노트가 있을 때 그 노트가 떨어지는 순서
        
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    class Note2
    {
        public int noteType { get; set; }
        public float order { get; set; }

        public Note2(int noteType, float order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private List<Note2> notes2 = new List<Note2>();
    // 유저가 선택한 난이도에 따른 startY Postion
    private float[] createPosition = { 6.6f, 11.6f, 16.6f, 21.6f, 26.6f };

    private float x, z; // 노트가 활성화되는 위치
    private float startY;
    

    // 하나의 노트가 만들어졌을 때 처리하는 함수
    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        // 설정된 시작 라인으로 노트를 이동시킨다.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        // Y축만 바꿔서 올려준다.
        obj.transform.position = new Vector3(x, startY, z); 
        obj.GetComponent<NormalNoteBehavior>().Initialize();
        obj.SetActive(true); // 보여줌
    }

    void MakeNote2(Note2 note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        // 설정된 시작 라인으로 노트를 이동시킨다.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        startY = YLocation(PlayerInformation.selectLevel);
        // Y축만 바꿔서 올려준다.
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NormalNoteBehavior>().Initialize();
        obj.SetActive(true); // 보여줌
    }

    private string musicTitle;
    private string musicArtist;
    private int bpm;
    private int divider;
    private float startPoint;
    private float beatCount;
    private float beatInterval;
    private float timing;

    float YLocation(string selectedLevel)
    {
        float yLocation = 0;

        for(int i = 0; i < createPosition.Length; i++)
        {
            // 선택된 레벨(1 ~ 5)이 i + 1과 같다면
            // createPosition의 i 번째 인덱스가 생성위치가 된다.
            if(Convert.ToInt32(selectedLevel) == i + 1)
            {
                yLocation = createPosition[i];
            }
        }
        return yLocation;
    }

    // 코루틴 사용
    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        // 1초 기다린 후에 아래 내용을 실행해라
        yield return new WaitForSeconds(startPoint + order * beatInterval);
        MakeNote(note); 
    }

    IEnumerator StartMakeNote(List<Note2> notes2)
    {
        Debug.Log("StartMakeNote");
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(AwaitMakeNote2(notes2));
    }

    // 코루틴 사용
    IEnumerator AwaitMakeNote2(List<Note2> notes2)
    {
        if (notes2.Count == 0) yield break;

        for (int i = 0; i < notes2.Count; i++)
        {
            // Debug.Log("AwaitMakeNote2");
            Note2 note2 = notes2[i];
            int noteType = note2.noteType;
            float noteTiming = note2.order;
            
            while (true)
            {
                float currentTiming = GameManager.instance.audioSource.time;
                // Debug.Log(currentTiming);
                
                if (noteTiming > currentTiming - 0.05f && noteTiming < currentTiming + 0.05f)
                {
                    // Debug.Log(noteTiming);
                    MakeNote2(note2);
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

        }
        //int noteType = note.noteType;
        //float order = note.order;
        //// 1초 기다린 후에 아래 내용을 실행해라
        //yield return new WaitForSeconds(startPoint + order * beatInterval);
        //MakeNote2(note);
    }

    // Start is called before the first frame update
    void Start()
    {
        startY = YLocation(PlayerInformation.selectLevel);
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // 초기화

        // 텍스트에서 비트정보를 읽는다
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StreamReader reader;
        // textAsset을 얻지 못하면 사용자가 추가하거나 다운 받은것이므로 내부폴더에서 가져온다.
        if (textAsset == null)
        {
            Debug.Log("textAsset is Null");
            string txtFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + PlayerInformation.selectedMusic + ".txt";
            reader = new StreamReader(txtFilePath);
        }
        else
        {
            // StringReader reader = new StringReader(textAsset.text);
            // Resources에 있는 txt파일을 streamReader로 읽기 위해서는 MemoryStream을 이용한다.
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text));
            reader = new StreamReader(ms);
        }

        // 첫번째 적힌 곡 이름을 읽는다
        musicTitle = reader.ReadLine();
        Debug.Log(musicTitle);
        // 두번째 아티스트를 읽는다
        musicArtist = reader.ReadLine();
        Debug.Log(musicArtist);
        // 세번째 줄에 적힌 비트 정보를 읽는다
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startPoint = (float) Convert.ToDouble(beatInformation.Split(' ')[2]);

        // 1초마다 떨어지는 비트 개수
        // 만약 txt 파일의 divider 값이 크면 1초에 생성되는 노트수가
        // 적어진다.
        beatCount = (float)bpm / divider;
        // 비트가 떨어지는 간격을 계산한다.(노트사이의 간격 시간임)
        beatInterval = 1 / beatCount;
        // 각 비트들이 떨어지는 위치 및 시간정보(4번째 줄부터)
        string line;
        //while ((line = reader.ReadLine()) != null)
        //{
        //    // 노트를 생성
        //    // 먼저 떨어지는 노트의 인덱스를 구하고
        //    // 언제 떨어지는 구해서 매개변수로 전달
        //    Note note = new Note(
        //        Convert.ToInt32(line.Split(' ')[0]) + 1,
        //        Convert.ToInt32(line.Split(' ')[1])
        //    );
        //    notes.Add(note);
        //}

        while ((line = reader.ReadLine()) != null)
        {
            // 노트를 생성
            // 먼저 떨어지는 노트의 인덱스를 구하고
            // 언제 떨어지는 구해서 매개변수로 전달
            Note2 note = new Note2(
                Convert.ToInt32(line.Split(' ')[0]) + 1,
                Convert.ToSingle(line.Split(' ')[1])
            );
            notes2.Add(note);
        }

        // 모든 노트를 정해진 시간에 출발하도록
        // 코루틴으로
        //for (int i = 0; i <notes.Count; i++)
        //{
        //    StartCoroutine(AwaitMakeNote(notes[i]));
        //}

        // 모든 노트를 정해진 시간에 출발하도록
        // 코루틴으로
        //for (int i = 0; i < notes2.Count; i++)
        //{
        //    StartCoroutine(AwaitMakeNote(notes[i]));
        //}

        StartCoroutine(StartMakeNote(notes2));

        // 마지막 노트를 기준으로 게임 종료 함수를 부른다.
        StartCoroutine(AwaitGameResult2(notes2[notes2.Count - 1].order));
    }


    IEnumerator AwaitGameResult(int order)
    {
        // Debug.Log("AwaitGameResult 실행");
        yield return new WaitForSeconds(startPoint + order * beatInterval + 4.0f);
        GameResult();
    }

    IEnumerator AwaitGameResult2(float order)
    {
        // Debug.Log("GameResult");
        yield return new WaitForSeconds(startPoint + order + 4.0f);
        GameResult();
    }

    void GameResult()
    {
        // 결과 화면 전환 전에 정보들을 저장한다.
        PlayerInformation.maxCombo = GameManager.instance.maxCombo;
        PlayerInformation.score = GameManager.instance.score;
        PlayerInformation.musicTitle = musicTitle;
        PlayerInformation.musicArtist = musicArtist;

        Debug.Log("GameResult 실행");
        SceneManager.LoadScene("NormalResultScene");
    }
}

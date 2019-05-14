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

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 6.6f; // 노트가 활성화되는 위치
    

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

    private string musicTitle;
    private string musicArtist;
    private int bpm;
    private int divider;
    private float startPoint;
    private float beatCount;
    private float beatInterval;

    // 코루틴 사용
    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        // 1초 기다린 후에 아래 내용을 실행해라
        yield return new WaitForSeconds(startPoint + order * beatInterval);
        MakeNote(note); 
    }

    // Start is called before the first frame update
    void Start()
    {
        // double noteSpeed = 10.4 / Time.deltaTime * 60 * 30;
        double noteSpeed = 0.2;
        Debug.Log(noteSpeed);

        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // 초기화

        // 텍스트에서 비트정보를 읽는다
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        // 첫번째 적힌 곡 이름을 읽는다
        musicTitle = reader.ReadLine();
        // 두번째 아티스트를 읽는다
        musicArtist = reader.ReadLine();
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
        while ((line = reader.ReadLine()) != null)
        {
            // 노트를 생성
            // 먼저 떨어지는 노트의 인덱스를 구하고
            // 언제 떨어지는 구해서 매개변수로 전달
            Note note = new Note(
                Convert.ToInt32(line.Split(' ')[0]) + 1,
                Convert.ToInt32(line.Split(' ')[1])
            );
            notes.Add(note);
        }

        // 모든 노트를 정해진 시간에 출발하도록
        // 코루틴으로
        for (int i = 0; i <notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }

        // 마지막 노트를 기준으로 게임 종료 함수를 부른다.
        StartCoroutine(AwaitGameResult(notes[notes.Count - 1].order));
    }

    IEnumerator AwaitGameResult(int order)
    {
        Debug.Log("AwaitGameResult 실행");
        yield return new WaitForSeconds(startPoint + order * beatInterval + 6.0f);
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

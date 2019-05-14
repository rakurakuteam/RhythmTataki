using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeNoteController : MonoBehaviour
{
    private KeyCode keyCode;
    public int noteType;

    // 하나의 노트에 대한 정보를 담는 노트 클래스 정의
    class Note
    {
        public int noteType { get; set; }
        
        public Note(int noteType)
        {
            this.noteType = noteType;
        }
    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 1.5f; // 노트가 활성화되는 위치

    // 하나의 노트가 만들어 졌을 때 처리하는 함수
    public void MakeNote(int noteType)
    {
        GameObject obj = noteObjectPooler.getObject(noteType);
        // 설정된 시작 라인으로 노트를 이동시킨다.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.SetActive(true); // 보여줌
    }



    // Start is called before the first frame update
    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>(); // 초기화
        // Debug.Log(noteObjectPooler);
        // 노트 생성
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 사용자가 드럼을 쳤을 때 노트를 생성한다.
         * 왼쪽 드럼을 치면 왼쪽에서 생성.
         * 오른쪽 드럼을 치면 오른쪽에서 생성
         */ 
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    MakeNote(1);
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    MakeNote(2);
        //}
    }
}

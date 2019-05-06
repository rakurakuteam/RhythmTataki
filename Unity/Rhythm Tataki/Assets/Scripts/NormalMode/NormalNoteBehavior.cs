using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNoteBehavior : MonoBehaviour
{
    public int noteType; // 노트가 1인지 2인지
    // public NormalGameManager normalGameManager;
    // public GameObject NGM;

    private GameManager.judes judge;
    // 키코드
    private KeyCode keyCode;
    private double noteSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = 10.4 / Time.deltaTime * 60 * 30;
        // normalGameManager = GetComponent<NormalGameManager>();
        if (noteType == 1) keyCode = KeyCode.LeftArrow;
        else if (noteType == 2) keyCode = KeyCode.RightArrow;
    }

    public void Initialize()
    {
        judge = GameManager.judes.NONE; // 처음은 none가지고 있는다ㅣ
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPause)
        {
            transform.Translate(Vector3.down * 0);
        }
        else
        {
            // 게임 매니저에 있는 노트속도값을 가져온다.
            // transform.Translate(Vector3.down * GameManager.instance.noteSpeed);
            transform.Translate(Vector3.down * 1 / 3f);
        }
        
        // transform.Translate(new Vector3(this.transform.position.x, (float)-3.4, this.transform.position.z));
        // 사용자가 노트 키를 입력한 경우
        if (Input.GetKeyDown(keyCode))
        {
            // 해당 노트에 대한 판정을 진행
            GameManager.instance.processJudge(judge, noteType);
            // 노트가 판정 선에 닿기 시작한 이후로는 해당 노트 제거(비활성)
            // NONE이 아니라면 충돌이 감지되었다는 말이다
            if (judge != GameManager.judes.NONE) gameObject.SetActive(false);
        }

        

    }

    // 노트에 대한 판정 수행
    // other는 판정선을 의미함
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judes.BAD;
        }
        else if (other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judes.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judes.PERFECT;
            if(GameManager.instance.autoPerfect)
            {
                GameManager.instance.processJudge(judge, noteType);
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judes.MISS;
            GameManager.instance.processJudge(judge, noteType);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class NormalNoteBehavior : MonoBehaviour
{
    public int noteType; // 노트가 1인지 2인지
    // public NormalGameManager normalGameManager;
    // public GameObject NGM;

    private GameManager.judes judge;
    // 키코드
    private KeyCode keyCode;

    private string code;
    private string input;
    // private Stopwatch sw = new Stopwatch();
    private float[] distanceArr = { 10, 15, 20, 25, 30 };
    private float selectedDistance;

    //public GameObject effectObj;
    //private Animator collisionEffectAnimator;
    
    void Start()
    {
        selectedDistance = SelectedDistance(PlayerInformation.selectLevel);
        // collisionEffectAnimator = GetComponent<Animator>();

        // normalGameManager = GetComponent<NormalGameManager>();
        if (noteType == 1) keyCode = KeyCode.LeftArrow;
        else if (noteType == 2) keyCode = KeyCode.RightArrow;

        if (noteType == 1) code = "1"; 
        else if (noteType == 2) code = "2";
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
            transform.Translate(Vector3.down * selectedDistance * Time.deltaTime);
        }
        
        // 사용자가 노트 키를 입력한 경우
        if (Input.GetKeyDown(keyCode))
        {
            // 해당 노트에 대한 판정을 진행
            GameManager.instance.processJudge(judge, noteType);

            //GameObject _obj = Instantiate(effectObj) as GameObject;
            // Instantiate(effectObj, gameObject.transform.position, Quaternion.identity);
            // effectObj.GetComponent<Animator>.SetTrigger("Show");
            
            //_obj.transform.localPosition = gameObject.transform.position;
            //_obj.GetComponent<Animator>().SetTrigger("Show");
            //Destroy(_obj, 0.3f);
            // 노트가 판정 선에 닿기 시작한 이후로는 해당 노트 제거(비활성)
            // NONE이 아니라면 충돌이 감지되었다는 말이다
            if (judge != GameManager.judes.NONE) gameObject.SetActive(false);
        }


        if (SerialManager.instance.serial.IsOpen)
        {
            try
            {
                input = SerialManager.instance.serial.ReadLine();
                // Debug.Log(input);
                if (input.Equals(code))
                {
                    // GameManager.instance.ShineTrail(noteType - 1);
                    // Debug.Log("code : " + code);
                    // 해당 노트에 대한 판정을 진행
                    GameManager.instance.processJudge(judge, noteType);
                    // 드럼소리 재생
                    // GameManager.instance.PlayDrumSound(noteType - 1);
                    // 노트가 판정 선에 닿기 시작한 이후로는 해당 노트 제거(비활성)
                    // NONE이 아니라면 충돌이 감지되었다는 말이다
                    if (judge != GameManager.judes.NONE) gameObject.SetActive(false);
                }
            }
            catch (TimeoutException e)
            {
                // Debug.Log("serial Error: " + e.ToString());
            }
        }

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
        //                    GameManager.instance.ShineTrail(0);
        //                    GameManager.instance.processJudge(judge, noteType);
        //                    GameManager.instance.PlayDrumSound(0);
        //                    if (judge != GameManager.judes.NONE) gameObject.SetActive(false);
        //                }
        //                if (hit.collider.name == "Trail 2")
        //                {
        //                    GameManager.instance.ShineTrail(1);
        //                    GameManager.instance.processJudge(judge, noteType);
        //                    GameManager.instance.PlayDrumSound(1);
        //                    if (judge != GameManager.judes.NONE) gameObject.SetActive(false);
        //                }
        //            }
        //        }
        //    }
        // }
    }

    // 노트에 대한 판정 수행
    // other는 판정선을 의미함
    private void OnTriggerEnter2D(Collider2D other)
    {
        // collisionEffectAnimator.SetTrigger("Show");
        if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judes.PERFECT;
            //GameObject _obj = Instantiate(effectObj) as GameObject;
            // Instantiate(effectObj, gameObject.transform.position, Quaternion.identity);
            // effectObj.GetComponent<Animator>.SetTrigger("Show");
            //_obj.transform.localPosition = gameObject.transform.position;
            //_obj.GetComponent<Animator>().SetTrigger("Show");
            // collisionEffectAnimator.SetTrigger("Show");
            // 자동 판정
            //if (GameManager.instance.autoPerfect)
            //{
            //    GameManager.instance.PlayDrumSound(noteType - 1);
            //    GameManager.instance.ShineTrail(noteType - 1);
            //    GameManager.instance.processJudge(judge, noteType);
            //    gameObject.SetActive(false);
            //}
        }
        else if (other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judes.BAD;
            
        }
        else if (other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judes.GOOD;
            if (GameManager.instance.autoPerfect)
            {
                GameManager.instance.PlayDrumSound(noteType - 1);
                GameManager.instance.ShineTrail(noteType - 1);
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

    // 선택된 레벨에 따른 다른 이동거리를 설정
    private float SelectedDistance(string selectedLevel)
    {
        float distance = 0;

        for (int i = 0; i < distanceArr.Length; i++)
        {
            // 선택된 레벨(1 ~ 5)이 i + 1과 같다면
            // createPosition의 i 번째 인덱스가 생성위치가 된다.
            if (Convert.ToInt32(selectedLevel) == i + 1)
            {
                /*
                 * 1초에 이동할 거리를 구한다.
                 * 노트가 처음에 생성되고 판정선까지 도착하는 데 까지 걸려야하는 시간이 2초
                 * 이동해야 하는 총 거리가 10 이라면
                 * 노트는 1초에 5만큼의 거리를 이동해야 함.
                 * 그리고 이 거리에 프레임 시간을 곱하면 노트를 정확하게 판정선까지 이동시킬 수 있음.
                 */ 
                distance = distanceArr[i] / PlayerInformation.NOTE_MOVEMENT_TIME;
            }
        }

        return distance;
    }
}

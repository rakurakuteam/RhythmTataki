using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeNoteBehavior : MonoBehaviour
{
    public int noteType;
    private KeyCode keyCode;

    // Start is called before the first frame update
    void Start()
    {
        if (noteType == 1) keyCode = KeyCode.LeftArrow;
        else if (noteType == 2) keyCode = KeyCode.RightArrow;
    }

    // Update is called once per frame
    void Update()
    {
        // 정지상태인지 체크 후 정지 상태가 이면 노트를 멈추고 그렇지 않으면 이동시킨다.
        if (FreeGameManager.instance.IsPause)
        {
            transform.Translate(Vector3.up * 0);
        }
        else
        {
            // 게임 매니저에 있는 노트속도값을 가져온다.
            transform.Translate(Vector3.up * FreeGameManager.instance.noteSpeed);
        }

        // 노트가 화면에 벗어가는 경우 해당 노트 제거(비활성)
        if (gameObject.transform.position.y > 6) gameObject.SetActive(false);
    }
    
}

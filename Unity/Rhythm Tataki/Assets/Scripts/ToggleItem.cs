using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleItem : MonoBehaviour
{
    public Toggle item;
    public Text soundsName;
    public Text soundFileName;


    private void Start()
    {
        // 토글 활성 비활성 사용법
        // item.isOn = false;
    }


    // 소리같은경우는 미리 DB에서 받은다음에 모드 선택씬에서 전부 셋팅하고
    // 토글버튼을 클릭하면 해당 토글버튼의 Text를 가져와서
    // 그 사운드 이름으로 Find를 하여 해당 오디오 클립을 가져온다. 
    // 그리고 해당 소리를 실행해주면될듯
    // 새로운 아이템이 추가된 경우는
    // 소리는 오디오 클립 리스트에, 텍스트는 토글에 담아두면될듯?

    /*
     * 웹의 경우 사용자가 해당 드럼소리를 다운 받으면 S3에 저장되는데
     * DB에는 S3에 저장된 파일 경로와 파일 이름을 같이 저장한다.
     * 그리고 사용자가 유니티상에서 초기화 버튼을 누르면
     * 파일과 text를 같이 가지고 오고, 로컬에 저장한다.
     */
}

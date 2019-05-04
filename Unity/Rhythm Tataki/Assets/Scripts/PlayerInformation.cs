using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerInformation
{
    // 단순히 플레이어의 정보를 가지고 있는다
    // 그래서 Monobehavior 상속 필요 없음
    public static int maxCombo { get; set; }
    public static float score { get; set; }
    public static string selectedMusic { get; set; }
    public static string musicTitle { get; set; }
    public static string musicArtist { get; set; }
    public static Sprite titleImage { get; set; }


    /*
     * DB 및 서버관련 정보
     */
    public static string URL = "https://capstone.rhythmtataki.p-e.kr/unity/";
    public static string userEmail { get; set; }
    

    /*
     * 게임에서 기본적으로 제공해 줄 노래의 정보를 담는다.
     * 이 클래스에서 변수를 정의하고
     * 사용자가 로그인 후 모드 선택 화면에서 초기화해준다.
     */
    public static int musicCount { get; set; }  // 기본 제공해주는 노래 수
    public static List<Sprite> titleSprites { get; set; } // 제목 이미지
    public static List<string> titleList { get; set; }    // 노래 제목
    public static List<int> scores { get; set; }          // 노래에 대한 점수
    public static List<Sprite> rankSprites { get; set; }  // 점수에 따라 별의 개수 표시

    /*
     * 게임에서 기본적으로 제공해 줄 드럼소리를 담는다
     * 사용자가 로그인 후 모드 선택 화면에서 초기화 해준다.
     */
    public static int drumSoundCount { get; set; }
    public static List<string> soundNameList { get; set; }
    public static List<string> soundFileNameList { get; set; }
    public static AudioClip leftDrumSound { get; set; }
    public static AudioClip rightDrumSound { get; set; }

}

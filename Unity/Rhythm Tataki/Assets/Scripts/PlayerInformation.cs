using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
     * 게임에 전반적으로 사용되는 변수들
     */
    public static int FPS = 60;     // 초당 프레임 수
    public static string selectLevel { get; set; } // 사용자가 선택한 레벨
    public const float NOTE_MOVEMENT_TIME = 2.0f;  // 노트가 생성 된 후 도착하는 시간.

    /*
     * DB 및 서버관련 정보
     */
    public static string URL = "http://capstone.rhythmtataki.p-e.kr/unity/";
    public static string devURL = "http://dev.rhythmtataki.p-e.kr/unity/";
    public static string userEmail { get; set; }

    /*
     * 폴더 관련 정보
     */
    public static string dataPath = Application.persistentDataPath + Path.DirectorySeparatorChar;
    
    /*
     * 게임에서 기본적으로 제공해 줄 노래의 정보를 담는다.
     * 이 클래스에서 변수를 정의하고
     * 사용자가 로그인 후 모드 선택 화면에서 초기화해준다.
     */
    public static int musicCount { get; set; }  // 기본 제공해주는 노래 수
    public static List<string> noteFileNames { get; set; }// 노트파일의 이름
    public static List<Sprite> titleSprites { get; set; } // 제목 이미지
    public static List<string> titleList { get; set; }    // 노래 제목
    public static List<int> scores { get; set; }          // 노래에 대한 점수
    public static List<Sprite> rankSprites { get; set; }  // 점수에 따라 별의 개수 표시
    // public static List<string> musicFileNameList { get; set; } // Music폴더의 음악파일의 이름 저장

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

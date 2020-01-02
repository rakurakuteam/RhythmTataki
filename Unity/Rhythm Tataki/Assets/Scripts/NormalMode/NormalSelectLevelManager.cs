using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class NormalSelectLevelManager : MonoBehaviour
{
    // UI를 사용하려면 public으로 해야함 
    public Text titleText;
    public Image titleImage;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // 해당 노래에 맞춘 노래가 나오도록
    private void UpdateSong()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        // 리소스에서 오디오 파일을 불러온다.
        audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        if (audioClip == null)
        {
            string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + PlayerInformation.selectedMusic + ".ogg";

            // 노래 찾는 함수 실행
            StartCoroutine(LoadAudioClip(path, audioClip));
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        // 또 노트 정보가 담긴 Text파일을 불러온다.
        // TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
    }

    // 노래 파일을 찾는 코루틴
    IEnumerator LoadAudioClip(string path, AudioClip audioClip)
    {
        Debug.Log("Start LoadAudioClip");

        // 파일 존재 여부 확인
        if (!File.Exists(path))
        {
            Debug.Log("Didn't Exist: " + path);
            yield break;
        }

        // WWW를 통해 오디오 클립을 가져온다.
        WWW audioFile = new WWW("file:///" + path);
        yield return audioFile;

        if (audioFile.error == null)
        {
            // 성공적으로 가져오면 노래를 재생시킨다.
            Debug.Log("Loaded file successfully");
            audioClip = audioFile.GetAudioClip();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("Open File Error : " + audioFile.error);
            yield break; // stop the coroutine here
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 기본 선택된 난이도는 매우 쉬움으로 한다.
        PlayerInformation.selectLevel = "1";

        // 노래 제목 셋팅
        titleText.text = PlayerInformation.musicTitle;

        // 노래 선택 화면에서 사용자가 선택한 노래 이미지를 셋팅
        titleImage.sprite = PlayerInformation.titleImage;
        // 선택된 title에 맞는 노래 재생
        UpdateSong();
    }

    // Start Button 클릭 시
    public void GameStart()
    {
        SceneManager.LoadScene("NormalGameScene");
    }

    public void BackButtonEvent()
    {
        SceneManager.LoadScene("NormalMusicSelectScene");
    }

    // 토글 버튼 클릭 시 이벤트
    public void ToggleClick_Result(Toggle toggle)
    {
        // 선택된 난이도 확인
        string level = toggle.GetComponentInChildren<Text>().text;
        PlayerInformation.selectLevel = level;
    }
}

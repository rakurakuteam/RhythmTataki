using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalSelectLevelManager : MonoBehaviour
{
    // UI를 사용하려면 public으로 해야함 
    public Image titleImage;

    // 해당 노래에 맞춘 노래가 나오도록
    private void UpdateSong()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        // 리소스에서 오디오 파일을 불러온다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        audioSource.clip = audioClip;
        audioSource.Play();

        // 또 노트 정보가 담긴 Text파일을 불러온다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
    }

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

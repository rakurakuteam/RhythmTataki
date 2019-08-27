using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasAudio2 : MonoBehaviour
{
    public AudioSource audios;

    public bool IsPause;
    public GameObject pausePanel;

    private void Awake()
    {
        audios.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        IsPause = false;
        // 패널숨김
        pausePanel.SetActive(false);
        Invoke("Music", 2.7f);
    }

    private void Update()
    {
        if (Swipe.instance.on == true)
        {
            audios.Stop();
        }
        if (Box.instance.MusicOff == true)
        {
            audios.Stop();
        }
    }
    void Music()
    {
        audios = GetComponent<AudioSource>();
        audios.Play();
     
    }

    // 일시정지 버튼 클릭 시 
    public void pauseButtonEvent()
    {
        IsPause = true;
        Time.timeScale = 0;
        audios.Pause();
        pausePanel.SetActive(true);

        // 입력을 못받게 해야함.
    }

    // 닫기 버튼
    public void BackButtonEvent()
    {
        Time.timeScale = 1;
        audios.Play();
        IsPause = false;
        pausePanel.SetActive(false);

    }

    // 계속하기 버튼 클릭 시
    public void ContinueButtonEvent()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        audios.Play();
        IsPause = false;
    }

    // 다시하기 버튼 클릭 시
    public void RetryButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ArcadeModeScene");

        CancelInvoke();
        // StopAllCoroutines();
    }

    // 홈버튼 클릭 시
    public void HomeButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SelectModeScene");

        CancelInvoke();
        // StopAllCoroutines();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteBlockGameManager : MonoBehaviour
{
    public bool IsPause;
    public GameObject pausePanel;

    // 음악변수
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
        // 패널숨김
        pausePanel.SetActive(false);
    }

    // 일시정지 버튼 클릭 시 
    public void pauseButtonEvent()
    {
        IsPause = true;
        Time.timeScale = 0;
        //audioSource.Pause();
        pausePanel.SetActive(true);

        // 입력을 못받게 해야함.
    }

    // 닫기 버튼
    public void BackButtonEvent()
    {
        Time.timeScale = 1;
        //audioSource.Play();
        IsPause = false;
        pausePanel.SetActive(false);

    }

    // 계속하기 버튼 클릭 시
    public void ContinueButtonEvent()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        //audioSource.Play();
        IsPause = false;
    }

    // 다시하기 버튼 클릭 시
    public void RetryButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ArcadeModeScene");

        // CancelInvoke();
        // StopAllCoroutines();
    }

    // 홈버튼 클릭 시
    public void HomeButtonEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SelectModeScene");

        // CancelInvoke();
        // StopAllCoroutines();
    }
}

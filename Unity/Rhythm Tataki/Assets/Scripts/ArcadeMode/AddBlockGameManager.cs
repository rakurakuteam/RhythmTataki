using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddBlockGameManager : MonoBehaviour
{
    public bool IsPause;
    public GameObject pausePanel;

    // 음악변수
    public AudioSource BGMAudioSource;
    public AudioClip BGMAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
        Invoke("Music", 1);
        // 패널숨김
        pausePanel.SetActive(false);
    }

    void Music()
    {
        BGMAudioSource = GetComponent<AudioSource>();
    }

    // 일시정지 버튼 클릭 시 
    public void pauseButtonEvent()
    {
        IsPause = true;
        Time.timeScale = 0;
        BGMAudioSource.Pause();
        pausePanel.SetActive(true);

        // 입력을 못받게 해야함.
    }

    // 닫기 버튼
    public void BackButtonEvent()
    {
        Time.timeScale = 1;
        BGMAudioSource.Play();
        IsPause = false;
        pausePanel.SetActive(false);

    }

    // 계속하기 버튼 클릭 시
    public void ContinueButtonEvent()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        BGMAudioSource.Play();
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

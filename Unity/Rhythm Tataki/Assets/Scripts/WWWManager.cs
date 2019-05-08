using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// DB처리를 위한 Manager
public class WWWManager : MonoBehaviour
{
    // 싱글톤으로 처리한다.
    public static WWWManager instance { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public string base_URL = "http://dev.rhythmtataki.p-e.kr/unity/";

    // 로그인 확인 라라벨로 송부
    IEnumerator StartLogin(string email, string pw)
    {

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("pw", pw);

        WWW www = new WWW(base_URL + "login", form);
        // 데이터가 올 때까지 기다림.
        yield return www;

        if (www.error == null)
        {
            // 성공한 경우
            Debug.Log("WWW OK : " + www.text);
            if (www.text == "1")
            {
                // 1을 리턴받으면 로그인 성공한 경우
                Debug.Log("로그인 성공");
                SceneManager.LoadScene("SelectModeScene");
            }
            else
            {
                // 0을 리턴받으면 로그인 실패한 경우
                Debug.Log("로그인 실패");
            }
        }
        else
        {
            // 실패한 경우
            Debug.Log("WWW Error : " + www.error);
        }
    }

    // 회원가입
    IEnumerator StartJoin(string name, string email, string pw)
    {
        Debug.Log("함수 실행");
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("email", email);
        form.AddField("pw", pw);

        WWW www = new WWW(base_URL + "join", form);
        // 데이터가 올 때까지 기다림.
        yield return www;

        if (www.error == null)
        {
            // 성공한 경우
            Debug.Log("WWW OK : " + www.text);
            if (www.text == "1")
            {
                // 1을 리턴받으면 회원가입 성공한 경우
                Debug.Log("회원가입 성공");
                SceneManager.LoadScene("LoginScene");
            }
            else
            {
                // 0을 리턴받으면 회원가입 실패한 경우
                Debug.Log("회원가입 실패");
            }
        }
        else
        {
            Debug.Log("WWW Error : " + www.error);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
    // 이름 이메일 및 패스워드 UI
    public InputField nameInputField;
    public InputField emailInputField;
    public InputField passwordInputField;

    // 오류에 대한 정보를 보여 줄 UI
    public Text messageUI;

    // 데이터를 전송할 URL
    private string Join_URL = "http://capstone.rhythmtataki.p-e.kr/unity/join";
    // private string Join_URL = "http://dev.rhythmtataki.p-e.kr/unity/join";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Join()
    {
        string name = nameInputField.text;
        string email = emailInputField.text;
        string pw = passwordInputField.text;
        Debug.Log(name);
        Debug.Log(email);
        Debug.Log(pw);

        StartCoroutine(StartJoin(name, email, pw));
    }

    // 회원가입 확인 라라벨로 송부
    IEnumerator StartJoin(string name, string email, string pw)
    {
        Debug.Log("함수 실행");
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("email", email);
        form.AddField("pw", pw);

        WWW www = new WWW(Join_URL, form);
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

    // 뒤로가기 버튼 클릭 시
    public void Back()
    {
        SceneManager.LoadScene("LoginScene");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

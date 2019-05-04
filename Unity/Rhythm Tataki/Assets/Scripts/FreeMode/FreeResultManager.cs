using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class FreeResultManager : MonoBehaviour
{
    
    void Start()
    {
        // 사용자가 선택한 노래의 파일 확장자 까지 가져옴.
        

        // 사용자의 동영상파일, txt파일, 노래파일의 제목을 가지고와서 string 배열로 저장함.
    }

    // 저장하기 버튼 클릭 시
    public void SaveButtonEvent()
    {

    }

    public void RetryButtonEvent()
    {
        SceneManager.LoadScene("FreeMusicSelectScene");
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    // 파일 업로드
    IEnumerator FileUpload(string path)
    {
        int count = 3;
        int listIndex = PlayerInformation.titleList.FindIndex(x => x.Equals(PlayerInformation.selectedMusic));
        // 만약 사용자가 선택한 노래가 기본 제공하는 노래라면 그 노래는 서버에 저장할 필요가 없다.
        if (listIndex < PlayerInformation.musicCount) {
            count = 2;
        }

        WWW[] uploadFileArr = new WWW[count];
        WWWForm postForm = new WWWForm();
        postForm.AddField("email", PlayerInformation.userEmail); // 메일 추가

        // 사용자가 선택한 노래의 파일명과 확장자를 가져옴.
        string fileName = PlayerInformation.selectedMusic;
        // AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        



        // 확장자 string
        string[] extension = { ".mp4", "txt", ""};

        // 동영상 바인딩
        WWW file = new WWW("file:///" + Path.DirectorySeparatorChar + fileName + ".mp4");
        

        for (int i = 0; i < count; i++)
        {
            // Path.DirectorySeparatorChar = "/" 를 의미
            // WWW file = new WWW("file:///" + Path.DirectorySeparatorChar + fileName);

            // 파일이 있는지 확인
            yield return file;

            if(file.error == null)
            {
                Debug.Log("Loaded file successfully");
            }
            else
            {
                Debug.Log("Open File Error : " + file.error);
                yield break; // stop the coroutine here
            }
            
            postForm.AddBinaryData("file[" + i + "]", file.bytes, "1.ogg");

        }

        WWW upload = new WWW("http://dev.rhythmtataki.p-e.kr/unity/fileUpload", postForm);

        yield return upload;

        if (upload.error == null)
        {
            Debug.Log("upload done : " + upload.text);
        }
        else
        {
            Debug.Log("Error during upload : " + upload.error);
        }
    }
}

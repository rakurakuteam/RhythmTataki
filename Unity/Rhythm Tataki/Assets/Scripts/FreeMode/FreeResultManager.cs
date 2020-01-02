using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class FreeResultManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath;
        // 사용자가 선택한 노래의 파일 확장자 까지 가져옴.


        // 사용자의 동영상파일, txt파일, 노래파일의 제목을 가지고와서 string 배열로 저장함.
        Debug.Log(PlayerInformation.selectedMusic);
    }

    // 저장하기 버튼 클릭 시
    public void SaveButtonEvent()
    {
        Debug.Log("Clicked Sava Button");
        StartCoroutine(FileUpload(filePath));
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
        string[] folderNames = { "records", "beats", "beats" };
        string[] extension = { ".mp4", ".txt", ".ogg" };

        int listIndex = PlayerInformation.titleList.FindIndex(x => x.Equals(PlayerInformation.selectedMusic));

        WWW[] uploadFileArr = new WWW[count];
        WWWForm postForm = new WWWForm();
        postForm.AddField("email", PlayerInformation.userEmail); // 사용자 이메일 바인딩.

        // 사용자가 선택한 노래의 파일명과 확장자를 가져옴.
        string fileName = PlayerInformation.selectedMusic; // 숫자가 나옴.

        for (int i = 0; i < count; i++)
        {
            // Path.DirectorySeparatorChar = "/" 를 의미
            WWW file = new WWW("file:///" + path + Path.DirectorySeparatorChar + folderNames[i] + Path.DirectorySeparatorChar + fileName + extension[i]);

            // 파일이 있는지 확인
            yield return file;

            if(file.error == null)
            {
                Debug.Log("Loaded file successfully");
                uploadFileArr[i] = file;
            }
            else
            {
                Debug.Log("Open File Error : " + file.error);
                yield break; // stop the coroutine here
            }
            
            // 파일 세개 바인딩
            postForm.AddBinaryData("file[" + i + "]", uploadFileArr[i].bytes, fileName + extension[i]);

        }

        // 리퀘스트
        WWW upload = new WWW("http://capstone.rhythmtataki.p-e.kr/unity/fileUpload", postForm);

        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done : " + upload.text);
            // 화면전환
            SceneManager.LoadScene("FreeMusicSelectScene");
        }
        else
        {
            Debug.Log("Error during upload : " + upload.error);
        }
    }

    // 파일 업로드
    IEnumerator FilesUpload(string path, string fileName)
    {
        WWW localFile = new WWW("file:///" + path + fileName);

        Debug.Log("file:///" + path + fileName);

        yield return localFile;
        if (localFile.error == null)
        {
            Debug.Log("Loaded file successfully");
        }
        else
        {
            Debug.Log("Open File Error : " + localFile.error);
            yield break; // stop the coroutine here
        }

        WWW localFile2 = new WWW("file:///" + path + "/test2.txt");
        yield return localFile2;
        if (localFile2.error == null)
        {
            Debug.Log("Loaded file successfully");
        }
        else
        {
            Debug.Log("Open File Error : " + localFile2.error);
            yield break; // stop the coroutine here
        }

        Debug.Log(localFile2.size);

        WWW localFile3 = new WWW("file:///" + path + "/test.mp4");
        yield return localFile3;
        if (localFile3.error == null)
        {
            Debug.Log("Loaded file successfully");
        }
        else
        {
            Debug.Log("Open File Error : " + localFile3.error);
            yield break; // stop the coroutine here
        }

        Debug.Log(localFile3.size);

        WWWForm postForm = new WWWForm();
        postForm.AddField("email", PlayerInformation.userEmail);
        postForm.AddBinaryData("file[0]", localFile.bytes, "1.ogg");
        postForm.AddBinaryData("file[1]", localFile2.bytes, "1.txt");
        postForm.AddBinaryData("file[2]", localFile3.bytes, "1.mp4");

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

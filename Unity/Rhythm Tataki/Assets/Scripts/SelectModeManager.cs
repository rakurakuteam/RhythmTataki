using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class SelectModeManager : MonoBehaviour {
    // 데이터를 전송할 URL
    private string URL = "https://capstone.rhythmtataki.p-e.kr/unity/getScores/";
    //private string URL = "http://dev.rhythmtataki.p-e.kr/unity/getScores/";
    public string jsonString;

    public void GoHome()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void GoFreeMode()
    {
        SceneManager.LoadScene("FreeMusicSelectScene");
    }

    public void GoNormalMode()
    {
        SceneManager.LoadScene("NormalMusicSelectScene");
    }

    public void GoArcadeMode()
    {
        SceneManager.LoadScene("ArcadeModeScene");
    }

    public void GoCloudGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Start()
    {
        var path = Directory.GetCurrentDirectory();
        Debug.Log(path);
        // 파일 업로드 테스트
        // StartCoroutine(FileUpload(Application.persistentDataPath, "/솜사탕.ogg"));

        // 사용자가 새로고침하면 로딩화면 보여주고
        // musicCount를 다운받은 음악의 수 만큼 + 해주고
        // 다운받은 음악의 수 만큼 반복.

        // 음악 파일 확인
        string pt = "/storage/Music/";

        string p = "/storage/";


        // 파일 생성.
        StreamWriter sw = new StreamWriter(pt + "test.txt", true);
        // 먼저 노래 제목을 첫번째 줄에 넣어준다.
        sw.WriteLine(PlayerInformation.selectedMusic);

        sw.Close();
    }

    // 파일 업로드
    IEnumerator FileUpload(string path, string fileName)
    {
        // UnityEngine.WWW.EscapeURL(fileName, System.Text.Encoding.UTF8);
        byte[] contents = Encoding.Default.GetBytes(fileName);
        string encodingFile = Encoding.UTF8.GetString(contents);
        Debug.Log(encodingFile);
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

    //ienumerator getaudioclip()
    //{
    //    using (var uwr = unitywebrequestmultimedia.getaudioclip("http://dev.rhythmtataki.p-e.kr/unity/filedownload/bbb@naver.com/솜사탕.ogg", audiotype.oggvorbis))
    //    {
    //        yield return uwr.sendwebrequest();
    //        if (uwr.isnetworkerror || uwr.ishttperror)
    //        {
    //            debug.logerror(uwr.error);
    //            yield break;
    //        }

    //        audioclip clip = downloadhandleraudioclip.getcontent(uwr);
    //        // use audio clip
    //        debug.log(clip);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public Image musicTitleImage;
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image starsUI;
    public Image newRecordUI;    

    // 전체 게임 결과에 대한 정보를 받아낼 수 있도록 한다.
    // Start is called before the first frame update
    void Start()
    {
        musicTitleImage.sprite = PlayerInformation.titleImage;
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "점수 : " + (int)PlayerInformation.score;
        maxComboUI.text = "최대 콤보 : " + PlayerInformation.maxCombo;
        newRecordUI.enabled = false;

        // 리소스에서 노트 파일을 불러온다
        // 별의 개수를 기준에 맞춰서 보여주기 위함.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        // 첫번째 두번째 줄을 무시한다.
        reader.ReadLine();
        reader.ReadLine();
        // 세번째 줄에 점수기준이 있기 때문에 세번째 줄을 가지고온다.
        string beatInformation = reader.ReadLine();
        // 4번째있는 것이 별3개, 5번째가 별 2개, 6번째가 별 1개
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        // 점수에 맞는 별 이미지를 보여준다.
        if(PlayerInformation.score >= scoreS)
        {
            starsUI.sprite = Resources.Load<Sprite>("Sprites/stars_S_Rank");
        }
        else if(PlayerInformation.score >= scoreA)
        {
            starsUI.sprite = Resources.Load<Sprite>("Sprites/stars_A_Rank");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            starsUI.sprite = Resources.Load<Sprite>("Sprites/stars_B_Rank");
        }
        else
        {
            starsUI.sprite = Resources.Load<Sprite>("Sprites/stars_default");
        }

        // 점수를 가지고 최고기록이 경신되었는지 확인하는 코루틴 호출.
        StartCoroutine(CheckNewRecord(PlayerInformation.userEmail, PlayerInformation.score));
        
    }

    public void Replay()
    {
        SceneManager.LoadScene("NormalMusicSelectScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // DB에 점수를 보낸 후 최고점수가 경신되었는지 확인한다.
    // 최고기록이 경신되었으면 PHP에서는 1을 return 한다.
    IEnumerator CheckNewRecord(string email, float score)
    {

        Debug.Log("SetScore실행");
        // WWWForm form = new WWWForm();
        // form.AddField("email", email);


        Debug.Log(PlayerInformation.URL + email + "?" + score);
        WWW www = new WWW(PlayerInformation.URL + "setScore/" + email + "/" + musicTitleUI.text + "/" + score);
        yield return www;

        if (www.error == null)
        {
            // 성공한 경우
            Debug.Log("WWW OK : " + www.text);
            StartCoroutine(SetNewRecordEffect(www.text, 1.0f));
        }
        else
        {
            // 실패한 경우
            Debug.Log("WWW Error : " + www.error);
        }
    }

    // PHP의 return 값이 1이라면 최고 기록이 경신된 것
    // 따라서 씬에서 경신되었다는 것을 알려주는 Text 초기화한다
    // 그리고 점수 List의 값도 바꿔준다.
    IEnumerator SetNewRecordEffect(string flag, float time)
    {
        // 1초 대기 후 아래 실행
        yield return new WaitForSeconds(time);
        if (flag.Equals("1"))
        {
            // 새로운 기록이라는 것을 띄어준다.
            newRecordUI.enabled = true;
            // 그리고 환호성 소리 재생
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            // 이후 해당곡의 점수와 별 이미지 초기화
            // 타이틀로 index를 알아냄
            int index = PlayerInformation.titleList.FindIndex(item => item.Equals(PlayerInformation.selectedMusic));

            // 점수 저장
            PlayerInformation.scores[index] = (int) PlayerInformation.score;
            // 이미지 저장
            PlayerInformation.rankSprites[index] = starsUI.sprite;
            
        }
    }
}

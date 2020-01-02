using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using ICSharpCode.SharpZipLib.Zip;

public class NormalSelectSongManager : MonoBehaviour
{
    public Image musicTitleImage;
    public GameObject setupPanel;
    public GameObject setupButton;

    // 리스트뷰 추가 관련 변수
    public Transform Content;
    public GameObject ListObject;

    // 드럼 리스트뷰 추가 관련 변수
    public Transform leftDrumContent;
    public Transform rightDrumContent;
    public GameObject toggleItem;
    public ToggleGroup leftToggleGroup;
    public ToggleGroup rightToggleGroup;
    public int drumType;

    private string beatsFolder = "/beats";
    private string zipFileName = "songs.zip";
    private string zipFilePath;
    
    // 초기화 버튼 눌렀을 때 가져온 파일의 개수
    // 리스트뷰에 리스트를 추가할 때 반복의 횟수로 쓰임.
    private int loadMusicCount = 0;
   
    void Start()
    {
        setupPanel.SetActive(false);
        zipFilePath = Application.persistentDataPath + beatsFolder + Path.DirectorySeparatorChar + zipFileName;
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void SettingButtonEvent()
    {
        // Debug.Log("셋팅버튼 클릭됨");
        setupPanel.SetActive(true);
        // setupButton.SetActive(false);
    }

    public void BackButtonEvent()
    {
        setupPanel.SetActive(false);
        // setupButton.SetActive(true);
    }

    public void ReloadButtonEvent()
    {
        // Debug.Log("clicked Reload Button");
        StartCoroutine(FileDownload());
    }

    public void DrumSoundReloadButtonEvent()
    {
        AddDrumSoundInformation("clap");
        AddDrumListViewItem();
        AudioClip audio = Resources.Load<AudioClip>("EffectSounds/" + "clap");
        // PlayerInformation.leftDrumSound = audio;
        PlayerInformation.rightDrumSound = audio;
    }

    IEnumerator FileDownload()
    {
        string url = "http://capstone.rhythmtataki.p-e.kr/unity/fileDownload/" + PlayerInformation.userEmail;

        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log((int)request.downloadedBytes);
            FileStream fs = new FileStream(Application.persistentDataPath + beatsFolder + Path.DirectorySeparatorChar + zipFileName, FileMode.Create);
            fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
            fs.Close();

            Debug.Log("OK");
        }

        bool unZipCheck = UnZipFiles(zipFilePath, Application.persistentDataPath + beatsFolder + Path.DirectorySeparatorChar, true);
        if(!unZipCheck)
        {
            Debug.Log("Do Not UpZipCheck");
        }
        // zip파일을 받아오고 해제 하였으면 리스트뷰에 담고 리스트로 보여준다.
        
    }

    /*
     * 압축파일을 받으면 압축을 풀어서 파일을 하나씩 원하는 경로에 저장하는 것
     * zipFilePath: zip 파일의 경로
     * upZipTargetFolderPath: 압축 해제 후 파일들 저장하려는 경로
     * isDeleteZipFile: zip 파일의 삭제 여부(true 삭제, false 삭제안함)
     */ 
    public bool UnZipFiles(string zipFilePath, string unZipTargetFolderPath, bool isDeleteZipFile)
    {
        bool retVal = false;
        // ZIP 파일이 있는 경우만 수행.
        if (File.Exists(zipFilePath))
        {
            // 목록 생성을 위해 음악 카운트 더해줌
            // 이걸로 파일이름을 지정해줌.
            //PlayerInformation.musicCount++;
            //loadMusicCount++;

            // Debug.Log("Exists Zip file");
            // ZIP 스트림 생성.
            ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath));
            try
            {
                // Debug.Log(zipInputStream.GetNextEntry());
                ZipEntry theEntry;

                // 반복하며 파일을 가져옴.
                while ((theEntry = zipInputStream.GetNextEntry()) != null)
                {
                    loadMusicCount++;
                    if(loadMusicCount % 2 == 1)
                    {
                        PlayerInformation.musicCount++;
                    }

                    // 폴더
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name); // 파일
                    string fileExtension = Path.GetExtension(theEntry.Name); // 파일 확장자만
                    
                    // 폴더 생성
                    Directory.CreateDirectory(unZipTargetFolderPath + directoryName);
                    Debug.Log(unZipTargetFolderPath + directoryName);
                    // 파일 확장자 있는 경우
                    if (fileExtension != String.Empty)
                    {
                        // 파일 스트림 생성.(파일생성)
                        FileStream streamWriter = File.Create((unZipTargetFolderPath + PlayerInformation.musicCount + fileExtension));
                        int size = 2048;
                        byte[] data = new byte[size];
                        // 파일 복사
                        while (true)
                        {
                            size = zipInputStream.Read(data, 0, data.Length);
                            if (size > 0)
                                streamWriter.Write(data, 0, size);
                            else
                                break;
                        }
                        // 파일스트림 종료
                        streamWriter.Close();
                    }

                    if(fileExtension.Equals(".txt"))
                    {
                        AddMusicInformation((PlayerInformation.musicCount).ToString());
                        AddListViewItem();
                    }
                }
                retVal = true;
            }
            catch
            {
                retVal = false;
            }
            finally
            {
                // ZIP 파일 스트림 종료
                zipInputStream.Close();
            }

            // ZIP파일 삭제를 원할 경우 파일 삭제.
            if (isDeleteZipFile)
                try
                {
                    File.Delete(zipFilePath);
                }
                catch {}
        }
        return retVal;
    }

    // 텍스트에서 첫번째줄을 읽어 제목을 return하는 함수
    public string GetTitleName(string txtFileName)
    {
        string txtFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + txtFileName + ".txt";
        StreamReader reader = new StreamReader(txtFilePath);

        string titleName = reader.ReadLine();
        return titleName;
    }

    // 다운받은 노래를 리스트뷰에 넣기 위해 리스트 정보를 PlayerInformation 에 저장함.
    public void AddMusicInformation(string fileName)
    {
        // 확장자를 제외한 파일명만 추출
        string fileNameExceptionExtension = GetTitleName(fileName);

        // 노래 제목 리스트에 선택한 노래의 제목을 추가한다.
        // 나중에 리스트에 보여주기 위해서.
        string listFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "List.txt";
        StreamWriter sw = new StreamWriter(listFilePath, true);
        sw.WriteLine(fileNameExceptionExtension);
        sw.Close();

        PlayerInformation.titleSprites.Add(Resources.Load<Sprite>("SongImages/user_select"));
        PlayerInformation.titleList.Add(fileNameExceptionExtension);
        PlayerInformation.scores.Add(0);
        PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_default"));
        // PlayerInformation.musicCount++;
        PlayerInformation.noteFileNames.Add((PlayerInformation.musicCount).ToString());
        // Debug.Log(PlayerInformation.musicCount);
    }

    // 기존의 리스트뷰에 새로 추가한 노래의 리스트를 넣는다.
    public void AddListViewItem()
    {
        Item itemTemp = new Item();

        itemTemp.titleImage = PlayerInformation.titleSprites[PlayerInformation.titleSprites.Count - 1];
        itemTemp.title = PlayerInformation.titleList[PlayerInformation.titleList.Count - 1];
        itemTemp.rankImage = PlayerInformation.rankSprites[PlayerInformation.rankSprites.Count - 1];
        itemTemp.noteName = PlayerInformation.noteFileNames[PlayerInformation.noteFileNames.Count - 1];
        itemTemp.onItemClick = new Button.ButtonClickedEvent();
        // 이벤트는 ListViewController에 있는 ItemClick_Result()를 가져와야 한다.
        itemTemp.onItemClick.AddListener(delegate { GameObject.Find("Normal List View Controller").GetComponent<NormalListViewController>().ItemClick_Result(); });

        GameObject btnItemTemp;
        ListObject itemobjectTemp;

        btnItemTemp = Instantiate(this.ListObject) as GameObject;
        itemobjectTemp = btnItemTemp.GetComponent<ListObject>();

        //각 속성 입력
        itemobjectTemp.titleText.text = itemTemp.title;
        itemobjectTemp.titleImage.sprite = itemTemp.titleImage;
        itemobjectTemp.rankImage.sprite = itemTemp.rankImage;
        itemobjectTemp.NoteNameText.text = itemTemp.noteName;
        itemobjectTemp.item.onClick = itemTemp.onItemClick;

        //화면에 추가
        btnItemTemp.transform.SetParent(this.Content);
        // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
        btnItemTemp.transform.localScale = Vector3.one;
    }

    // 다운받은 노래를 리스트뷰에 넣기 위해 리스트 정보를 PlayerInformation 에 저장함.
    public void AddDrumSoundInformation(string fileName)
    {
        // 확장자를 제외한 파일명만 추출
        string fileNameExceptionExtension = "clap";

        // 노래 제목 리스트에 선택한 노래의 제목을 추가한다.
        // 나중에 리스트에 보여주기 위해서.
        //string listFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "List.txt";
        //StreamWriter sw = new StreamWriter(listFilePath, true);
        //sw.WriteLine(fileNameExceptionExtension);
        //sw.Close();

        PlayerInformation.soundNameList.Add(fileNameExceptionExtension);
        PlayerInformation.soundFileNameList.Add(fileNameExceptionExtension);
        PlayerInformation.drumSoundCount++;
    }

    // 기존의 리스트뷰에 새로 추가한 노래의 리스트를 넣는다.
    public void AddDrumListViewItem()
    {
        InItem itemTemp = new InItem();

        itemTemp.soundName = PlayerInformation.soundNameList[PlayerInformation.soundNameList.Count - 1];
        itemTemp.soundFileName = PlayerInformation.soundFileNameList[PlayerInformation.soundFileNameList.Count - 1];
        itemTemp.OnToggleClick = new Toggle.ToggleEvent();
        // 이벤트는 ListViewController에 있는 ItemClick_Result()를 가져와야 한다.
        itemTemp.OnToggleClick.AddListener(delegate { GameObject.Find("Left Drum Sound List Manager").GetComponent<DrumSoundListViewManager>().ToggleClick_Result(); });

        GameObject toggleItemTemp;
        ToggleItem itemobjectTemp;
        int AddDrumSoundLoop = 2;

        for(int i = 0; i < AddDrumSoundLoop; i++)
        {
            //추가할 오브젝트를 생성한다.
            toggleItemTemp = Instantiate(this.toggleItem) as GameObject;
            //오브젝트가 가지고 있는 'ToggleItem'를 찾는다.
            itemobjectTemp = toggleItemTemp.GetComponent<ToggleItem>();

            //각 속성 입력
            itemobjectTemp.soundsName.text = itemTemp.soundName;
            itemobjectTemp.soundFileName.text = itemTemp.soundFileName;
            itemobjectTemp.item.onValueChanged = itemTemp.OnToggleClick;

            if (i % 2 == 0)
            {
                itemobjectTemp.item.group = leftToggleGroup;
                toggleItemTemp.transform.SetParent(this.leftDrumContent);
            }
            else
            {
                itemobjectTemp.item.group = rightToggleGroup;
                toggleItemTemp.transform.SetParent(this.rightDrumContent);
            }

            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            toggleItemTemp.transform.localScale = Vector3.one;
        }
    }
}

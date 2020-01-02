using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;
using System;


public class FreeSelectSongManager : MonoBehaviour
{

    public GameObject setupPanel;
    public GameObject setupButton;
    public GameObject AddMusicPanel;

    public GameObject musicListObject;
    
    public Transform content;
    public List<MusicItem> musicItemList;
    public List<string> musicFileNameList; // Music폴더의 음악파일의 이름을 저장하는 List

    private string musicFolderPath;
    private string systemPath;
    private string extension;

    public GameObject ListObject;
    public Transform Content;

    // Start is called before the first frame update
    void Start()
    {
        // 셋팅 패널과 음악 추가 패널은 처음에 보여주지 않음.
        setupPanel.SetActive(false);
        AddMusicPanel.SetActive(false);

        musicFileNameList = new List<string>();
        musicItemList = new List<MusicItem>();

        // Music 폴더 경로
        // musicFolderPath = "/storage/emulated/0/Music";   // 안드로이드
        musicFolderPath = "C:/Users/SAMSUNG9/Music";    //  윈도우
        systemPath = Application.persistentDataPath;
        extension = "*.ogg";   // 확장자. ogg확장자를 가진 모든 파일을 나타냄.

        // Music폴더에서 원하는 확장자의 파일명을 가져온다.
        FindMusicFile(musicFolderPath, extension, musicFileNameList);
        AddListInItem();
        Binding();
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("SelectModeScene");
    }

    public void SettingButtonEvent()
    {
        setupPanel.SetActive(true);
        // setupButton.SetActive(false);
    }

    public void BackButtonEvent()
    {
        setupPanel.SetActive(false);
        // setupButton.SetActive(true);
    }

    public void MusicPanelBackButtonEvent()
    {
        AddMusicPanel.SetActive(false);
    }

    /*
     * AddButtonEvent
     * 사용자가 원하는 노래를 추가하기 위한 버튼
     * 해당 버튼을 클릭하면 Music폴더의 노래를 가져와서 ListView형태로 보여준다.
     */
    public void AddButtonEvent()
    {
        AddMusicPanel.SetActive(true); // 패널 활성화
    }

    // 폴더안에서 음악 파일을 찾는 함수
    /*
     * 폴더안에서 음악 파일을 찾는 함수
     * directory : 파일을 가져올 폴더 명
     * ext_name : 확인할 확장자명
     * musicFileNameList : 파일명을 담아 둘 List
     */
    public void FindMusicFile(string directory, string ext_name, List<string> inputList)
    {
        Debug.Log("in FindMusicFile");

        DirectoryInfo di = new DirectoryInfo(directory);

        // 폴더 존재여부 확인.
        if(di.Exists)
        {
            // 존재 할 경우
            // 디렉토리의 파일을 가져와서 배열로 저장한다.
            FileInfo[] files = di.GetFiles(ext_name);

            // 반복을 하면서 배열에 파일정보를 저장한다.
            foreach (FileInfo file in files)
            {
                // 중복되는 파일이 이미 List에 있으면 넘어간다.
                if (inputList.Contains(file.Name))
                {
                    continue;
                }
                else
                {
                    inputList.Add((file.Name).Substring(0, file.Name.LastIndexOf('.')));
                }
            }
        }
        else
        {
            Debug.Log("Folder Not Exists");
        }
    }

    // ListObject안의 Item 셋팅
    public void AddListInItem()
    {
        MusicItem itemTemp;
        
        for (int i = 0; i < musicFileNameList.Count; i++)
        {
            itemTemp = new MusicItem();

            itemTemp.musicName = musicFileNameList[i];
            itemTemp.onItemClick = new Button.ButtonClickedEvent();
            itemTemp.onItemClick.AddListener(delegate { ItemClick_Result(); });

            // 리스트뷰의 항목인 Item을 만들어서 musicItemList 추가한다.
            musicItemList.Add(itemTemp);
        }
    }

    // InItem 리스트의 InItem을 리스트뷰에 바인딩 함으로써
    // 실제 리스트뷰의 컨텐츠에 추가하고 화면에 보여주도록 한다.
    public void Binding()
    {
        GameObject btnItemTemp;
        MusicListObject itemobjectTemp;

        foreach (MusicItem item in musicItemList)
        {
            //추가할 오브젝트를 생성한다.
            btnItemTemp = Instantiate(this.musicListObject) as GameObject;
            //오브젝트가 가지고 있는 'MusicListObject'를 찾는다.
            itemobjectTemp = btnItemTemp.GetComponent<MusicListObject>();

            //각 속성 입력
            itemobjectTemp.musicName.text = item.musicName;
            itemobjectTemp.item.onClick = item.onItemClick;

            //화면에 추가
            itemobjectTemp.transform.SetParent(this.content);
            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            itemobjectTemp.transform.localScale = Vector3.one;
        }
    }
    /*
     * 사용자 노래 리스트에서 노래를 선택했을 때
     * 1. 먼저 해당 노래를 게임내부 폴더로 복사한다.
     * 2. 노래 List가 담긴 text파일에 노래를 추가해준다.
     * 3. 노래의 제목을 바꾼다.
     * 4. 버튼 아이템을 만들어 리스트뷰에 추가시켜준다.
     */ 
    public void ItemClick_Result()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        MusicListObject listObject = obj.GetComponent<MusicListObject>();
        
        string musicFileName = listObject.musicName.text;

        // 파일 정보를 PlayerInformation의 정보에담는다.
        AddMusicInformation(musicFileName);

        string originalFilePath = musicFolderPath + Path.DirectorySeparatorChar + musicFileName + ".ogg";
        string saveFilePath = systemPath + Path.DirectorySeparatorChar + "beats" + Path.DirectorySeparatorChar + PlayerInformation.musicCount + ".ogg"; // 여기 카운트 들어갸야함
        Debug.Log(saveFilePath);

        // 파일 복사하는 함수 호출
        CopyMusicFile(originalFilePath, saveFilePath);

        // 리스트뷰에 추가 하는 함수 호출
        AddListViewItem();

        // 창을 닫고 로딩화면을 보여준다.
        AddMusicPanel.SetActive(false);
    }

    /*
     * 파일 복사하는 것
     * originalFilePath : 복사하고자 하는 대상 파일의 경로
     * saveFilePath : 복사하고 나서 저장할 파일의 경로
     */
    public void CopyMusicFile(string originalFilePath, string saveFilePath)
    {
        if (File.Exists(originalFilePath))
        {
            FileInfo fileInfo = new FileInfo(originalFilePath);

            // 파일 복사 (원래파일경로, 이동하고자하는 경로, true)는 중복파일일 경우 덮어쓰기.
            // false일 경우 해당 파일이 있으면 오류 발생(try catch 문을 이용하여도됨)
            File.Copy(originalFilePath, saveFilePath, true);

            Debug.Log("File copy complete");
        }
        else
        {
            Debug.Log("File does not Exist");
        }
    }

    // 사용자가 선택한 노래를 리스트뷰에 넣기 위해 리스트 정보를 PlayerInformation 에 저장함.
    public void AddMusicInformation(string fileName)
    {
        // 확장자를 제외한 파일명만 추출
        // string fileNameExceptionExtension = fileName.Substring(0, fileName.IndexOf("."));

        // 노래 제목 리스트에 선택한 노래의 제목을 추가한다.
        // 나중에 리스트에 보여주기 위해서.
        string listFilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "List.txt";
        StreamWriter sw = new StreamWriter(listFilePath, true);
        sw.WriteLine(fileName);
        sw.Close();
        
        PlayerInformation.titleSprites.Add(Resources.Load<Sprite>("SongImages/user_select"));
        PlayerInformation.titleList.Add(fileName);
        PlayerInformation.scores.Add(0);
        PlayerInformation.rankSprites.Add(Resources.Load<Sprite>("Sprites/stars_default"));
        PlayerInformation.musicCount++;
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
        itemTemp.onItemClick.AddListener(delegate { GameObject.Find("List View Controller").GetComponent<FreeListViewController>().ItemClick_Result(); });

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
    
}

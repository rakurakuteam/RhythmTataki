using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class FreeListViewController : MonoBehaviour
{

    public GameObject ListObject;
    public Transform Content;
    public List<Item> ItemList;

    private List<Sprite> normalTitleSprites;  // title Sprites
    private List<string> normalTitleList;     // title List
    private List<Sprite> normalRankSprites;   // rank Sprites

    private int basMusicCount; // 기본 제공하는 노래의 개수.
    // Start is called before the first frame update
    void Start()
    {
        basMusicCount = 12;

        // 노래 리스트 셋팅
        AddListItem();

        // 바인딩
        Binding();
    }

    private void AddListItem()
    {
        Item itemTemp;

        for(int i = 0; i < PlayerInformation.musicCount; i++)
        {
            itemTemp = new Item();

            itemTemp.titleImage = PlayerInformation.titleSprites[i];
            itemTemp.title = PlayerInformation.titleList[i];
            itemTemp.rankImage = PlayerInformation.rankSprites[i];
            itemTemp.noteName = PlayerInformation.noteFileNames[i];
            itemTemp.onItemClick = new Button.ButtonClickedEvent();
            itemTemp.onItemClick.AddListener(delegate { ItemClick_Result(); });

            // 리스트뷰의 항목인 Item을 만들어서 ItemList에 추가한다.
            ItemList.Add(itemTemp);
        }

        
    }

    // Item 리스트의 Item을 리스트뷰에 바인딩 함으로써
    // 실제 리스트뷰의 컨텐츠에 추가하고 화면에 보여주도록 한다.
    private void Binding()
    {
        GameObject btnItemTemp;
        ListObject itemobjectTemp;

        foreach (Item item in this.ItemList)
        {
            //추가할 오브젝트를 생성한다.
            btnItemTemp = Instantiate(this.ListObject) as GameObject;
            //오브젝트가 가지고 있는 'ListObject'를 찾는다.
            itemobjectTemp = btnItemTemp.GetComponent<ListObject>();
          
            //각 속성 입력
            itemobjectTemp.titleText.text = item.title;
            itemobjectTemp.titleImage.sprite = item.titleImage;
            itemobjectTemp.rankImage.sprite = item.rankImage;
            itemobjectTemp.NoteNameText.text = item.noteName;
            itemobjectTemp.item.onClick = item.onItemClick;

            //화면에 추가
            btnItemTemp.transform.SetParent(this.Content);
            // 스케일을 1로 지정한다 이것을 추가하지 않으면 프리팹의 스케일이 너무 커짐.
            btnItemTemp.transform.localScale = Vector3.one;
        }
    }

    // 아이템 클릭
    public void ItemClick_Result()
    {
        /*
         * 사용자가 선택한 버튼의 정보를 가지고 온다.
         * 먼저 using UnityEngine.EventSystems; namespace를 가지고오고
         * 게임오브젝트를 생성한다. 그 다음에 선택된 오브젝트에서 ListObject를 가지고 오면된다.
         */
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        ListObject listObject = obj.GetComponent<ListObject>();
        // Debug.Log(listObject.item);

        // 그 뒤 UI에서 text, Image 등을 가지고 와서 PlayerInformation에 저장한다.
        PlayerInformation.selectedMusic = listObject.NoteNameText.text;
        PlayerInformation.musicTitle = listObject.titleText.text;
        // Debug.Log(listObject.titleText.text);
        PlayerInformation.titleImage = listObject.titleImage.sprite;
        // Debug.Log(listObject.titleImage.sprite);


        // PlayerInformation.musicCount++;
        // 기존의 노래의 경우 노래파일을 beats폴더로 옮겨준다.
        // 새로 추가하는 노래의 경우 추가할 때 옮겨지기 때문에 필요 없음.
        //if(Convert.ToInt32(PlayerInformation.selectedMusic) < basMusicCount)
        //{
            
        //}

        // 게임씬으로 이동
        SceneManager.LoadScene("FreeModeGameScene");
    }
}
